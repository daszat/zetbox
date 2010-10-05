using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Kistl.Client.Presentables.ValueViewModels
{
    public interface IValueViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets a value indicating whether or not the property has a value.
        /// </summary>
        /// <seealso cref="IsNull"/>
        bool HasValue { get; }

        /// <summary>
        /// Gets a value indicating whether or not the property is null.
        /// </summary>
        /// <seealso cref="HasValue"/>
        bool IsNull { get; }

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
        string ToolTip { get; }

        /// <summary>
        /// Gets a value indicating whether or not the property may be edited
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Clears the value of this Model. After calling this method the value should be <value>null</value> or "empty".
        /// </summary>
        void ClearValue();

        ICommand ClearValueCommand { get; }
    }

    /// <summary>
    /// A Model describing a read-only value of type <typeparamref name="TValue"/>, usually read from a property or a method return value.
    /// </summary>
    /// <typeparam name="TValue">the type of the presented value</typeparam>
    public interface IValueViewModel<TValue>
        : IValueViewModel
    {
        /// <summary>
        /// Gets or sets the value of this model.
        /// </summary>
        TValue Value { get; set; }
    }

    /// <summary>
    /// WPF is not able to bind to a explicit implemented interface
    /// </summary>
    public interface IFormattedValueViewModel : IValueViewModel
    {
        string FormattedValue { get; set; }
    }
}
