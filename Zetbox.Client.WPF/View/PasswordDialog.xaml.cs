// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Zetbox.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for PasswordDialog.xaml
    /// </summary>
    public partial class PasswordDialog : Window
    {
        public PasswordDialog()
        {
            InitializeComponent();
            Title = PasswordDialogResources.Title;
            _usernameLabel.Text = PasswordDialogResources.Username;
            _passwordLabel.Text = PasswordDialogResources.Password;
            _login.Content = PasswordDialogResources.Login;
            _cancel.Content = PasswordDialogResources.Cancel;
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            Activate();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_username.Text) || string.IsNullOrWhiteSpace(_password.Password))
                return;

            DialogResult = true;
            Close();
        }

        internal class Adapter : IPasswordDialog
        {
            private PasswordDialog _dlg;

            public string Username
            {
                get { return _dlg._username.Text ?? string.Empty; }
            }

            public string Password
            {
                get { return _dlg._password.Password ?? string.Empty; }
            }

            public bool QueryUser()
            {
                // Create a new dialog every time
                // WPF cannot re-open windows once they're closed
                _dlg = new PasswordDialog();
                return _dlg.ShowDialog() ?? false;
            }
        }
    }
}
