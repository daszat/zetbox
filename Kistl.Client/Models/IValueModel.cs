
namespace Kistl.Client.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.Client.Presentables;

    public interface IValueModel : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// Gets a value indicating whether or not to allow <value>null</value> as input.
        /// </summary>
        bool AllowNullInput { get; }

        /// <summary>
        /// Gets a label to display with the Value.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Gets a tooltip to display with the Value.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets a value indicating whether or not the property may be edited
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Clears the value of this Model. After calling this method the value should be <value>null</value> or "empty".
        /// </summary>
        void ClearValue();

    }

    public interface IValueModel<TValue> : IValueModel
    {
        TValue Value { get; set; }
    }

    public interface IEnumerationValueModel : IValueModel<int?>
    {
        Enumeration Enumeration { get; }
    }

    public interface IDateTimeValueModel : IValueModel<DateTime?>
    {
        DateTimeStyles DateTimeStyle { get; }
    }

    public interface IObjectReferenceValueModel : IValueModel<IDataObject>
    {
        ObjectClass ReferencedClass { get; }
    }


    public interface IObjectCollectionValueModel<TCollection> : IValueModel<TCollection>, INotifyCollectionChanged
    {
        ObjectClass ReferencedClass { get; }
        RelationEnd RelEnd { get; }
    }
}
