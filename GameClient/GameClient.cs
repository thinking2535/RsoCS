using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using rso.Base;
using rso.physics;
using rso.core;
using rso.net;
using rso.game;
using System.IO;
using rso.gameutil;
using System.Reflection;

namespace GameClientTest
{
    using TPeerCnt = UInt32;
    using TUID = Int64;
    using TFriends = System.Collections.Generic.Dictionary<Int64, rso.game.SFriend>;
    using TNick = System.String;

    public partial class GameClientTest : Form
    {
        struct SClient
        {
            TPeerCnt PeerNum;
            public IFSM FSM;
            public CClient Client;

            public SClient(TPeerCnt PeerNum_)
            {
                PeerNum = PeerNum_;
                FSM = new CFSM(2000);
                Client = null;
            }

            public void Initialize()
            {
                FSM.Set(AutoUnLinked);
            }
            public void Proc()
            {
                FSM.Proc();
            }
            public void Login(string ID_, string PW_, TUID UID_)
            {
                Client = new CClient(ID_, PW_, UID_);
            }
            public void LoginFail()
            {
                FSM.Set(AutoUnLinked);
            }
            public void Logout()
            {
                Client = null;
                FSM.Set(AutoUnLinked);
            }
            void AutoUnLinked()
            {
            }
        }

        SClient[] _Clients;
        static rso.game.CClient _Net = null;
        CClientBinder _Binder = null;
        readonly Int32 ServerNo = 0;
        readonly string DataPath;
        readonly String IP;
        readonly UInt16 Port;
        readonly TPeerCnt StartNum = 0;
        readonly Int32 ClientCnt = 1;
        String ID;
        String PW;
        readonly bool _Auto = false;
        readonly System.Drawing.Graphics _Graphics;
        void Link(CKey Key_, TUID UID_, string Nick_, TFriends Friends_)
        {
            _Clients[Key_.PeerNum].Login(ID + (StartNum + Key_.PeerNum).ToString(), PW, UID_);

            if (!_Auto)
            {
                btnDisconnect.Enabled = true;
            }

            _Log("Login PeerNum : " + Key_.PeerNum + " UID : " + UID_);
        }
        void LinkFail(TPeerCnt PeerNum_, EGameRet GameRet_)
        {
            _Clients[PeerNum_].LoginFail();

            if (!_Auto)
            {
                btnLoginUID.Enabled = true;
                btnDisconnect.Enabled = false;
            }

            _Log("LoginFail:" + PeerNum_ + " GameRet:" + GameRet_.ToString());
        }
        void UnLink(CKey Key_, EGameRet GameRet_)
        {
            _Clients[Key_.PeerNum].Logout();

            if (!_Auto)
            {
                AcceptButton = btnLoginUID;
                btnLoginUID.Enabled = true;
                btnDisconnect.Enabled = false;
            }

            _Log("Logout GameRet:" + GameRet_.ToString());
        }
        void Recv(CKey Key_, CStream Stream_)
        {
            Int32 ProtoNum = 0;
            Stream_.Pop(ref ProtoNum);

            _Binder.Recv(Key_, ProtoNum, Stream_);
            _Log("RecvCallback");
        }
        void Error(TPeerCnt PeerNum_, EGameRet GameRet_)
        {
            _Log("Error:" + GameRet_.ToString());
        }
        void Check(TUID UID_, CStream Stream_)
        {
            _Log("Check");
        }
        void FriendAdded(TPeerCnt PeerNum_, TUID UID_, SFriend Friend_)
        {
            _Log("FriendAdded");
        }
        void FriendRequested(TPeerCnt PeerNum_, TUID UID_, TNick Nick_)
        {
            _Log("FriendRequested");
        }
        void FriendAllowed(TPeerCnt PeerNum_, TUID FriendUID_)
        {
            _Log("FriendAllowed");
        }
        void FriendDenyed(TPeerCnt PeerNum_, TUID FriendUID_)
        {
            _Log("FriendDenyed");
        }
        void FriendBlocked(TPeerCnt PeerNum_, TUID FriendUID_)
        {
            _Log("FriendBlocked");
        }
        void FriendUnBlocked(TPeerCnt PeerNum_, TUID FriendUID_)
        {
            _Log("FriendUnBlocked");
        }
        void StateChanged(TPeerCnt PeerNum_, Byte State_)
        {
            _Log("StateChanged");
        }
        void FriendStateChanged(TPeerCnt PeerNum_, TUID FriendUID_, Byte State_)
        {
            _Log("FriendStateChanged");
        }
        void MessageReceived(TPeerCnt PeerNum_, TUID FriendUID_, String Message_)
        {
            _Log("MessageReceived");
        }

