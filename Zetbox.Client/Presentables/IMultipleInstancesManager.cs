using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.Client.Presentables
{
    // TODO: Make that easier
    public interface IMultipleInstancesManager
    {
        ViewModel SelectedItem { get; set; }
        void AddItem(ViewModel mdl);
    }
}
