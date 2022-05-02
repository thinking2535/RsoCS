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
        using TPeerCnt = UInt32;
        using TLongIP = UInt32;
        using TPort = UInt16;
        using TSize = Int32;
        using TCheckSum = UInt64;

        public class CNet
        {
            enum _EAsyncOp
            {
                Link,
                LinkFail,
                UnLink,
                Recv,
                Send,
                Max,
                Null
            }
            class _SLFIOCP
            {
                public SKey Key = null;
                public _EAsyncOp Op = _EAsyncOp.Null;
            }

            CPeriod _ProcPeriod = new CPeriod(new TimeSpan(1000));
            CPeerMgr _PeerMgr = new CPeerMgr();
            CPeer[] _Peers = null;
            CLFQueue<_SLFIOCP> _LFIOCPs = new CLFQueue<_SLFIOCP>();
            bool _NoDelay = false;
            TLinkFailFunc _LinkFailFunc = null;
            TUnLinkFunc _UnLinkFunc = null;

            void _LinkFail(TPeerCnt PeerNum_, Int32 ErrorCode_)
            {
                _PeerMgr.UnLink(PeerNum_);
                _LinkFailFunc(PeerNum_, ErrorCode_);
            }
            void _UnLink(TPeerCnt PeerNum_, Enum NetState_)
            {
                _PeerMgr.UnLink(PeerNum_);
                _UnLinkFunc(PeerNum_, NetState_);
            }
            void _Worker(object Sender, SocketAsyncEventArgs Event_)
            {
                _SLFIOCP pLFIOCP = null;
                while ((pLFIOCP = _LFIOCPs.GetPushBuf()) == null)
                    Thread.Sleep(3);

                pLFIOCP.Key = (SKey)Event_.UserToken;
                switch (Event_.LastOperation)
                {
                    case SocketAsyncOperation.Connect:
                        if (Event_.SocketError == SocketError.Success)
                            pLFIOCP.Op = _EAsyncOp.Link;
                        else
                            pLFIOCP.Op = _EAsyncOp.LinkFail;
                        break;

                    case SocketAsyncOperation.Disconnect:
                        pLFIOCP.Op = _EAsyncOp.UnLink;
                        break;

                    case SocketAsyncOperation.Receive:
                        pLFIOCP.Op = _EAsyncOp.Recv;
                        break;

                    case SocketAsyncOperation.Send:
                        pLFIOCP.Op = _EAsyncOp.Send;
                        break;

                    default:
                        throw new Exception("Invalid Operation");
                }

                _LFIOCPs.Push();
            }
            public void Dispose()
            {
                if (_Peers != null)
                {
                    foreach (var Peer in _Peers)
                    {
                        if (Peer != null)
                            Peer.Dispose();
                    }
                }
            }
            public CNet(TPeerCnt PeerCnt_, TimeSpan HBRcvDelay_, TimeSpan HBSndDelay_,
                TLinkFunc LinkFunc_, TLinkFailFunc LinkFailFunc_, TUnLinkFunc UnLinkFunc_, TRecvFunc RecvFunc_, bool NoDelay_)
            {
                _LinkFailFunc = LinkFailFunc_;
                _UnLinkFunc = UnLinkFunc_;

                _Peers = new CPeer[PeerCnt_];
                for (Int32 i = 0; i < _Peers.Length; ++i)
                    _Peers[i] = new CPeer(HBRcvDelay_, HBSndDelay_, LinkFunc_, _LinkFail, _UnLink, RecvFunc_);

                _NoDelay = NoDelay_;
            }
            ~CNet()
            {
                Dispose();
            }
            public void InitFunc(TLinkFunc LinkFunc_, TLinkFailFunc LinkFailFunc_, TUnLinkFunc UnLinkFunc_, TRecvFunc RecvFunc_)
            {
                _LinkFailFunc = LinkFailFunc_;
                _UnLinkFunc = UnLinkFunc_;

                foreach (var Peer in _Peers)
                    Peer.InitFunc(LinkFunc_, RecvFunc_);
            }
            public bool Connected(TPeerCnt PeerNum_)
            {
                return _Peers[PeerNum_].Connected;
            }
            public bool Link(TPeerCnt PeerNum_, SIPPort IPPort_, Int32 ConnectTimeOut_)
            {
                try
                {
                    var Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    Socket.NoDelay = _NoDelay;

                    SKey Key = _PeerMgr.Link(PeerNum_);
                    if (Key == null)
                        return false;

                    if (!_Peers[Key.PeerNum].Link(Socket, IPPort_, Key, _Worker, ConnectTimeOut_))
                    {
                        _PeerMgr.UnLink(Key.PeerNum);
                        return false;
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            // 아래 같은 방법밖에 없나? 제네릭을 쓸 수는 있나?
            public void Close(TPeerCnt PeerNum_, Enum NetState_)
            {
                _Peers[PeerNum_].Close(NetState_);
            }
            public bool Send(TPeerCnt PeerNum_, CStream Stream_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Stream_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Stream_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, IProto Proto_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Proto_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Proto_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, IProto Proto_, IProto Proto2_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Proto_, Proto2_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Proto_, Proto2_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, IProto Proto_, IProto Proto2_, IProto Proto3_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Proto_, Proto2_, Proto3_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Proto_, Proto2_, Proto3_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, IProto Proto_, IProto Proto2_, Int32 Proto3_, IProto Proto4_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Proto_, Proto2_, Proto3_, Proto4_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Proto_, Proto2_, Proto3_, Proto4_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, Boolean Data_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Data_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Data_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, SByte Data_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Data_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Data_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, Byte Data_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Data_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Data_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, Int16 Data_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Data_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Data_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, UInt16 Data_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Data_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Data_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, Int32 Data_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Data_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Data_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, UInt32 Data_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Data_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Data_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, Int64 Data_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Data_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Data_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, UInt64 Data_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Data_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Data_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, float Data_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Data_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Data_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, double Data_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Data_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Data_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, String Data_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Data_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Data_);
                }
            }
            public bool Send(TPeerCnt PeerNum_, DateTime Data_)
            {
                if (PeerNum_ == TPeerCnt.MaxValue)
                {
                    for (TPeerCnt PeerNum = 0; PeerNum < _Peers.Length; ++PeerNum)
                        _Peers[(Int32)PeerNum].Send(Data_);

                    return true;
                }
                else
                {
                    return _Peers[PeerNum_].Send(Data_);
                }
            }
            public TPeerCnt MaxPeerCnt { get { return (TPeerCnt)_Peers.Length; } }
            public void Proc()
            {
                for (var pLFIOCP = _LFIOCPs.GetPopBuf();
                    pLFIOCP != null;
                    pLFIOCP = _LFIOCPs.GetPopBuf())
                {
                    var Peer = _Peers[pLFIOCP.Key.PeerNum];

                    switch (pLFIOCP.Op)
                    {
                        case _EAsyncOp.Link:
                            Peer.Linked(pLFIOCP.Key);
                            break;
                        case _EAsyncOp.LinkFail:
                            Peer.LinkFailed(pLFIOCP.Key);
                            break;
                        case _EAsyncOp.UnLink:
                            Peer.UnLink(pLFIOCP.Key, ENetState.Normal);
                            break;
                        case _EAsyncOp.Recv:
                            Peer.Recved(pLFIOCP.Key);
                            break;
                        case _EAsyncOp.Send:
                            Peer.Sended(pLFIOCP.Key);
                            break;
                        default:
                            break;
                    }

                    _LFIOCPs.Pop();
                }

                if (_ProcPeriod.CheckAndNextLoose())
                {
                    foreach(var Peer in _Peers)
                        Peer.Proc();
                }
            }
        }
    }
}