using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Client.Presentables
{
    // TODO: Make that easier
    public interface IMultipleInstancesManager
    {
        ViewModel SelectedItem { get; set; }
        void HistoryTouch(ViewModel mdl);
    }
}
