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
using Zetbox.Client.Presentables.GUI;
using Zetbox.Client.WPF.Toolkit;

namespace Zetbox.Client.WPF.View.ZetboxBase
{
    /// <summary>
    /// No ViewDescriptor -> internal
    /// </summary>
    public partial class SavedListConfiguratorDisplay : UserControl, IHasViewModel<SavedListConfiguratorViewModel>
    {
        public SavedListConfiguratorDisplay()
        {
            InitializeComponent();
        }

        public SavedListConfiguratorViewModel ViewModel
        {
            get { return (SavedListConfiguratorViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }
    }
}
