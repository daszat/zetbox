
namespace Zetbox.Client.WPF.View.GUI
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
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.WPF.CustomControls;
    using Zetbox.Client.WPF.Toolkit;

    /// <summary>
    /// Interaction logic for NavigationScreenDisplay.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class NavigationScreenDisplay : UserControl, IHasViewModel<NavigationEntryViewModel>
    {
        public NavigationScreenDisplay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        #region IHasViewModel<NavigationScreenViewModel> Members

        public NavigationEntryViewModel ViewModel
        {
            get { return (NavigationEntryViewModel)DataContext; }
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
