namespace GameClientTest
{
    partial class GameClientTest
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TimerNet = new System.Windows.Forms.Timer(this.components);
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnJoin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPW = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.btnLoginUID = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TimerNet
            // 
            this.TimerNet.Tick += new System.EventHandler(this.TimerNet_Tick);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(1734, 13);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(89, 21);
            this.btnDisconnect.TabIndex = 13;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(1487, 40);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(336, 504);
            this.txtLog.TabIndex = 22;
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(1205, 40);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(132, 23);
            this.btnJoin.TabIndex = 23;
            this.btnJoin.Text = "Create";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1219, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 35;
            this.label4.Text = "PW";
            // 
            // tbPW
            // 
            this.tbPW.Location = new System.Drawing.Point(1248, 13);
            this.tbPW.Name = "tbPW";
            this.tbPW.Size = new System.Drawing.Size(90, 21);
            this.tbPW.TabIndex = 34;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1099, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 12);
            this.label5.TabIndex = 33;
            this.label5.Text = "ID";
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(1121, 13);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(90, 21);
            this.tbID.TabIndex = 32;
            // 
            // btnLoginUID
            // 
            this.btnLoginUID.Location = new System.Drawing.Point(1616, 13);
            this.btnLoginUID.Name = "btnLoginUID";
            this.btnLoginUID.Size = new System.Drawing.Size(112, 21);
            this.btnLoginUID.TabIndex = 38;
            this.btnLoginUID.Text = "LoginUID";
            this.btnLoginUID.UseVisualStyleBackColor = true;
            this.btnLoginUID.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(971, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 12);
            this.label1.TabIndex = 40;
            this.label1.Text = "UID";
            // 
            // tbUID
            // 
            this.tbUID.Location = new System.Drawing.Point(1000, 13);
            this.tbUID.Name = "tbUID";
            this.tbUID.Size = new System.Drawing.Size(90, 21);
            this.tbUID.TabIndex = 39;
            // 
            // GameClientTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1837, 978);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbUID);
            this.Controls.Add(this.btnLoginUID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbPW);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnDisconnect);
            this.KeyPreview = true;
            this.Name = "GameClientTest";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GalaxyClient_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GalaxyClient_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer TimerNet;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPW;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.Button btnLoginUID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUID;
    }
}

