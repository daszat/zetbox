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
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables.ZetboxBase;
using Zetbox.Client.WPF.CustomControls;
using Zetbox.Client.WPF.Toolkit;

namespace Zetbox.Client.WPF.View.ZetboxBase
{
    /// <summary>
    /// Interaction logic for AnyReferencePropertyEditor.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class AnyReferencePropertyEditor : PropertyEditor, IHasViewModel<AnyReferencePropertyViewModel>
    {
        public AnyReferencePropertyEditor()
        {
            InitializeComponent();
        }

        public AnyReferencePropertyViewModel ViewModel
        {
            get { return (AnyReferencePropertyViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        protected override FrameworkElement MainControl
        {
            get { return null; }
        }
    }
}
