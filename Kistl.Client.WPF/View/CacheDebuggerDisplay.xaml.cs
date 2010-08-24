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
using Kistl.Client.GUI;
using Kistl.Client.Presentables;
using Kistl.Client.WPF.CustomControls;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for CacheDebuggerDisplay.xaml
    /// </summary>
    public partial class CacheDebuggerDisplay : WindowView, IHasViewModel<CacheDebuggerViewModel>
    {
        public CacheDebuggerDisplay()
        {
            InitializeComponent();
        }

        public CacheDebuggerViewModel ViewModel
        {
            get { return (CacheDebuggerViewModel)DataContext; }
        }
    }
}
