
namespace Zetbox.Client.WPF.View.ZetboxBase
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
    using Zetbox.App.GUI;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.Client.WPF.CustomControls;

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
