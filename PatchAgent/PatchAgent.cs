using rso.gameutil;
using rso.net;
using rso.patch;
using rso.util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PatchAgent
{
    using TPeerCnt = UInt32;
    using TVersion = System.Int32;
    using TUpdateFiles = System.Collections.Generic.Dictionary<System.String,System.Boolean>;
    using TFiles = System.Collections.Generic.Dictionary<System.String,rso.patch.SFile>;
    public partial class PatchAgent : Form
    {
        enum EField
        {
            FileName,
            Add,
            Max,
            Null
        }
        class _CUpdateList
        {
            public bool IsAdded;
            public ListViewItem Item;

            public _CUpdateList(bool IsAdded_, ListViewItem Item_)
            {
                IsAdded = IsAdded_;
                Item = Item_;
            }
        }

        COptionJson<SOption> _Option;
        CAgent _Net;
        Dictionary<string, _CUpdateList> _UpdateList = new Dictionary<string, _CUpdateList>();
        void Log(string Msg_)
        {
            try
            {
                tbLog.AppendText(Msg_.Replace("\n", "\r\n") + "\r\n");
            }
            catch
            {
            }
        }
        void Link(CKey Key_)
        {
            Log("Link PeerNum : " + Key_.PeerNum.ToString());
            btnLogout.Enabled = true;
            btnSub.Enabled = true;
            btnAdd.Enabled = true;
            btnDel.Enabled = true;
            btnUpdate.Enabled = true;
            btnReset.Enabled = true;
        }
        void LinkFail(TPeerCnt PeerNum_, ENetRet NetRet_)
        {
            Log("LinkFail PeerNum : " + PeerNum_.ToString());
            btnLogin.Enabled = true;
        }
        void UnLink(CKey Key_, ENetRet NetRet_)
        {
            ClearUpdateList();
            Log("UnLink PeerNum : " + Key_.PeerNum.ToString());
            btnLogin.Enabled = true;
            btnLogout.Enabled = false;
            btnSub.Enabled = false;
            btnAdd.Enabled = false;
            btnDel.Enabled = false;
            btnUpdate.Enabled = false;
            btnReset.Enabled = false;
        }
        void SetVersion(SVersion Version_)
        {
            tbMainVer.Text = Version_.Main.ToString();
            tbSubVer.Text = Version_.Sub.ToString();
        }
        void DataCallback(SPatchData PatchData_)
        {
            if (Int32.Parse(tbMainVer.Text) != PatchData_.Version.Main ||
                Int32.Parse(tbSubVer.Text) > PatchData_.Version.Sub)
                lbFullData.Items.Clear();

            SetVersion(PatchData_.Version);
            Log("Version : Main[" + PatchData_.Version.Main.ToString() + "] Sub[" + PatchData_.Version.Sub.ToString() + "]");

            foreach (var i in PatchData_.Files)
            {
                if (i.Value.IsAdded)
                {
                    if (!lbFullData.Items.Contains(i.Key))
                        lbFullData.Items.Add(i.Key);
                }
                else
                {
                    if (lbFullData.Items.Contains(i.Key))
                        lbFullData.Items.Remove(i.Key);
                }
            }
        }
        public PatchAgent()
        {
            InitializeComponent();

            Int32[] Widths = new Int32[(Int32)EField.Max]
            {
                650,
                50
            };

            AgentFields = new System.Windows.Forms.ColumnHeader[(Int32)EField.Max];
            for (Int32 i = 0; i < (Int32)EField.Max; ++i)
            {
                AgentFields[i] = new System.Windows.Forms.ColumnHeader();
                AgentFields[i].Text = ((EField)i).ToString();
                AgentFields[i].Width = Widths[i];
            }

            lvUpdateList.Columns.AddRange(AgentFields);
            //lvUpdateList.Location = new System.Drawing.Point(13, 39);
            //lvUpdateList.Size = new System.Drawing.Size(989, 677);
            lvUpdateList.UseCompatibleStateImageBehavior = false;
            lvUpdateList.FullRowSelect = true;
            lvUpdateList.ListViewItemSorter = new ListViewItemComparer(lvUpdateList.Sorting, 0);

            btnLogout.Enabled = false;
            btnSub.Enabled = false;
            btnAdd.Enabled = false;
            btnDel.Enabled = false;
            btnUpdate.Enabled = false;
            btnReset.Enabled = false;

            try
            {
                _Option = new COptionJson<SOption>("Option.ini", true);

                tbID.Text = _Option.Data.ID;
                tbPW.Text = _Option.Data.PW;

                _Net = new CAgent(_Option.Data.DataFileName, _Option.Data.DataPath, new CNamePort(_Option.Data.MasterNamePort), Link, LinkFail, UnLink, DataCallback);

                lbFullData.ColumnWidth = Width;
                foreach (var i in _Net.GetData().Files)
                    lbFullData.Items.Add(i.Key);

                SetVersion(_Net.GetData().Version);
                timerNet.Enabled = true;
            }
            catch (Exception Exception_)
            {
                Log(Exception_.ToString());
            }
        }

        private void btLogin_Click(object sender, System.EventArgs e)
        {
            if (!_Net.Connect(tbID.Text, tbPW.Text))
            {
                Log("Connect Fail");
                return;
            }

            btnLogin.Enabled = false;
        }

        private void timerNet_Tick(object sender, EventArgs e)
        {
            _Net.Proc();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _Net.Close();
            btnLogout.Enabled = false;
            btnLogin.Enabled = true;
        }

        void AddUpdateList(string FileName_, bool IsAdded_)
        {
            if (!_UpdateList.ContainsKey(FileName_))
            {
                var Item = new ListViewItem(FileName_);
                Item.SubItems.Add(IsAdded_.ToString());
                Item.UseItemStyleForSubItems = false;
                lvUpdateList.Items.Add(Item);
                _UpdateList.Add(FileName_, new _CUpdateList(IsAdded_, Item));
            }
            else
            {
                _UpdateList[FileName_].IsAdded = IsAdded_;
            }
        }

        void DelUpdateList(string FileName_)
        {
            if (!_UpdateList.ContainsKey(FileName_))
                return;

            lvUpdateList.Items.Remove(_UpdateList[FileName_].Item);
            _UpdateList.Remove(FileName_);
        }
        void ClearUpdateList()
        {
            lvUpdateList.Items.Clear();
            _UpdateList.Clear();
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            string[] SelectItems = new string[lbFullData.SelectedItems.Count];
            lbFullData.SelectedItems.CopyTo(SelectItems, 0);

            foreach (var i in SelectItems)
                AddUpdateList(i, false);
        }

        private void lvUpdateList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    btnDel_Click(sender, e);
                    break;
            }
        }

        private void lbFullData_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    btnSub_Click(sender, e);
                    break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*";
            dlg.Multiselect = true;
            dlg.InitialDirectory = _Net.GetDataPathFull();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (var i in dlg.FileNames)
                    AddUpdateList(i.Replace(dlg.InitialDirectory, ""), true);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            ListViewItem[] SelectItems = new ListViewItem[lvUpdateList.SelectedItems.Count];
            lvUpdateList.SelectedItems.CopyTo(SelectItems, 0);

            foreach (var i in SelectItems)
                DelUpdateList(i.Text);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var Files = new TUpdateFiles();

            foreach (var i in _UpdateList)
                Files.Add(i.Key, i.Value.IsAdded);

            _Net.Update(false, Files);
            ClearUpdateList();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            var Files = new TUpdateFiles();

            foreach (var i in _UpdateList)
            {
                if (!i.Value.IsAdded)
                    continue;

                Files.Add(i.Key, i.Value.IsAdded);
            }

            _Net.Update(true, Files);
            ClearUpdateList();
        }
    }
}
