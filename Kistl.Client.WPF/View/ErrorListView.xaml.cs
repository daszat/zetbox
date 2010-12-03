
namespace Kistl.Client.WPF.View
{
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
    
    using Kistl.Client.Presentables;
    using Kistl.Client.WPF.CustomControls;
using Kistl.Client.GUI;

    /// <summary>
    /// Interaction logic for ErrorListView.xaml
    /// </summary>
    public partial class ErrorListView : WindowView, IHasViewModel<ErrorListViewModel>
    {
        public ErrorListView()
        {
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
