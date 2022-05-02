using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rso.proto;
using rso.Base;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;


namespace rso
{
    namespace net
    {
        using rso.crypto;

        using TPeerCnt = UInt32;
        using TLongIP = UInt32;
        using TPort = UInt16;
        using TSize = Int32;
        using TCheckSum = UInt64;
        using TPacketSeq = UInt64;

        public delegate void TEventHandler(object sender, SocketAsyncEventArgs e);
        public delegate void TLinkFunc(TPeerCnt PeerNum_, TLongIP LongIP_);
        public delegate void TLinkFailFunc(TPeerCnt PeerNum_, Int32 ErrorCode_);
        public delegate void TUnLinkFunc(TPeerCnt PeerNum_, Enum State_);
        public delegate void TRecvFunc(TPeerCnt PeerNum_, CStream Stream_);

        class CPeer
        {
            const Int32 c_HeaderSize = sizeof(TSize) + sizeof(TCheckSum) + sizeof(TPacketSeq);

            SocketAsyncEventArgs _RecvEvent = null;
            SocketAsyncEventArgs _SendEvent = null;
            Socket _Socket = null;
            TLongIP _LongIP;
            SKey _Key;
            CPeriod _HBRcvPeriod = null;
            CPeriod _HBSndPeriod = null;
            TLinkFunc _LinkFunc = null;
            TLinkFailFunc _LinkFailFunc = null;
            TUnLinkFunc _UnLinkFunc = null;
            TRecvFunc _RecvFunc = null;
            bool _Sending = false;
            bool _Connected = false;
            Int32 _ConnectTime = 0;
            Int32 _ConnectTimeOut = 0; // Overflow 때문에 _ConnectEndTime 이 아닌 TimeOutSpan 으로 처리
            bool _Connecting = false;
            SHeader _RecvHeader = null;
            byte[] _aRecvBuf = new byte[40960];
            CStream _StreamRcv = new CStream();
            CStream _StreamSnd = new CStream();
            CCrypto _Crypto = new CCrypto();
            TPacketSeq _SendPacketSeq = 0;
            TPacketSeq _RecvPacketSeq = 0;
            int _StreamSndTail = 0;     // 전 패킷에 붙여보내기 위한 인덱스

