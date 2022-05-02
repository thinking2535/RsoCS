using System;
using System.Windows.Forms;
using rso.core;

namespace MonitorClient
{
    public partial class ProcMessage : Form
    {
        MonitorClient _Parent;
        public ProcMessage(MonitorClient Parent_)
        {
            _Parent = Parent_;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbMessage.Text == "")
                return;

            _Parent.SendProcMessage(tbMessage.Text);
            tbMessage.Text = "";
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
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
