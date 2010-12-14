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
    public partial class PasswordDialog : Form
    {
        public PasswordDialog()
        {
            InitializeComponent();
        }

        public string UserName { get { return txtUserName.Text; } }
        public string Password { get { return txtPassword.Text; } }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            if (cbSave.Checked)
            {
                Properties.Settings.Default.UserName = txtUserName.Text;
                Properties.Settings.Default.Password = txtPassword.Text.Protect();
            }
            else
            {
                Properties.Settings.Default.UserName = string.Empty;
                Properties.Settings.Default.Password = string.Empty;
            }
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void Password_Load(object sender, EventArgs e)
        {
            txtUserName.Text = Properties.Settings.Default.UserName;
            txtPassword.Text = Properties.Settings.Default.Password.Unprotect();
            cbSave.Checked = !string.IsNullOrEmpty(Properties.Settings.Default.UserName);
        }
    }
}
