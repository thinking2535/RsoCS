using rso.net;

namespace rso
{
    namespace patch
    {
        using core;
        using System;
        using TPeerCnt = System.UInt32;
        using TUpdateFiles = System.Collections.Generic.Dictionary<System.String, System.Boolean>;
        public delegate void TDataFunc(SPatchData Data_);
        public class CAgent : CClientCore // Agent는 분산된 데이터가 아닌 오리지날 데이터를 받아야 하기 때문에 CClient를 상속받지 않음.
        {
            CNamePort _MasterNamePort;
            string _ID;
            string _PW;
            TLinkFunc _LinkFunc;
            TLinkFailFunc _LinkFailFunc;
            TUnLinkFunc _UnLinkFunc;
            TDataFunc _DataFunc;
            net.CClient _NetM;

            void _LinkM(CKey Key_)
            {
                _NetM.Send(Key_.PeerNum, new SHeader(EProto.AmLogin), new SAmLogin(_ID, _PW, _Data.Data.Version));
                _LinkFunc(Key_);
            }
            void _LinkFailM(TPeerCnt PeerNum_, ENetRet NetRet_)
            {
                _LinkFailFunc(PeerNum_, NetRet_);
            }
            void _UnLinkM(CKey Key_, ENetRet NetRet_)
            {
                _UnLinkFunc(Key_, NetRet_);
            }
            void _RecvM(CKey Key_, CStream Stream_)
            {
                var Header = new SHeader();
                Stream_.Pop(ref Header);

                try
                {
                    switch (Header.Proto)
                    {
                        case EProto.MaPatchData:
                            _RecvMaPatchData(Key_, Stream_);
                            return;
                        default:
                            throw new Exception();
                    }
                }
                catch
                {
                    _NetM.Close(Key_.PeerNum);
                }
            }
            void _RecvMaPatchData(CKey Key_, CStream Stream_)
            {
                var Proto = new SPatchData();
                Stream_.Pop(ref Proto);

                _PatchCore(Proto);
                _DataFunc(Proto);
            }

            public CAgent(
                string FileName_, string DataPath_,
                CNamePort MasterNamePort_, TLinkFunc LinkFunc_, TLinkFailFunc LinkFailFunc_, TUnLinkFunc UnLinkFunc_,
                TDataFunc DataFunc_) :
                base(FileName_, DataPath_)
            {
                _MasterNamePort = MasterNamePort_;
                _LinkFunc = LinkFunc_;
                _LinkFailFunc = LinkFailFunc_;
                _UnLinkFunc = UnLinkFunc_;
                _DataFunc = DataFunc_;
                _NetM = new net.CClient(
                    _LinkM, _LinkFailM, _UnLinkM, _RecvM,
                    false, 1024000, 1024000,
                    TimeSpan.FromMilliseconds(30000), TimeSpan.FromMilliseconds(20000), 60);
            }
            void _SetAccount(string ID_, string PW_)
            {
                _ID = ID_;
                _PW = PW_;
            }
            public CKey Connect(string ID_, string PW_, TPeerCnt PeerNum_)
            {
                _SetAccount(ID_, PW_);
                return _NetM.Connect(_MasterNamePort, PeerNum_);
            }
            public CKey Connect(string ID_, string PW_)
            {
                _SetAccount(ID_, PW_);
                return _NetM.Connect(_MasterNamePort);
            }
            public void Proc()
            {
                _NetM.Proc();
            }
            public void Close()
            {
                _NetM.CloseAll();
            }
            public void Update(bool IsReset_, TUpdateFiles Files_)
            {
                _NetM.SendAll(new SHeader(EProto.AmUpdate), new SUpdateData(IsReset_, Files_));
            }
        }
    }
}