            bool _IsValid(SKey Key_)
            {
                return (_Key != null && _Key.Equals(Key_));
            }
            bool _Recv()
            {
                try
                {
                    _RecvEvent.UserToken = _Key;
                    _RecvEvent.SetBuffer(_aRecvBuf, 0, _aRecvBuf.Length);

                    if (!_Socket.ReceiveAsync(_RecvEvent))
                        _Recved();
                }
                catch (Exception e)
                {
#if _PACKET_DEBUG
                    Console.WriteLine(e.ToString());
#endif
                    return false;
                }

                return true;
            }
            TPeerCnt _UnLinkCore()
            {
                if (_Key == null)
                    return TPeerCnt.MaxValue;

                if (_Socket != null)
                {
                    _Socket.Close();
                    _Socket = null;
                }
                _RecvHeader = null;
                _StreamSndTail = 0;
                _RecvPacketSeq = 0;
                _SendPacketSeq = 0;
                _StreamRcv.Clear();
                _StreamSnd.Clear();
                _LongIP = 0;
                _Sending = false;
                _Connected = false;
                _ConnectTime = 0;
                _ConnectTimeOut = 0;
                _Connecting = false;

                var PeerNum = _Key.PeerNum;
                _Key = null;

                return PeerNum;
            }
            void _UnLink(Enum NetState_)
            {
                var PeerNum = _UnLinkCore();
                if (PeerNum == TPeerCnt.MaxValue)
                    return;

                _UnLinkFunc(PeerNum, NetState_);
            }
            bool _SendBegin()  // 외부에서 Stream 에 한번에 담아 보낼 수 있으나, 복사를 한번 더 하게 되기 때문에 IProto 를 여러 파라미터로받아 내부에서 SendBegin, SendEnd 영역을 두어 보내는 방식으로 처리
            {
                if (_Socket == null)
                    return false;

                _Sending = _StreamSnd.Size > 0;
                _StreamSndTail = _StreamSnd.Tail;

                if (!(new SHeader(0, 0, 0).Pop(_StreamSnd)))
                    return false;

                return true;
            }
            bool _SendEnd()
            {
                ++_SendPacketSeq;

                byte[] Body = _StreamSnd.GetStreamAt(_StreamSndTail + c_HeaderSize);
                if (!_StreamSnd.SetAt(_StreamSndTail, Body.Length))
                    return false;

                if (!_StreamSnd.SetAt(_StreamSndTail + sizeof(TSize) + sizeof(TCheckSum), _SendPacketSeq))
                    return false;

                if (Body.Length > 0)
                {
                    var CheckSum = CBase.GetCheckSum(Body);
                    if (!_StreamSnd.SetAt(_StreamSndTail + sizeof(TSize), CheckSum))
                        return false;

                    _Crypto.Encode(Body, (0x1f3a49b72c8d5ef6 ^ (UInt64)Body.Length ^ CheckSum ^ _SendPacketSeq));
                    for (int i = 0; i < Body.Count(); ++i)
                    {
                        if (!_StreamSnd.SetAt(_StreamSndTail + c_HeaderSize + i, Body[i]))
                            return false;
                    }
                }

                if (!_Sending)
                    _SendCore();

                return true;
            }
            void _SendCore()
            {
                try
                {
                    byte[] SendBuf = _StreamSnd.GetStream();
                    _SendEvent.UserToken = _Key;
                    _SendEvent.SetBuffer(SendBuf, 0, SendBuf.Length);

                    if (!_Socket.SendAsync(_SendEvent))
                        _Sended();
                }
                catch
                {
                }
            }
            bool _Send()
            {
                try
                {
                    if (!_SendBegin())
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public void Dispose()
            {
                if (_Key == null)
                    return;

                if (_Connected)
                    _UnLink(ENetState.Normal);
                else
                    _UnLinkCore();
            }
            public CPeer(TimeSpan HBRcvDelay_, TimeSpan HBSndDelay_, TLinkFunc LinkFunc_, TLinkFailFunc LinkFailFunc_, TUnLinkFunc UnLinkFunc_, TRecvFunc RecvFunc_)
            {
                _LinkFunc = LinkFunc_;
                _LinkFailFunc = LinkFailFunc_;
                _UnLinkFunc = UnLinkFunc_;
                _RecvFunc = RecvFunc_;

                if (HBRcvDelay_.TotalSeconds > 0.0)
                    _HBRcvPeriod = new CPeriod(HBRcvDelay_ + new TimeSpan(0, 0, 0, 0, 3000));

                if (HBSndDelay_.TotalSeconds > 0.0)
                    _HBSndPeriod = new CPeriod(HBSndDelay_);
            }
            ~CPeer()
            {
                Dispose();
            }
            public void InitFunc(TLinkFunc LinkFunc_, TRecvFunc RecvFunc_)
            {
                _LinkFunc = LinkFunc_;
                _RecvFunc = RecvFunc_;
            }
            public bool Connected { get { return _Connected; } }
            // 유저 호출
            public bool Link(Socket Socket_, SIPPort IPPort_, SKey Key_, TEventHandler RecvEventHandler_, Int32 ConnectTimeOut_)
            {
                if (_Socket != null)
                    return false;

                _RecvEvent = new SocketAsyncEventArgs();
                _RecvEvent.Completed += new EventHandler<SocketAsyncEventArgs>(RecvEventHandler_);
                _SendEvent = new SocketAsyncEventArgs();
                _SendEvent.Completed += new EventHandler<SocketAsyncEventArgs>(RecvEventHandler_);

                _Socket = Socket_;

                _RecvEvent.UserToken = Key_;
                _RecvEvent.RemoteEndPoint = IPPort_.EndPoint;

                _Key = Key_;
                _LongIP = IPPort_.IP;

                try
                {
                    if (!_Socket.ConnectAsync(_RecvEvent))  // _Worker 로 통보받을 필요 없음.
                    {
                        _HBRcvPeriod.NextLoose();
                        return _Linked();
                    }
                    else
                    {
                        _Connecting = true;
                        _ConnectTime = Environment.TickCount;
                        _ConnectTimeOut = ConnectTimeOut_;
                    }
                }
                catch
                {
                    _LinkFailed();
                    return false;
                }

                return true;
            }
            bool _Linked()
            {
                _Connected = true;
                _Connecting = false;
                _LinkFunc(_Key.PeerNum, _LongIP);

                if (!_Recv())
                {
                    _UnLink(ENetState.Normal);
                    return false;
                }

                return true;
            }
            public void Linked(SKey Key_)
            {
                if (!_IsValid(Key_))
                    return;

                _Linked();
            }
            void _LinkFailed()
            {
                _Connecting = false;
                var PeerNum = _UnLinkCore();
                if (PeerNum == TPeerCnt.MaxValue)
                    return;

                _LinkFailFunc(PeerNum, 0);
            }
            public void LinkFailed(SKey Key_)
            {
                if (!Key_.Equals(_Key))
                    return;

                _LinkFailed();
            }
            public void UnLink(SKey Key_, Enum NetState_) // Socket 콜백
            {
                if (!_IsValid(Key_))
                    return;

                _UnLink(NetState_);
            }
            public void Close(Enum NetState_) // 유저 호출
            {
                if (_Socket == null)
                    return;

                _Socket.Close();
                _Socket = null;

                _UnLink(NetState_);
            }
            void _Recved() // 수신 처리
            {
                if (_RecvEvent.SocketError != SocketError.Success)
                {
                    _UnLink(ENetState.Normal);
                    return;
                }

                if (_RecvEvent.BytesTransferred == 0)
                {
                    _UnLink(ENetState.Normal);
                    return;
                }

                _HBRcvPeriod.NextLoose();

                if (!_StreamRcv.Push(_aRecvBuf.SubArray(0, _RecvEvent.BytesTransferred)))
                {
                    _UnLink(ENetState.Normal);
                    return;
                }

                while (true)
                {
                    // For Header /////////////////////
                    if (_RecvHeader == null)
                    {
                        if (_StreamRcv.Size < c_HeaderSize)
                            break;

                        try
                        {
                            _RecvHeader = new SHeader();
                        }
                        catch
                        {
                            _UnLink(ENetState.Normal);
                            return;
                        }

                        if (!_StreamRcv.Pop(_RecvHeader))
                        {
                            _UnLink(ENetState.Normal);
                            return;
                        }
                    }

                    // For Body /////////////////////
                    if (_StreamRcv.Size < _RecvHeader.Size)
                        break;

                    if (_RecvHeader.Size > 0)
                    {
                        if (_RecvHeader.PacketSeq <= _RecvPacketSeq &&
                            (_RecvPacketSeq - _RecvHeader.PacketSeq) < (TPacketSeq.MaxValue << 1))
                        {
                            _UnLink(ENetState.Normal);
                            return;
                        }
                        _RecvPacketSeq = _RecvHeader.PacketSeq;

                        var Body = _StreamRcv.GetStream(_RecvHeader.Size);
                        _Crypto.Decode(Body, (0x1f3a49b72c8d5ef6 ^ (UInt64)_RecvHeader.Size ^ _RecvHeader.CheckSum ^ _RecvHeader.PacketSeq));
                        for (int i = 0; i < Body.Count(); ++i)
                            _StreamRcv.SetAt(_StreamRcv.Head + i, Body[i]);

                        if (_RecvHeader.CheckSum != CBase.GetCheckSum(Body))
                        {
                            _UnLink(ENetState.Normal);
                            return;
                        }

                        var OldSize = _StreamRcv.Size;
                        var OldHead = _StreamRcv.Head;
                        var OldTail = _StreamRcv.Tail;
                        _StreamRcv.Tail = _StreamRcv.Head + _RecvHeader.Size;	// For Notifying Received Size to RecvFunc

                        _RecvFunc(_Key.PeerNum, _StreamRcv);

                        if (_Key == null)
                            return;

                        // 외부에서 모두 Pop하지 않았거나 Stream에 대해서 Clear 하지 않아도 상관 없으므로 에러 체크하지 않음.
                        //if (_StreamRcv.Size != OldSize - _RecvHeader.Size)
                        //    throw new Exception("Did Not Receive Whole Protocol");

                        if (OldSize > _RecvHeader.Size)
                        {
                            _StreamRcv.Head = OldHead + _RecvHeader.Size;
                            _StreamRcv.Tail = OldTail;
                        }
                        else
                        {
                            _StreamRcv.Clear();
                        }
                    }

                    _RecvHeader = null;
                }

                if (_Socket == null) 
                    return;

                if (!_Recv())
                {
                    _UnLink(ENetState.Normal);
                    return;
                }
            }
            public void Recved(SKey Key_) // Socket 콜백 처리
            {
                if (!_IsValid(Key_))
                    return;

                _Recved();
            }
            bool _Sended()
            {
                if (_SendEvent.SocketError != SocketError.Success)
                {
                    _UnLink(ENetState.Normal);
                    return false;
                }

                if (_SendEvent.BytesTransferred == 0)
                {
                    _UnLink(ENetState.Normal);
                    return false;
                }

                _StreamSnd.PopStream(_SendEvent.BytesTransferred);

                if (_HBSndPeriod != null)
                    _HBSndPeriod.NextLoose();

                return true;
            }
            public void Sended(SKey Key_)
            {
                if (!_IsValid(Key_))
                    return;

                if (_Sended())
                    if (_StreamSnd.Size > 0)
                        _SendCore();
            }
            public bool Send(CStream Stream_)
            {
                try
                {
                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Stream_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(IProto Proto_)
            {
                try
                {
                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Proto_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(IProto Proto_, IProto Proto2_)
            {
                try
                {
                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Proto_))
                        throw new Exception();

                    if (!_StreamSnd.Push(Proto2_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(IProto Proto_, IProto Proto2_, IProto Proto3_)
            {
                try
                {
                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Proto_))
                        throw new Exception();

                    if (!_StreamSnd.Push(Proto2_))
                        throw new Exception();

                    if (!_StreamSnd.Push(Proto3_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(IProto Proto_, IProto Proto2_, Int32 Proto3_, IProto Proto4_)
            {
                try
                {
                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Proto_))
                        throw new Exception();

                    if (!_StreamSnd.Push(Proto2_))
                        throw new Exception();

                    if (!_StreamSnd.Push(Proto3_))
                        throw new Exception();

                    if (!_StreamSnd.Push(Proto4_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(Boolean Data_)
            {
                try
                {

                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Data_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(SByte Data_)
            {
                try
                {

                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Data_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(Byte Data_)
            {
                try
                {

                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Data_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(Int16 Data_)
            {
                try
                {

                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Data_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(UInt16 Data_)
            {
                try
                {

                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Data_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(Int32 Data_)
            {
                try
                {

                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Data_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(UInt32 Data_)
            {
                try
                {

                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Data_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(Int64 Data_)
            {
                try
                {

                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Data_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(UInt64 Data_)
            {
                try
                {

                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Data_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(float Data_)
            {
                try
                {

                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Data_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(double Data_)
            {
                try
                {

                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Data_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(String Data_)
            {
                try
                {

                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Data_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public bool Send(DateTime Data_)
            {
                try
                {

                    if (!_SendBegin())
                        throw new Exception();

                    if (!_StreamSnd.Push(Data_))
                        throw new Exception();

                    if (!_SendEnd())
                        throw new Exception();

                    return true;
                }
                catch
                {
                    _StreamSnd.Tail = _StreamSndTail;
                    return false;
                }
            }
            public void Proc()
            {
                if (_Socket == null)
                    return;

                if (_Socket.Connected)
                {
                    if (_HBRcvPeriod != null)
                    {
                        if (_HBRcvPeriod.CheckAndNextLoose())
                            _UnLink(ENetState.Normal);
                    }

                    if (_HBSndPeriod != null)
                    {
                        if (_HBSndPeriod.CheckAndNextLoose())
                            _Send();
                    }
                }

                // Connecting Check
                if (_Socket != null)
                {
                    if (!_Socket.Connected &&
                        _Connecting &&
                        (Environment.TickCount - _ConnectTime) > _ConnectTimeOut)
                    {
                        _LinkFailed();
                    }
                }
            }
        }
    }
}