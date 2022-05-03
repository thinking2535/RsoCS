using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using rso.core;
using rso.net;
using rso.monitor;
using rso.util;

namespace MonitorClient
{
    using TUID = Int64;
    using TPeerCnt = UInt32;
    using TLongIP = UInt32;
    using System.IO;
    using System.Collections;
    using rso.gameutil;
    using rso.Base;

    public partial class MonitorClient : Form
    {
        enum EField
        {
            AgentNum,
            AgentName,
            AgentOption,
            AgentStat,
            ProcName,
            ProcStat,
            Max,
            Null
        }

        class _CAgentInfo
        {
            public CKey Key;
            public ListViewItem Item;
            public SAgentOption AgentOption;
            public JsonDataObject AgentStat;
            public JsonDataObject ProcStat;

            public _CAgentInfo(CKey Key_, ListViewItem Item_, SAgentOption AgentOption_, JsonDataObject AgentStat_, JsonDataObject ProcStat_)
            {
                Key = Key_;
                Item = Item_;
                AgentOption = AgentOption_;
                AgentStat = AgentStat_;
                ProcStat = ProcStat_;
            }
        }

        COptionJson<SOption> _Option;
        Dictionary<TPeerCnt, _CAgentInfo> _AgentInfos = new Dictionary<TPeerCnt, _CAgentInfo>();
        OpenFileDialog _OpenFileDialog = new OpenFileDialog();
        FolderBrowserDialog _FolderBrowserDialog = new FolderBrowserDialog();

