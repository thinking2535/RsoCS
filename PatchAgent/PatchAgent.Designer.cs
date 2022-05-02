namespace PatchAgent
{
    partial class PatchAgent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label4 = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPW = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.timerNet = new System.Windows.Forms.Timer(this.components);
            this.btnLogout = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSubVer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMainVer = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSub = new System.Windows.Forms.Button();
            this.lbFullData = new System.Windows.Forms.ListBox();
            this.btnDel = new System.Windows.Forms.Button();
            this.lvUpdateList = new System.Windows.Forms.ListView();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1451, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "ID";
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(1470, 13);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(80, 21);
            this.tbID.TabIndex = 12;
            this.tbID.Text = "rso";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1556, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "PW";
            // 
            // tbPW
            // 
            this.tbPW.Location = new System.Drawing.Point(1585, 12);
            this.tbPW.Name = "tbPW";
            this.tbPW.PasswordChar = '*';
            this.tbPW.Size = new System.Drawing.Size(76, 21);
            this.tbPW.TabIndex = 16;
            this.tbPW.Text = "5528";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(1667, 10);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 18;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btLogin_Click);
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(1424, 41);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(399, 904);
            this.tbLog.TabIndex = 19;
            // 
            // timerNet
            // 
            this.timerNet.Tick += new System.EventHandler(this.timerNet_Tick);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(1748, 10);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 21;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "SubVer";
            // 
            // tbSubVer
            // 
            this.tbSubVer.Location = new System.Drawing.Point(184, 12);
            this.tbSubVer.Name = "tbSubVer";
            this.tbSubVer.ReadOnly = true;
            this.tbSubVer.Size = new System.Drawing.Size(64, 21);
            this.tbSubVer.TabIndex = 24;
            this.tbSubVer.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "MainVer";
            // 
            // tbMainVer
            // 
            this.tbMainVer.Location = new System.Drawing.Point(70, 12);
            this.tbMainVer.Name = "tbMainVer";
            this.tbMainVer.ReadOnly = true;
            this.tbMainVer.Size = new System.Drawing.Size(58, 21);
            this.tbMainVer.TabIndex = 22;
            this.tbMainVer.Text = "0";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(1262, 11);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 26;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSub
            // 
            this.btnSub.Location = new System.Drawing.Point(637, 12);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(75, 23);
            this.btnSub.TabIndex = 28;
            this.btnSub.Text = ">>";
            this.btnSub.UseVisualStyleBackColor = true;
            this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
            // 
            // lbFullData
            // 
            this.lbFullData.ColumnWidth = 500;
            this.lbFullData.FormattingEnabled = true;
            this.lbFullData.HorizontalScrollbar = true;
            this.lbFullData.ItemHeight = 12;
            this.lbFullData.Location = new System.Drawing.Point(12, 41);
            this.lbFullData.MultiColumn = true;
            this.lbFullData.Name = "lbFullData";
            this.lbFullData.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbFullData.Size = new System.Drawing.Size(700, 904);
            this.lbFullData.TabIndex = 30;
            this.lbFullData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbFullData_KeyDown);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(1181, 11);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 31;
            this.btnDel.Text = "Del";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // lvUpdateList
            // 
            this.lvUpdateList.HideSelection = false;
            this.lvUpdateList.Location = new System.Drawing.Point(718, 41);
            this.lvUpdateList.Name = "lvUpdateList";
            this.lvUpdateList.Size = new System.Drawing.Size(700, 904);
            this.lvUpdateList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvUpdateList.TabIndex = 32;
            this.lvUpdateList.UseCompatibleStateImageBehavior = false;
            this.lvUpdateList.View = System.Windows.Forms.View.Details;
            this.lvUpdateList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvUpdateList_KeyDown);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(1343, 11);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 33;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(1100, 11);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 34;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // PatchAgent
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1835, 961);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lvUpdateList);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.lbFullData);
            this.Controls.Add(this.btnSub);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSubVer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbMainVer);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPW);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbID);
            this.Name = "PatchAgent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PatchAgent";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPW;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Timer timerNet;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSubVer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMainVer;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSub;
        private System.Windows.Forms.ListBox lbFullData;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.ListView lvUpdateList;
        private System.Windows.Forms.ColumnHeader[] AgentFields;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnAdd;
    }
}

