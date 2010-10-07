
namespace Kistl.Client.WPF.View.KistlBase
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Kistl.Client.GUI;
    using Kistl.Client.Presentables.ValueViewModels;

    /// <summary>
    /// Interaction logic for CompoundObjectPropertyEditor.xaml
    /// </summary>
    [ViewDescriptor("GUI", Kistl.App.GUI.Toolkit.WPF, Kind = "Kistl.App.GUI.CompoundObjectPropertyKind")]
    public partial class CompoundObjectPropertyEditor : PropertyEditor, IHasViewModel<CompoundObjectPropertyViewModel>
    {
        public CompoundObjectPropertyEditor()
        {
            InitializeComponent();
        }

        #region IHasViewModel<CompoundObjectPropertyViewModel> Members

        public CompoundObjectPropertyViewModel ViewModel
        {
            get { return (CompoundObjectPropertyViewModel)DataContext; }
        }

        #endregion
    }
}
