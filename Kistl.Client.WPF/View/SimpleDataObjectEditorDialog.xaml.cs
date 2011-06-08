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
using Kistl.Client.GUI;
using Kistl.Client.Presentables;
using Kistl.Client.WPF.CustomControls;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for SimpleDataObjectEditorDialog.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class SimpleDataObjectEditorDialog : WindowView, IHasViewModel<SimpleDataObjectEditorTaskViewModel>
    {
        public SimpleDataObjectEditorDialog()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        #region IHasViewModel<SimpleDataObjectEditorTaskViewModel> Members

        public SimpleDataObjectEditorTaskViewModel ViewModel
        {
            get { return (SimpleDataObjectEditorTaskViewModel)DataContext; }
        }

        #endregion
    }
}
