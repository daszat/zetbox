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
using Kistl.Client.GUI;
using Kistl.Client.Presentables.GUI;
using Kistl.Client.WPF.CustomControls;

namespace Kistl.Client.WPF.View.GUI
{
    /// <summary>
    /// Interaction logic for SelectionDialog.xaml
    /// </summary>
    [ViewDescriptor("GUI", Kistl.App.GUI.Toolkit.WPF, Kind = "Kistl.App.GUI.MultiLineEditorDialog")]
    public partial class MultiLineEditorDialog : WindowView, IHasViewModel<MultiLineEditorDialogModel>
    {
        public MultiLineEditorDialog()
        {
            InitializeComponent();
        }

        #region IHasViewModel<MultiLineEditorDialogModel> Members

        public MultiLineEditorDialogModel ViewModel
        {
            get { return (MultiLineEditorDialogModel)DataContext; }
        }

        #endregion
    }
}
