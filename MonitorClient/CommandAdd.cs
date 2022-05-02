using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonitorClient
{
    public partial class CommandAdd : Form
    {
        MonitorClient _Parent;
        String _Command;

        public CommandAdd(MonitorClient Parent_, String Command_)
        {
            _Parent = Parent_;
            _Command = Command_;
            InitializeComponent();
        }

        private void btnCommandAdd_Click(object sender, EventArgs e)
        {
            if (tbCommandName.Text == "")
                return;

            _Parent.AddCommand(tbCommandName.Text, _Command);
            Close();
        }

        private void CommandAdd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Enter:
                    btnCommandAdd_Click(sender, e);
                    break;
            }
        }
    }
}
