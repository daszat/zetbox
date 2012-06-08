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
using Zetbox.Client.Presentables.SchemaMigration;
using Zetbox.Client.WPF.CustomControls;
using Zetbox.Client.WPF.View.ZetboxBase;

namespace Zetbox.Client.WPF.View.SchemaMigration
{
    /// <summary>
    /// Interaction logic for DestinationPropertyEditor.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class DestinationPropertyEditor : PropertyEditor, IHasViewModel<DestinationPropertyViewModel>
    {
        public DestinationPropertyEditor()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        #region IHasViewModel<DestinationPropertyViewModel> Members

        public DestinationPropertyViewModel ViewModel
        {
            get { return (DestinationPropertyViewModel)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return null; }
        }
    }
}
