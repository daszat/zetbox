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
using Kistl.Client.WPF.View.KistlBase;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for TextValueSelectionView.xaml
    /// </summary>
    public partial class TextValueSelectionView : PropertyEditor
    {
        public TextValueSelectionView()
        {
            InitializeComponent();
        }

        protected override FrameworkElement MainControl
        {
            get { return cb; }
        }
    }
}
