using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kistl.Client.Bootstrapper
{
    public partial class AddressDialog : Form
    {
        public AddressDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddress.Text))
            {
                Properties.Settings.Default.Address = txtAddress.Text;
                Properties.Settings.Default.Save();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void AddressDialog_Load(object sender, EventArgs e)
        {
            txtAddress.Text = Properties.Settings.Default.Address;
        }
    }
}
