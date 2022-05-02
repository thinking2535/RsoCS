namespace MonitorClient
{
    partial class MonitorClient
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.tbID = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lvAgents = new System.Windows.Forms.ListView();
            this.cmCommand = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setKeepAliveOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keepAliveOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.directorysendtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pushMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TimerNet = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCommand = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnSavedDel = new System.Windows.Forms.Button();
            this.btnRecentSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lvSavedCommands = new System.Windows.Forms.ListView();
            this.savedName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.savedCommand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvRecentCommands = new System.Windows.Forms.ListView();
            this.recentCommand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbRemoteFileTransferDirectory = new System.Windows.Forms.TextBox();
            this.cbServer = new System.Windows.Forms.ComboBox();
            this.cmCommand.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(1473, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(112, 21);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(1591, 12);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(112, 21);
            this.btnDisconnect.TabIndex = 2;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(1081, 12);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(161, 21);
            this.tbID.TabIndex = 3;
            this.tbID.Text = "yoo";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(1277, 12);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(162, 21);
            this.tbPassword.TabIndex = 4;
            this.tbPassword.Text = "2535";
            // 
            // lvAgents
            // 
            this.lvAgents.HideSelection = false;
            this.lvAgents.Location = new System.Drawing.Point(0, 0);
            this.lvAgents.Name = "lvAgents";
            this.lvAgents.Size = new System.Drawing.Size(121, 97);
            this.lvAgents.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAgents.TabIndex = 26;
            this.lvAgents.UseCompatibleStateImageBehavior = false;
            this.lvAgents.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvAgents_ColumnClick);
            // 
            // cmCommand
            // 
            this.cmCommand.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setKeepAliveOnToolStripMenuItem,
            this.keepAliveOffToolStripMenuItem,
            this.runProcessToolStripMenuItem,
            this.killProcessToolStripMenuItem,
            this.fileSendToolStripMenuItem,
            this.directorysendtoolStripMenuItem,
            this.toolStripMenuItem1,
            this.stopToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripSeparator1,
            this.pushMessageToolStripMenuItem});
            this.cmCommand.Name = "cmCommand";
            this.cmCommand.Size = new System.Drawing.Size(151, 214);
            // 
            // setKeepAliveOnToolStripMenuItem
            // 
            this.setKeepAliveOnToolStripMenuItem.Name = "setKeepAliveOnToolStripMenuItem";
            this.setKeepAliveOnToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.setKeepAliveOnToolStripMenuItem.Text = "KeepAliveOn";
            this.setKeepAliveOnToolStripMenuItem.Click += new System.EventHandler(this.setKeepAliveOnToolStripMenuItem_Click);
            // 
            // keepAliveOffToolStripMenuItem
            // 
            this.keepAliveOffToolStripMenuItem.Name = "keepAliveOffToolStripMenuItem";
            this.keepAliveOffToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.keepAliveOffToolStripMenuItem.Text = "KeepAliveOff";
            this.keepAliveOffToolStripMenuItem.Click += new System.EventHandler(this.keepAliveOffToolStripMenuItem_Click);
            // 
            // runProcessToolStripMenuItem
            // 
            this.runProcessToolStripMenuItem.Name = "runProcessToolStripMenuItem";
            this.runProcessToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.runProcessToolStripMenuItem.Text = "RunProcess";
            this.runProcessToolStripMenuItem.Click += new System.EventHandler(this.runProcessToolStripMenuItem_Click);
            // 
            // killProcessToolStripMenuItem
            // 
            this.killProcessToolStripMenuItem.Name = "killProcessToolStripMenuItem";
            this.killProcessToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.killProcessToolStripMenuItem.Text = "KillProcess";
            this.killProcessToolStripMenuItem.Click += new System.EventHandler(this.killProcessToolStripMenuItem_Click);
            // 
            // fileSendToolStripMenuItem
            // 
            this.fileSendToolStripMenuItem.Name = "fileSendToolStripMenuItem";
            this.fileSendToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.fileSendToolStripMenuItem.Text = "FileSend";
            this.fileSendToolStripMenuItem.Click += new System.EventHandler(this.fileSendToolStripMenuItem_Click);
            // 
            // directorysendtoolStripMenuItem
            // 
            this.directorysendtoolStripMenuItem.Name = "directorysendtoolStripMenuItem";
            this.directorysendtoolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.directorysendtoolStripMenuItem.Text = "DirectorySend";
            this.directorysendtoolStripMenuItem.Click += new System.EventHandler(this.directorysendtoolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(147, 6);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(150, 22);
            this.toolStripMenuItem2.Text = "Message";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // pushMessageToolStripMenuItem
            // 
            this.pushMessageToolStripMenuItem.Name = "pushMessageToolStripMenuItem";
            this.pushMessageToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.pushMessageToolStripMenuItem.Text = "UserProtocol";
            this.pushMessageToolStripMenuItem.Click += new System.EventHandler(this.pushMessageToolStripMenuItem_Click);
            // 
            // TimerNet
            // 
            this.TimerNet.Tick += new System.EventHandler(this.TimerNet_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1248, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "PW";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1059, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "ID";
            // 
            // tbCommand
            // 
            this.tbCommand.Location = new System.Drawing.Point(1008, 114);
            this.tbCommand.Name = "tbCommand";
            this.tbCommand.Size = new System.Drawing.Size(639, 21);
            this.tbCommand.TabIndex = 12;
            this.tbCommand.Enter += new System.EventHandler(this.tbCommand_Enter);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(1653, 112);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(50, 23);
            this.btnSend.TabIndex = 13;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnSavedDel
            // 
            this.btnSavedDel.Location = new System.Drawing.Point(1653, 553);
            this.btnSavedDel.Name = "btnSavedDel";
            this.btnSavedDel.Size = new System.Drawing.Size(50, 23);
            this.btnSavedDel.TabIndex = 19;
            this.btnSavedDel.Text = "Del";
            this.btnSavedDel.UseVisualStyleBackColor = true;
            this.btnSavedDel.Click += new System.EventHandler(this.btnSavedDel_Click);
            // 
            // btnRecentSave
            // 
            this.btnRecentSave.Location = new System.Drawing.Point(1653, 358);
            this.btnRecentSave.Name = "btnRecentSave";
            this.btnRecentSave.Size = new System.Drawing.Size(50, 23);
            this.btnRecentSave.TabIndex = 18;
            this.btnRecentSave.Text = "Save";
            this.btnRecentSave.UseVisualStyleBackColor = true;
            this.btnRecentSave.Click += new System.EventHandler(this.btnRecentSave_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1008, 539);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "Saved Commands";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1008, 344);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "Recent Commands";
            // 
            // lvSavedCommands
            // 
            this.lvSavedCommands.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.savedName,
            this.savedCommand});
            this.lvSavedCommands.HideSelection = false;
            this.lvSavedCommands.Location = new System.Drawing.Point(1008, 554);
            this.lvSavedCommands.MultiSelect = false;
            this.lvSavedCommands.Name = "lvSavedCommands";
            this.lvSavedCommands.Size = new System.Drawing.Size(639, 162);
            this.lvSavedCommands.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvSavedCommands.TabIndex = 15;
            this.lvSavedCommands.UseCompatibleStateImageBehavior = false;
            this.lvSavedCommands.View = System.Windows.Forms.View.Details;
            this.lvSavedCommands.Click += new System.EventHandler(this.lvSavedCommands_Click);
            // 
            // savedName
            // 
            this.savedName.Text = "Name";
            this.savedName.Width = 85;
            // 
            // savedCommand
            // 
            this.savedCommand.Text = "Command";
            this.savedCommand.Width = 1200;
            // 
            // lvRecentCommands
            // 
            this.lvRecentCommands.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.recentCommand});
            this.lvRecentCommands.HideSelection = false;
            this.lvRecentCommands.Location = new System.Drawing.Point(1008, 359);
            this.lvRecentCommands.MultiSelect = false;
            this.lvRecentCommands.Name = "lvRecentCommands";
            this.lvRecentCommands.Size = new System.Drawing.Size(639, 162);
            this.lvRecentCommands.TabIndex = 14;
            this.lvRecentCommands.UseCompatibleStateImageBehavior = false;
            this.lvRecentCommands.View = System.Windows.Forms.View.Details;
            this.lvRecentCommands.Click += new System.EventHandler(this.lvRecentCommands_Click);
            // 
            // recentCommand
            // 
            this.recentCommand.Text = "Command";
            this.recentCommand.Width = 1200;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1008, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "ShellCommand";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1006, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "Output";
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(1008, 166);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutput.Size = new System.Drawing.Size(639, 163);
            this.tbOutput.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1006, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(165, 12);
            this.label9.TabIndex = 25;
            this.label9.Text = "RemoteFileTransferDirectory";
            // 
            // tbRemoteFileTransferDirectory
            // 
            this.tbRemoteFileTransferDirectory.Location = new System.Drawing.Point(1008, 62);
            this.tbRemoteFileTransferDirectory.Name = "tbRemoteFileTransferDirectory";
            this.tbRemoteFileTransferDirectory.Size = new System.Drawing.Size(639, 21);
            this.tbRemoteFileTransferDirectory.TabIndex = 24;
            this.tbRemoteFileTransferDirectory.TextChanged += new System.EventHandler(this.tbRemoteFileTransferDirectory_TextChanged);
            // 
            // cbServer
            // 
            this.cbServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServer.FormattingEnabled = true;
            this.cbServer.Location = new System.Drawing.Point(825, 12);
            this.cbServer.Name = "cbServer";
            this.cbServer.Size = new System.Drawing.Size(228, 20);
            this.cbServer.TabIndex = 27;
            this.cbServer.SelectedIndexChanged += new System.EventHandler(this.CbServer_SelectedIndexChanged);
            // 
            // MonitorClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1715, 728);
            this.Controls.Add(this.cbServer);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbRemoteFileTransferDirectory);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSavedDel);
            this.Controls.Add(this.btnRecentSave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lvSavedCommands);
            this.Controls.Add(this.lvRecentCommands);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbCommand);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvAgents);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MonitorClient";
            this.Text = "MonitorClient";
            this.cmCommand.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.ListView lvAgents;
        private System.Windows.Forms.ColumnHeader[] AgentFields;
        private System.Windows.Forms.Timer TimerNet;
        private System.Windows.Forms.ContextMenuStrip cmCommand;
        private System.Windows.Forms.ToolStripMenuItem killProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setKeepAliveOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keepAliveOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem fileSendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runProcessToolStripMenuItem;
        private System.Windows.Forms.TextBox tbCommand;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnSavedDel;
        private System.Windows.Forms.Button btnRecentSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView lvSavedCommands;
        private System.Windows.Forms.ColumnHeader savedName;
        private System.Windows.Forms.ColumnHeader savedCommand;
        private System.Windows.Forms.ListView lvRecentCommands;
        private System.Windows.Forms.ColumnHeader recentCommand;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.ToolStripMenuItem directorysendtoolStripMenuItem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbRemoteFileTransferDirectory;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem pushMessageToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbServer;
    }
}