        void _WriteSavedCommandJson()
        {
            _Option.Data.Commands.Clear();

            foreach (ListViewItem Item in lvSavedCommands.Items)
                _Option.Data.Commands.Add(new SCommand(Item.SubItems[0].Text, Item.SubItems[1].Text));

            _Option.Save();
        }
        void _AddCommandToListView(String Name_, String Command_)
        {
            var Item = lvSavedCommands.Items.Add(Name_);
            Item.SubItems.Add(Command_);
        }
        ListViewItem _AddItem(string Value_)
        {
            var Item = new ListViewItem(Value_);

            for (Int32 i = 1; i < (Int32)EField.Max; ++i)
                Item.SubItems.Add("");

            lvAgents.Items.Add(Item);

            return Item;
        }
        _CAgentInfo _AddAgent(CKey Key_, SAgent Agent_)
        {
            if (_AgentInfos.ContainsKey(Key_.PeerNum))
                return null;

            var Agent = _AgentInfos[Key_.PeerNum] = new _CAgentInfo(Key_, _AddItem(Key_.PeerNum.ToString("D5")), Agent_.Option, (JsonDataObject)JsonParser.Parse(Agent_.Stat), null);

            Agent.Item.SubItems[(Int32)EField.AgentName].Text = Agent_.Name;
            Agent.Item.SubItems[(Int32)EField.AgentOption].Text = Agent_.Option.ToJsonString();
            Agent.Item.SubItems[(Int32)EField.AgentStat].Text = Agent_.Stat;
            Agent.Item.UseItemStyleForSubItems = false;

            return Agent;
        }
        void _AddAgent(CKey Key_, SAgent Agent_, SProc Proc_)
        {
            var Agent = _AddAgent(Key_, Agent_);
            if (Agent == null)
                throw new Exception("AddAgent Fail");

            _SetProc(Agent, Proc_);
        }
        void _DelAgent(TPeerCnt PeerNum_)
        {
            var Agent = _AgentInfos[PeerNum_];
            lvAgents.Items.Remove(Agent.Item);
            _AgentInfos.Remove(PeerNum_);
        }
        void _SetProc(_CAgentInfo Agent_, SProc Proc_)
        {
            Agent_.ProcStat = (JsonDataObject)JsonParser.Parse(Proc_.Stat);
            Agent_.Item.SubItems[(Int32)EField.ProcName].Text = Proc_.Name;
            Agent_.Item.SubItems[(Int32)EField.ProcStat].Text = Proc_.Stat;

            lvAgents.Sort();
        }
        void _UnsetProc(_CAgentInfo Agent_)
        {
            Agent_.Item.SubItems[(Int32)EField.ProcName].Text = "";
            Agent_.Item.SubItems[(Int32)EField.ProcStat].Text = "";
            Agent_.Item.SubItems[(Int32)EField.ProcName].BackColor = Color.FromArgb(255, 255, 255, 255);

            lvAgents.Sort();
        }
        public void AddCommand(String Name_, String Command_)
        {
            _AddCommandToListView(Name_, Command_);
            _WriteSavedCommandJson();
        }
        void SubCommand(ListViewItem Item_)
        {
            lvSavedCommands.Items.Remove(Item_);
            _WriteSavedCommandJson();
        }
        public MonitorClient()
        {
            InitializeComponent();

            Int32[] Widths = new Int32[(Int32)EField.Max]
            {
                80,
                110,
                110,
                200,
                500,
                80
            };

            AgentFields = new System.Windows.Forms.ColumnHeader[(Int32)EField.Max];
            for (Int32 i = 0; i < (Int32)EField.Max; ++i)
            {
                AgentFields[i] = new System.Windows.Forms.ColumnHeader();
                AgentFields[i].Text = ((EField)i).ToString();
                AgentFields[i].Width = Widths[i];
            }

            lvAgents.Columns.AddRange(AgentFields);
            lvAgents.ContextMenuStrip = cmCommand;
            lvAgents.Location = new System.Drawing.Point(13, 39);
            lvAgents.Name = "lvAgents";
            lvAgents.Size = new System.Drawing.Size(989, 677);
            lvAgents.Sorting = System.Windows.Forms.SortOrder.Ascending;
            lvAgents.TabIndex = 6;
            lvAgents.UseCompatibleStateImageBehavior = false;
            lvAgents.View = System.Windows.Forms.View.Details;
            lvAgents.SelectedIndexChanged += new System.EventHandler(lvAgents_SelectedIndexChanged);
            lvAgents.FullRowSelect = true;

            lvAgents.ListViewItemSorter = new ListViewItemComparer(lvAgents.Sorting, 0);

            cmCommand.Enabled = false;

            try
            {
                _Option = new COptionJson<SOption>("Option.ini", true);

                var Server = _Option.Data.Servers[_Option.Data.ServerNo];

                foreach (var i in _Option.Data.Servers)
                    cbServer.Items.Add(i.ServerName);

                cbServer.SelectedIndex = _Option.Data.ServerNo;

                tbID.Text = _Option.Data.ID;
                tbPassword.Text = _Option.Data.PW;

                _FolderBrowserDialog.SelectedPath = _Option.Data.LocalDirectory;
                tbRemoteFileTransferDirectory.Text = _Option.Data.RemoteDirectory;
            }
            catch (Exception Exception_)
            {
                MessageBox.Show("[Option.ini 파일 에러] " + Exception_.Message);
                throw;
            }

            foreach (var i in _Option.Data.Commands)
                _AddCommandToListView(i.Name, i.Command);
        }
        _CAgentInfo GetAgent(ListViewItem Item_)
        {
            return _AgentInfos[TPeerCnt.Parse(Item_.Text)];
        }
        CKey GetKey(ListViewItem Item_)
        {
            return GetAgent(Item_).Key;
        }
        void Link(CKey Key_)
        {
        }
        void LinkFail(TPeerCnt PeerNum_, ENetRet NetRet_)
        {
            cbServer.Enabled = true;
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }
        void UnLink(CKey Key_, ENetRet NetRet_)
        {
            while (_AgentInfos.Count > 0)
                _DelAgent(_AgentInfos.First().Key);

            cbServer.Enabled = true;
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }
        void Recv(CKey Key_, EScProto ScProto_, CStream Stream_)
        {
            switch (ScProto_)
            {
                case EScProto.AgentOn:
                    {
                        var Proto = new SScAgentOn();
                        Proto.Push(Stream_);

                        _AddAgent(new CKey(Proto.Key), Proto);
                    }
                    break;

                case EScProto.AgentOff:
                    {
                        var Proto = new SScAgentOff();
                        Proto.Push(Stream_);

                        _DelAgent(Proto.Key.PeerNum);
                    }
                    break;

                case EScProto.ProcOn:
                    {
                        var Proto = new SScProcOn();
                        Proto.Push(Stream_);

                        _SetProc(_AgentInfos[Proto.Key.PeerNum], Proto.Proc);
                    }
                    break;

                case EScProto.ProcOff:
                    {
                        var Proto = new SScProcOff();
                        Proto.Push(Stream_);

                        _UnsetProc(_AgentInfos[Proto.Key.PeerNum]);
                    }
                    break;

                case EScProto.AgentInfos:
                    {
                        var Proto = new SScAgentInfos();
                        Proto.Push(Stream_);

                        foreach (var Info in Proto.Infos)
                        {
                            if (Info.Proc.Name == "")
                                _AddAgent(new CKey(Info.Key), Info.Agent);
                            else
                                _AddAgent(new CKey(Info.Key), Info.Agent, Info.Proc);
                        }
                    }
                    break;

                case EScProto.AgentOption:
                    {
                        var Proto = new SScAgentOption();
                        Proto.Push(Stream_);

                        var Agent = _AgentInfos[Proto.AgentKey.PeerNum];
                        Agent.AgentOption = Proto.Option;
                        Agent.Item.SubItems[(Int32)EField.AgentOption].Text = Agent.AgentOption.ToJsonString();
                    }
                    break;

                case EScProto.AgentStat:
                    {
                        var Proto = new SScAgentStat();
                        Proto.Push(Stream_);

                        var Agent = _AgentInfos[Proto.AgentKey.PeerNum];
                        Agent.AgentStat[Proto.KeyData.Key] = JsonParser.Parse(Proto.KeyData.Data);
                        Agent.Item.SubItems[(Int32)EField.AgentStat].Text = Agent.AgentStat.ToString();
                    }
                    break;

                case EScProto.ProcStat:
                    {
                        var Proto = new SScProcStat();
                        Proto.Push(Stream_);

                        var Agent = _AgentInfos[Proto.AgentKey.PeerNum];
                        Agent.ProcStat[Proto.KeyData.Key] = JsonParser.Parse(Proto.KeyData.Data);
                        Agent.Item.SubItems[(Int32)EField.ProcStat].Text = Agent.ProcStat.ToString();
                    }
                    break;

                case EScProto.Notify:
                    {
                        var Proto = new SScNotify();
                        Proto.Push(Stream_);

                        tbOutput.AppendText(Proto.Msg);
                    }
                    break;
            }
        }

