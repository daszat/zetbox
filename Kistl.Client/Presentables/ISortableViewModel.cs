
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;

    public interface ISortableViewModel
    {
        void Sort(string propName, ListSortDirection direction);
        ListSortDirection SortDirection { get; }
    }
}
