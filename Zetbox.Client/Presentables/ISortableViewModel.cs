
namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;

    public interface ISortableViewModel : INotifyPropertyChanged
    {
        void Sort(string propName, ListSortDirection direction);
        string SortProperty { get; }
        ListSortDirection SortDirection { get; }
    }
}
