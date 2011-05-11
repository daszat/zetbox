
namespace Kistl.Client.WPF.View.GUI
{
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
    using Kistl.Client.Presentables.GUI;
    using Kistl.Client.WPF.CustomControls;
    using Kistl.Client.WPF.Toolkit;

    /// <summary>
    /// Interaction logic for NavigationScreenDisplay.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class NavigationScreenDisplay : UserControl, IHasViewModel<NavigationScreenViewModel>
    {
        public NavigationScreenDisplay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        #region IHasViewModel<NavigationScreenViewModel> Members

        public NavigationScreenViewModel ViewModel
        {
            get { return (NavigationScreenViewModel)DataContext; }
        }

        #endregion

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == DataContextProperty)
            {
                if (ViewModel != null) this.ApplyIsBusyBehaviour(ViewModel);
            }
        }
    }
}
