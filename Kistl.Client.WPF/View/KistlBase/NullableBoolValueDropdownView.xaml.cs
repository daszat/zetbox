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
using Kistl.Client.GUI;
using Kistl.Client.Presentables;
using Kistl.Client.Presentables.ValueViewModels;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for NullableBoolValueView.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class NullableBoolValueDropdownView : PropertyEditor, IHasViewModel<IValueViewModel<bool>>
    {
        public NullableBoolValueDropdownView()
        {
            InitializeComponent();

            cbBool.ItemsSource = new[] { 
                new KeyValuePair<bool?, string>(null, string.Empty),
                new KeyValuePair<bool?, string>(true, true.ToString()),
                new KeyValuePair<bool?, string>(false, false.ToString())
            };
        }

        #region IHasViewModel<IValueModel<bool>> Members

        public IValueViewModel<bool> ViewModel
        {
            get { return (IValueViewModel<bool>)DataContext; }
        }

        #endregion
    }
}
