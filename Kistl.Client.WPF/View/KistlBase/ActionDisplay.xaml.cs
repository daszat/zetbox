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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kistl.Client.Presentables;
using Kistl.Client.GUI;
using Kistl.Client.WPF.CustomControls;

namespace Kistl.Client.WPF.View.KistlBase
{
    /// <summary>
    /// Interaction logic for ActionView.xaml
    /// </summary>
    public partial class ActionDisplay : CommandButton, IHasViewModel<ActionViewModel>
    {
        public ActionDisplay()
        {
            InitializeComponent();
        }

        public ActionViewModel ViewModel
        {
            get { return (ActionViewModel)DataContext; }
        }
    }
}
