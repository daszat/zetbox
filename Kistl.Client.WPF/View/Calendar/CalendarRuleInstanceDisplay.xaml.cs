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
using Kistl.Client.Presentables.Calendar;

namespace Kistl.Client.WPF.View.Calendar
{
    /// <summary>
    /// Interaction logic for CalendarRuleInstanceDisplay.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class CalendarRuleInstanceDisplay : UserControl, IHasViewModel<CalendarRuleInstanceViewModel>
    {
        public CalendarRuleInstanceDisplay()
        {
            InitializeComponent();
        }

        public CalendarRuleInstanceViewModel ViewModel
        {
            get { return (CalendarRuleInstanceViewModel)DataContext; }
        }
    }
}
