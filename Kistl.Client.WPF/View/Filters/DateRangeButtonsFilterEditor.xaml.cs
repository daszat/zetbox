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
using Kistl.Client.GUI;
using Kistl.Client.Presentables.FilterViewModels;
using Kistl.Client.Presentables.KistlBase;
using System.Windows.Controls.Primitives;

namespace Kistl.Client.WPF.View.Filters
{
    /// <summary>
    /// Interaction logic for StringFilterExpression.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class DateRangeButtonsFilterEditor : UserControl, IHasViewModel<DateRangeFilterViewModel>
    {
        public DateRangeButtonsFilterEditor()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        public DateRangeFilterViewModel ViewModel
        {
            get { return (DateRangeFilterViewModel)DataContext; }
        }

        public void btn_OnUncheck(object sender, RoutedEventArgs e)
        {
            var btn = (ToggleButton)sender;
            var scope = FocusManager.GetFocusScope(btn);
            var focus = FocusManager.GetFocusedElement(scope);
            btn.Focus();

            Dispatcher.BeginInvoke(new Action(() => focus.Focus()), System.Windows.Threading.DispatcherPriority.Background);
        }
    }
}
