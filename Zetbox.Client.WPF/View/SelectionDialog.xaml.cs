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

namespace Zetbox.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for SelectionDialog.xaml
    /// </summary>
    public partial class SelectionDialog : WindowView, IHasViewModel<DataObjectSelectionTaskViewModel>
    {
        public SelectionDialog()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        #region IHasViewModel<DataObjectSelectionTaskViewModel> Members

        public DataObjectSelectionTaskViewModel ViewModel
        {
            get { return (DataObjectSelectionTaskViewModel)DataContext; }
        }

        #endregion
    }
}
