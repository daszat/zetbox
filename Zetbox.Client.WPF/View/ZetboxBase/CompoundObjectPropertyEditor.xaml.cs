
namespace Zetbox.Client.WPF.View.ZetboxBase
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
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.WPF.CustomControls;

    /// <summary>
    /// Interaction logic for CompoundObjectPropertyEditor.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class CompoundObjectPropertyEditor : PropertyEditor, IHasViewModel<CompoundObjectPropertyViewModel>
    {
        public CompoundObjectPropertyEditor()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        #region IHasViewModel<CompoundObjectPropertyViewModel> Members

        public CompoundObjectPropertyViewModel ViewModel
        {
            get { return (CompoundObjectPropertyViewModel)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return null; }
        }
    }
}
