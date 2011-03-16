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
using Kistl.Client.Presentables;
using Kistl.Client.WPF.View.KistlBase;
using Kistl.Client.Presentables.ValueViewModels;
using Kistl.Client.WPF.CustomControls;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for EnumSelectionView.xaml
    /// </summary>
    public partial class EnumSelectionView : PropertyEditor, IHasViewModel<EnumerationValueViewModel>
    {
        public EnumSelectionView()
        {
            InitializeComponent();
        }

        #region IHasViewModel<EnumerationPropertyModel> Members

        public EnumerationValueViewModel ViewModel
        {
            get { return (EnumerationValueViewModel)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return cb; }
        }
    }
}
