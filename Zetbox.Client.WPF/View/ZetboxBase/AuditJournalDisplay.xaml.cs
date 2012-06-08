
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
    using Kistl.App.GUI;
    using Kistl.Client.GUI;
    using Kistl.Client.Presentables.KistlBase;
    using Kistl.Client.WPF.CustomControls;

    /// <summary>
    /// Interaction logic for AuditJournalView.xaml
    /// </summary>
    [ViewDescriptor(Toolkit.WPF)]
    public partial class AuditJournalView : PropertyEditor, IHasViewModel<AuditJournalViewModel>
    {
        public AuditJournalView()
        {
            InitializeComponent();
        }

        public AuditJournalViewModel ViewModel
        {
            get { return (AuditJournalViewModel)DataContext; }
        }

        protected override FrameworkElement MainControl
        {
            get { return list; }
        }
    }
}