        public GameClientTest()
        {
            InitializeComponent();

            try
            {
                var SetupJson = (JsonDataObject)JsonParser.Parse(File.ReadAllText("Option.ini"));

                _Auto = SetupJson["Auto"].GetBool();
                ServerNo = SetupJson["ServerNo"].GetInt32();
                var ServerInfo = ((JsonDataObject)(((JsonDataArray)SetupJson["Servers"])[ServerNo]));

                StartNum = ServerInfo["StartNum"].GetUInt32();
                ClientCnt = ServerInfo["ClientCnt"].GetInt32();
                if (ClientCnt <= 0)
                    throw new Exception("Invalid ClientCnt");

                DataPath = ServerInfo["DataPath"].GetString();
                IP = ServerInfo["IP"].GetString();
                Port = ServerInfo["Port"].GetUInt16();

                ID = ServerInfo["ID"].GetString();
                PW = ServerInfo["PW"].GetString();

                TimerNet.Enabled = true;

                if (_Auto)
                {
                    btnLoginUID.Enabled = false;
                }

                btnDisconnect.Enabled = false;

                _Net = new rso.game.CClient(new SVersion(1, 0));
                if (_Net == null)
                    return;

                _Net.LinkFunc = Link;
                _Net.LinkFailFunc = LinkFail;
                _Net.UnLinkFunc = UnLink;
                _Net.RecvFunc = Recv;
                _Net.ErrorFunc = Error;
                _Net.CheckFunc = Check;
                _Net.FriendAddedFunc = FriendAdded;

                _Net.FriendRequestedFunc = FriendRequested;
                _Net.FriendAllowedFunc = FriendAllowed;
                _Net.FriendDenyedFunc = FriendDenyed;
                _Net.FriendBlockedFunc = FriendBlocked;
                _Net.FriendUnBlockedFunc = FriendUnBlocked;
                _Net.StateChangedFunc = StateChanged;
                _Net.FriendStateChangedFunc = FriendStateChanged;
                _Net.MessageReceivedFunc = MessageReceived;

                _Binder = new CClientBinder(_Net);

                _Clients = new SClient[ClientCnt];

                for (TPeerCnt i = 0; i < ClientCnt; ++i)
                {
                    _Clients[i] = new SClient(i);
                    _Clients[i].Initialize();
                }

                //_Binder.AddSendProto<SChatNetCs>((Int32)EProtoNetCs.Chat);
                //_Binder.AddSendProto<SFCMTokenSetNetCs>((Int32)EProtoNetCs.FCMTokenSet);
                //_Binder.AddSendProto<SFCMTokenUnsetNetCs>((Int32)EProtoNetCs.FCMTokenUnset);
                //_Binder.AddSendProto<SFCMCanPushAtNightNetCs>((Int32)EProtoNetCs.FCMCanPushAtNight);
                //_Binder.AddSendProto<SPurchaseNetCs>((Int32)EProtoNetCs.Purchase);
                //_Binder.AddSendProto<SChangeNickNetCs>((Int32)EProtoNetCs.ChangeNick);

                //_Binder.AddRecvProto((Int32)EProtoNetSc.Ret, RetNetSc);
                //_Binder.AddRecvProto((Int32)EProtoNetSc.Msg, MsgNetSc);
                //_Binder.AddRecvProto((Int32)EProtoNetSc.Login, LoginNetSc);
                //_Binder.AddRecvProto((Int32)EProtoNetSc.Lobby, LobbyNetSc);
                //_Binder.AddRecvProto((Int32)EProtoNetSc.Chat, ChatNetSc);
                //_Binder.AddRecvProto((Int32)EProtoNetSc.Purchase, PurchaseNetSc);
                //_Binder.AddRecvProto((Int32)EProtoNetSc.ChangeNick, ChangeNickNetSc);
                //_Binder.AddRecvProto((Int32)EProtoNetSc.ChangeNickFail, ChangeNickFailNetSc);

                TimerNet.Enabled = true;
                _Graphics = this.CreateGraphics();
            }
            catch (Exception Exception_)
            {
                _Log(Exception_.ToString());
                return;
            }
        }
        public void _Log(String Msg_)
        {
            txtLog.AppendText(Msg_.Replace("\n", "\r\n") + "\r\n");
        }
        void _Draw(CChar Player_, double Time_)
        {
            //_Graphics.DrawLine(Pens.Red, new Point(0, 1000), new Point(1000, 1000));

            Player_.Proc(Time_);
            // ggg
            //var PosTheta = Player_.GetPosTheta(Time_);
            //_Graphics.DrawEllipse(System.Drawing.Pens.Black, new Rectangle((Int32)(PosTheta.Pos.X - 15.0), (Int32)(PosTheta.Pos.Y - 15.0), (Int32)(30.0), (Int32)(30.0)));
        }
        public void UnLinked_Auto(TPeerCnt PeerNum_)
        {
            String NumberedID = ID + (StartNum + PeerNum_).ToString();

            //if (_ClientInfos[Key_.PeerNum].JoinMode)
            //{
            //    if (_Net.Join(new SCsJoin(new SCsLogin(new SAccount(NumberedID, PW), new SVersion(global::Boat.global.c_Ver_Main, 0)), NumberedID, 1), Key_.PeerNum))
            //    {
            //        _Log("Join " + Key_.PeerNum);
            //    }
            //    else
            //    {
            //        _Log("JoinFail " + Key_.PeerNum);
            //    }
            //}
            //else
            //{
            //    if (_Net.Login(new SAccount(NumberedID, PW), new SVersion(global::Boat.global.c_Ver_Main, 0), Key_.PeerNum))
            //    {
            //        _Log("Login " + Key_.PeerNum);
            //    }
            //    else
            //    {
            //        _Log("LoginFail " + Key_.PeerNum);
            //    }
            //}
        }
        public void Linked_Auto(TPeerCnt PeerNum_)
        {
            //if (_ClientInfos[Key_.PeerNum].ReLogin)
            //{
            //}
            //else
            //{
            //    _Net.Send(new SCsLoginInfo(), Key_);
            //    _Log("LoginInfo " + Key_.PeerNum);
            //}
        }
        public void LoginInfoGot_Auto(TPeerCnt PeerNum_)
        {
            //if (_ClientInfos[Key_.PeerNum].ReLogin)
            //{
            //}
            //else
            //{
            //    MatchStart_Auto(Key_);
            //}
        }
        public void RetNetSc(SKey Key_, CStream Stream_)
        {
//            var Proto = new SRetNetSc();
//            Proto.Push(Stream_);

//            if (_Auto)
//            {
////                _Log("ScFail CsProto:" + Proto.CsProto.ToString() + " ErrorNo:" + Proto.ErrorNo + " Msg:" + Proto.Msg);
//            }
//            else
//            {
//                _Log("RetNetSc");
////                _Log("ScFail CsProto:" + Proto.CsProto.ToString() + " ErrorNo:" + Proto.ErrorNo + " Msg:" + Proto.Msg);
//            }
        }
        public void MsgNetSc(SKey Key_, CStream Stream_)
        {
        }
        public void LoginNetSc(SKey Key_, CStream Stream_)
        {
        }
        public void LobbyNetSc(SKey Key_, CStream Stream_)
        {
        }
        public void ChatNetSc(SKey Key_, CStream Stream_)
        {
        }
        public void PurchaseNetSc(SKey Key_, CStream Stream_)
        {
        }
        public void ChangeNickNetSc(SKey Key_, CStream Stream_)
        {
        }
        public void ChangeNickFailNetSc(SKey Key_, CStream Stream_)
        {
        }
        private void TimerNet_Tick(object sender, EventArgs e)
        {
            if (_Net != null)
                _Net.Proc();

//            var Time = _GetTime();
            if (_Auto)
            {
                foreach (var i in _Clients)
                    i.Proc();
            }
            else
            {
                //var Begin = _Clients.Begin();
                //if (Begin)
                //{
                //    var Client = Begin.Data;

                //    if (Client.IsInRoom)
                //    {
                //        foreach (var Player in Client.Players)
                //            _Draw(Player.Value, Time);

                //        //if (++_TickCounter % 10 == 0)
                //        //    Invalidate();
                //    }
                //}
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.A)
            //{
            //    _ADown = false;
            //}
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (_Auto)
            //    return;

            //if (_Net == null)
            //    return;

            //var Client = _Clients.First();
            //if (Client == null)
            //    return;

            //if (!Client.IsPlaying)
            //    return;

            //if (e.KeyCode == Keys.A)
            //{
            //    if (_ADown)
            //        return;

            //    _ADown = true;
            //    // ggg
            //    //var Player = _Players[_Clients[0].UID];
            //    //Player.SetSpeed(0.0, _GetTime());

            //    _Net.Send(new SCsGameAttack());
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_Net.Login(0, DataPath, new CNamePort(IP, Port), tbID.Text, 0, new CStream()))
            {
                _Log("Login Fail");
                return;
            }

