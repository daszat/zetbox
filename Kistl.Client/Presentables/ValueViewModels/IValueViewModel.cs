
namespace Kistl.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Kistl.API.Utils;
    
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
        /// Gets or sets a value indicating whether or not the property may be edited
        /// </summary>
        bool IsReadOnly { get; set; }

        /// <summary>
        /// Clears the value of this Model. After calling this method the value should be <value>null</value> or "empty".
        /// </summary>
        void ClearValue();

        ICommandViewModel ClearValueCommand { get; }

        /// <summary>
        /// Returns the untyped value.
        /// </summary>
        object UntypedValue { get; }
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
        void Focus();
        void Blur();

        string FormattedValue { get; set; }
    }

    public interface IBaseValueCollectionViewModel<TElement, TList>
        : IValueViewModel<TList>
    {
        /// <summary>
        /// Gets a value whether or not this list has a persistent order. 
        /// </summary>
        /// <remarks>
        /// While lists on the client always have a definite order, the order 
        /// is only persisted if the underlying datamodel actually supports 
        /// this.
        /// </remarks>
        bool HasPersistentOrder { get; }

        /// <summary>
        /// Adds the given item to the underlying value. Triggers <see cref="INotifyCollectionChanged.CollectionChanged"/>
        /// on the underlying <see cref="IValueViewModel{TValue}.Value"/> property when the change has propagated.
        /// </summary>
        void AddItem(TElement item);

        /// <summary>
        /// Remove the given item from the underlying value. Triggers <see cref="INotifyCollectionChanged.CollectionChanged"/>
        /// on the underlying <see cref="IValueViewModel{TValue}.Value"/> property when the change has propagated.
        /// </summary>
        void RemoveItem(TElement item);

        /// <summary>
        /// Permanentely delete the given item from the data store.
        /// Triggers <see cref="INotifyCollectionChanged.CollectionChanged"/> on the underlying <see cref="IValueViewModel{TValue}.Value"/> property when the change has propagated.
        /// </summary>
        void DeleteItem(TElement item);

        /// <summary>
        /// Activates the item for the user to edit.
        /// </summary>
        /// <param name="item">the item to activate</param>
        /// <param name="activate">whether or not to raise the item to the top</param>
        void ActivateItem(TElement item, bool activate);

        /// <summary>
        /// Stores the currently selected item of this list. 
        /// </summary>
        TElement SelectedItem { get; set; }
    }

    public interface IValueCollectionViewModel<TElement, TList>
        : IBaseValueCollectionViewModel<TElement, TList>
    {
        void Sort(string propName, ListSortDirection direction);
    }

    public interface IValueListViewModel<TElement, TList>
        : IBaseValueCollectionViewModel<TElement, TList>
    {
        /// <summary>
        /// Moves the given item one item up in the list. Triggers <see cref="INotifyCollectionChanged.CollectionChanged"/>
        /// on the underlying <see cref="IValueViewModel{TValue}.Value"/> property when the change has propagated.
        /// </summary>
        void MoveItemUp(TElement item);

        /// <summary>
        /// Moves the given item one item down in the list. Triggers <see cref="INotifyCollectionChanged.CollectionChanged"/>
        /// on the underlying <see cref="IValueViewModel{TValue}.Value"/> property when the change has propagated.
        /// </summary>
        void MoveItemDown(TElement item);

    }
}
