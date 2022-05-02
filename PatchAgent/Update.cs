using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchAgent
{
    public partial class Update : Form
    {
        PatchAgent _Parent;
        public Update(PatchAgent Parent_)
        {
            _Parent = Parent_;
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
        }
    }
}
