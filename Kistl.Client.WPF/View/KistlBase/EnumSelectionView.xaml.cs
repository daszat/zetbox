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

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for EnumSelectionView.xaml
    /// </summary>
    public partial class EnumSelectionView : PropertyEditor, IHasViewModel<EnumerationPropertyModel>
    {
        public EnumSelectionView()
        {
            InitializeComponent();
        }

        #region IHasViewModel<EnumerationPropertyModel> Members

        public EnumerationPropertyModel ViewModel
        {
            get { return (EnumerationPropertyModel)DataContext; }
        }

        #endregion
    }
}
