using System;
using System.Windows.Forms;

namespace MonitorClient
{
    public partial class ProcStop : Form
    {
        MonitorClient _Parent;
        public ProcStop(MonitorClient Parent_)
        {
            _Parent = Parent_;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbMessage.Text == "")
                return;

            _Parent.SendProcStop(tbMessage.Text, Int32.Parse(tbSecondLeft.Text));
            Close();
        }
        private void ProcStop_KeyDown(object sender, KeyEventArgs e)
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
