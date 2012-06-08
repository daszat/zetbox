
namespace Zetbox.Client.WPF.View
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
    using System.Windows.Shapes;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.WPF.CustomControls;

    /// <summary>
    /// Interaction logic for ErrorListView.xaml
    /// </summary>
    public partial class ErrorListView : WindowView, IHasViewModel<ErrorListViewModel>
    {
        public ErrorListView()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        #region IHasViewModel<ErrorListViewModel> Members

        public ErrorListViewModel ViewModel
        {
            get { return (ErrorListViewModel)DataContext; }
        }

        #endregion
    }
}
