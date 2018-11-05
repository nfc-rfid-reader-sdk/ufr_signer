using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uFRSigner
{
    public partial class frmPassword : Form
    {
        public string password
        {
            get { return tbPassword.Text; }
        }

        public frmPassword()
        {
            InitializeComponent();
        }
    }
}
