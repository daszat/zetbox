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
using Zetbox.ConfigEditor.ViewModels;

namespace Zetbox.ConfigEditor
{
    /// <summary>
    /// Interaction logic for SelectModuleDialog.xaml
    /// </summary>
    public partial class SelectModuleDialog : Window
    {
        private SelectModuleDlgViewModel _vmdl;
        public SelectModuleDialog(SelectModuleDlgViewModel vmdl)
        {
            InitializeComponent();
            _vmdl = vmdl;
            DataContext = vmdl;
            _vmdl.Close += new EventHandler(_vmdl_Close);
        }

        void _vmdl_Close(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