        rso.monitor.CClient _Net = null;

        private void btnConnect_Click(object sender, EventArgs e)
        {
            _Net = new rso.monitor.CClient(new CNamePort(_Option.Data.Servers[_Option.Data.ServerNo]), Link, LinkFail, UnLink, Recv);
            if (_Net == null)
                return;

            if (_Net.Login(new SCsLogin(tbID.Text, tbPassword.Text)))
            {
                cbServer.Enabled = false;
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                TimerNet.Enabled = true;
            }
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            TimerNet.Enabled = false;
            _Net.Logout();
            btnDisconnect.Enabled = false;
        }
        private void TimerNet_Tick(object sender, EventArgs e)
        {
            _Net.Proc();
        }
        List<SKey> GetAgentKeys()
        {
            var Keys = new List<SKey>();

            foreach (ListViewItem Item in lvAgents.SelectedItems)
                Keys.Add(GetKey(Item));

            return Keys;
        }
        private void runProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Net.AgentRunProcess(GetAgentKeys(), new SSaRunProcess());
        }
        private void killProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Press OK to Kill Process", "KillProcess", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;

            _Net.AgentKillProcess(GetAgentKeys(), new SSaKillProcess());
        }
        private void fileSendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _OpenFileDialog.Multiselect = true;
            var Ret = _OpenFileDialog.ShowDialog();
            if (Ret != DialogResult.OK)
                return;

            if (_OpenFileDialog.FileNames.Count() == 0)
                return;

            try
            {
                var Proto = new SSaFileSend();
                for (Int32 i = 0; i < _OpenFileDialog.FileNames.Count(); ++i)
                {
                    var fileinfo = new SFileInfo();
                    fileinfo.PathName = Path.Combine(tbRemoteFileTransferDirectory.Text, _OpenFileDialog.SafeFileNames[i]);
                    fileinfo.Stream.LoadFile(_OpenFileDialog.FileNames[i]);
                    Proto.Files.Add(fileinfo);
                }

                _Net.AgentFileSend(GetAgentKeys(), Proto);
            }
            catch (Exception Exception_)
            {
                MessageBox.Show(Exception_.Message);
            }
        }
        private void directorysendtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Ret = _FolderBrowserDialog.ShowDialog();
            if (Ret != DialogResult.OK)
                return;

            if (_FolderBrowserDialog.SelectedPath.Length == 0)
                return;

            _Option.Data.LocalDirectory = _FolderBrowserDialog.SelectedPath;
            _Option.Save();

            try
            {
                var Proto = new SSaFileSend();
                foreach (var file in Directory.GetFiles(_FolderBrowserDialog.SelectedPath, "*", SearchOption.AllDirectories))
                {
                    var SafeName = file.Substring(_FolderBrowserDialog.SelectedPath.Length + 1);
                    var fileinfo = new SFileInfo();

                    fileinfo.PathName = Path.Combine(Path.Combine(tbRemoteFileTransferDirectory.Text, new DirectoryInfo(_FolderBrowserDialog.SelectedPath).Name), SafeName);
                    fileinfo.Stream.LoadFile(file);
                    Proto.Files.Add(fileinfo);
                }

                _Net.AgentFileSend(GetAgentKeys(), Proto);
            }
            catch (Exception Exception_)
            {
                MessageBox.Show(Exception_.Message);
            }
        }
        private void setKeepAliveOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Net.AgentKeepAlive(GetAgentKeys(), new SSaKeepAlive(true));
        }
        private void keepAliveOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Net.AgentKeepAlive(GetAgentKeys(), new SSaKeepAlive(false));
        }
        public void SendProcStop(string Message_, Int32 SecondLeft_)
        {
            _Net.ProcStop(GetAgentKeys(), new SApStop(Message_, SecondLeft_));
        }
        public void SendProcMessage(string Message_)
        {
            _Net.ProcMessage(GetAgentKeys(), new SApMessage(Message_));
        }
        public void SendPushMessage(string Title_, string Msg_)
        {
            var Stream = new CStream();
            Stream.Push("PushMessage");
            Stream.Push(Title_);
            Stream.Push(Msg_);
            _Net.ProcUserProto(GetAgentKeys(), Stream);
        }
        public void SendPunish(TUID UID_, TimePoint EndTime_)
        {
            var Stream = new CStream();
            Stream.Push("Punish");
            Stream.Push(UID_);
            Stream.Push(EndTime_);
            _Net.ProcUserProto(GetAgentKeys(), Stream);
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new ProcStop(this);
            dlg.ShowDialog();
        }
        private void lvAgents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAgents.SelectedItems.Count <= 0)
                cmCommand.Enabled = false;
            else
                cmCommand.Enabled = true;
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            _Net.AgentShellCommand(GetAgentKeys(), new SSaShellCommand(tbCommand.Text));

            if (lvRecentCommands.Items.Count == 0 ||
                lvRecentCommands.Items[0].Text != tbCommand.Text)
                lvRecentCommands.Items.Insert(0, tbCommand.Text);
        }
        private void lvRecentCommands_Click(object sender, EventArgs e)
        {
            AcceptButton = btnRecentSave;

            foreach (ListViewItem Item in (ListView.SelectedListViewItemCollection)lvRecentCommands.SelectedItems)
                tbCommand.Text = Item.SubItems[0].Text;
        }
        private void lvSavedCommands_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in (ListView.SelectedListViewItemCollection)lvSavedCommands.SelectedItems)
                tbCommand.Text = Item.SubItems[1].Text;
        }
        private void btnSavedDel_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in (ListView.SelectedListViewItemCollection)lvSavedCommands.SelectedItems)
                SubCommand(Item);
        }
        private void btnRecentSave_Click(object sender, EventArgs e)
        {
            if (tbCommand.Text == "")
                return;

            var dlg = new CommandAdd(this, tbCommand.Text);
            dlg.ShowDialog();
        }
        private void tbCommand_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnSend;
        }

        private void lvAgents_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == ((ListViewItemComparer)lvAgents.ListViewItemSorter).Column)
            {
                if (lvAgents.Sorting == SortOrder.Ascending)
                    lvAgents.Sorting = SortOrder.Descending;
                else
                    lvAgents.Sorting = SortOrder.Ascending;
            }
            else
            {
                ((ListViewItemComparer)lvAgents.ListViewItemSorter).Column = e.Column;
                lvAgents.Sorting = SortOrder.Ascending;
            }

            ((ListViewItemComparer)lvAgents.ListViewItemSorter).Order = lvAgents.Sorting;
            lvAgents.Sort();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var dlg = new ProcMessage(this);
            dlg.ShowDialog();
        }

        private void pushMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new UserProtocol(this);
            dlg.ShowDialog();
        }

        private void CbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Option.Data.ServerNo = cbServer.SelectedIndex;
            _Option.Save();
        }

        private void tbRemoteFileTransferDirectory_TextChanged(object sender, EventArgs e)
        {
            _Option.Data.RemoteDirectory = tbRemoteFileTransferDirectory.Text;
            _Option.Save();
        }
    }
}
