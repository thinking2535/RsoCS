using rso.core;
using System;
using System.Windows.Forms;

namespace MonitorClient
{
    public partial class UserProtocol : Form
    {
        MonitorClient _Parent;
        public UserProtocol(MonitorClient Parent_)
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (tbPunishUID.Text == "")
            {
                MessageBox.Show("Need UID to punish");
                return;
            }

            var TIck = DateTime.MinValue.ToTimePoint().ticks;

            TimePoint EndTime;
            if (cbPermanent.Checked)
            {
                EndTime = new DateTime(3000, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToTimePoint();
            }
            else
            {
                if (tbPunishEndTime.Text == "")
                    EndTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToTimePoint();
                else
                    EndTime = DateTime.Parse(tbPunishEndTime.Text).ToTimePoint();
            }


            _Parent.SendPunish(Int64.Parse(tbPunishUID.Text), EndTime);
            tbPunishUID.Text = "";
            tbPunishEndTime.Text = "";
        }

        private void cbPermanent_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPermanent.Checked)
                tbPunishEndTime.Enabled = false;
            else
                tbPunishEndTime.Enabled = true;
        }
    }
}
