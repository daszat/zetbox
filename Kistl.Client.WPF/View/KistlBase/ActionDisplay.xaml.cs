using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Kistl.Client.GUI;
using Kistl.Client.Presentables;
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
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(ActionDisplay_Loaded);
        }

        void ActionDisplay_Loaded(object sender, RoutedEventArgs e)
        {
            // Set Keyboardmanager manual, because this Control has to be a CommandButton (Styling, Toolbar, etc.)
            this.SetValue(FocusManager.FocusedElementProperty, this);
        }

        public ActionViewModel ViewModel
        {
            get { return (ActionViewModel)DataContext; }
        }
    }
}