            btnLoginUID.Enabled = false;
            btnDisconnect.Enabled = true;

            TimerNet.Enabled = true;
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            _Net.Logout();
            btnDisconnect.Enabled = false;
        }

        private void GalaxyClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_Net != null)
            {
                _Net.Dispose();
                _Net = null;
            }
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            //_Net.Send(new SGameProtoNetCs(true, 1, 2, 3, "ok", "크크", new DateTime(), new rso.core.TimePoint(100)));
            //var stm = new CStream();
            //stm.Push(new SUserCreateOption(new SUserLoginOption(EOS.Android)));
            //_Net.Create(0, "Data/", new CNamePort("127.0.0.1", 30130), "IDID", "NickNick", 0, 0, stm);
            //_Net.Create("Data/", new CNamePort("121.137.228.80", 30130), "IDID", "NickNick", 0, stm);
        }
        private void GalaxyClient_MouseDown(object sender, MouseEventArgs e)
        {
            if (_Auto)
                return;

            if (_Net == null)
                return;

            //var Client = _Clients.First();
            //if (Client == null)
            //    return;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        // ggg
                        //var Time = _GetTime();
                        //var Player = _Players[_Clients[0].UID];
                        //var Pos = Player.GetPos(Time);
                        //Player.ChangeMove(Math.Atan2(e.Y - Pos.Y, e.X - Pos.X), Time);


                        //_Net.Send(new SCsGameChangeMove(Player));
                    }
                    break;
                case MouseButtons.Right:
                    {
                        // ggg
                        //var Time = _GetTime();
                        //var Player = _Players[_Clients[0].UID];
                        //var Pos = Player.GetPos(Time);


                        //if(Player.ChangeTheta(Math.Atan2(e.Y - Pos.Y, e.X - Pos.X)))
                        //    _Net.Send(new SCsGameChangeTheta(Math.Atan2(e.Y - Pos.Y, e.X - Pos.X)));
                    }
                    break;
            }
        }

    }
}
