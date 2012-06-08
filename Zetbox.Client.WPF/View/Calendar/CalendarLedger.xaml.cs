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
using Zetbox.Client.Presentables.Calendar;

namespace Zetbox.Client.WPF.View.Calendar
{
    /// <summary>
    /// Interaction logic for CalendarLedger.xaml
    /// </summary>
    /// <remarks>Based on http://www.codeproject.com/KB/WPF/WPFOutlookCalendar.aspx </remarks>
    public partial class CalendarLedger : UserControl
    {
        public CalendarLedger()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
            this.DataContext = Ledger;
        }

        private LedgerViewModel _Ledger;
        public LedgerViewModel Ledger
        {
            get
            {
                if (_Ledger == null)
                {
                    _Ledger = new LedgerViewModel();
                }
                return _Ledger;
            }
        }
    }
}
