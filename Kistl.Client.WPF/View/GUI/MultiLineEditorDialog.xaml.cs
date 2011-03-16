
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
    using System.Windows.Shapes;
    using Kistl.Client.GUI;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.GUI;
    using Kistl.Client.WPF.CustomControls;

    /// <summary>
    /// Interaction logic for SelectionDialog.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class MultiLineEditorDialog : WindowView, IHasViewModel<MultiLineEditorDialogViewModel>
    {
        public MultiLineEditorDialog()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        #region IHasViewModel<MultiLineEditorDialogViewModel> Members

        public MultiLineEditorDialogViewModel ViewModel
        {
            get { return (MultiLineEditorDialogViewModel)DataContext; }
        }

        #endregion
    }
}
