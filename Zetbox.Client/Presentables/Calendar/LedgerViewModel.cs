namespace Kistl.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class LedgerViewModel
    {
        private List<LedgerItemViewModel> _LedgerItems;
        public IEnumerable<LedgerItemViewModel> LedgerItems
        {
            get
            {
                if (_LedgerItems == null)
                {
                    _LedgerItems = new List<LedgerItemViewModel>();
                    for (int i = 0; i < 24; i++)
                    {
                        _LedgerItems.Add(new LedgerItemViewModel(i));
                    }
                }
                return _LedgerItems;
            }
        }
    }
}
