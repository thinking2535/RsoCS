using System;
using System.Windows.Forms;

namespace MonitorClient
{
    public partial class PushMessage : Form
    {
        MonitorClient _Parent;
        public PushMessage(MonitorClient Parent_)
        {
            _Parent = Parent_;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbTitle.Text == "" || tbMsg.Text == "")
                return;

            _Parent.SendPushMessage(tbTitle.Text, tbMsg.Text);
            tbTitle.Text = "";
            tbMsg.Text = "";
        }

        private void PushMessage_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Enter:
                    button1_Click(sender, e);
                    break;
            }
        }
    }
}
