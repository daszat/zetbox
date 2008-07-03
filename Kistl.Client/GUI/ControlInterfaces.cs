using System;
using System.Collections.Generic;

using Kistl.API;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace Kistl.GUI
{
    /// <summary>
    /// This interfaces contains properties that each Control has to provide.
    /// </summary>
    public interface IBasicControl
    {
        /// <summary>
        /// A short label describing the contents of this control.
        /// </summary>
        string ShortLabel { get; set; }
        /// <summary>
        /// A longer description of the contents of this control. 
        /// This is a suitable text for tooltips.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The preferred display size of this control.
        /// </summary>
        FieldSize Size { get; set; }
    }

    /// <summary>
    /// A control that displays a specific value
    /// </summary>
    /// <typeparam name="TYPE">the Type of the value</typeparam>
    public interface IValueControl<TYPE> : IBasicControl
    {
        /// <summary>
        /// The actually displayed value. Setting the Value here does not count 
        /// as user-input.
        /// 
        /// If you want to be notified of all changes of the value, use the 
        /// appropriate events on the model.
        /// </summary>
        TYPE Value { get; set; }

        /// <summary>
        /// Whether the displayed value is valid. This is set by the Presenter, 
        /// when Value changes.
        /// </summary>
        bool IsValidValue { get; set; }

        /// <summary>
        /// This event is triggered after UserInput. The Presenter will then 
        /// fetch the Value and do Validity checks.
        /// </summary>
        event EventHandler UserInput;
    }

    /// <summary>
    /// a control that displays a reference to a IDataObject.
    /// </summary>
    /// <typeparam name="TYPE"></typeparam>
    // TODO: perhaps better called "ISingleSelectControl"?
    public interface IReferenceControl : IValueControl<IDataObject>
    {
        /// <summary>
        /// The Type of the listed objects
        /// </summary>
        Type ObjectType { get; set; }
        IList<IDataObject> ItemsSource { get; set; }
    }

    /// <summary>
    /// a control that displays a list of references to IDataObjects.
    /// </summary>
    /// <typeparam name="TYPE"></typeparam>
    public interface IReferenceListControl : IValueControl<ObservableCollection<IDataObject>>
    {
        /// <summary>
        /// The Type of the listed objects
        /// </summary>
        Type ObjectType { get; set; }
        IList<IDataObject> ItemsSource { get; set; }

        /// <summary>
        /// [optional]
        /// Is fired when the user wants to add an object to this list. The presenter
        /// will query the user for the object to add and perform the addition on the 
        /// model. The control receives the appropriate change event from the value.
        /// </summary>
        /// Highly sophisticated or specialised controls like ASP.NET can avoid firing
        /// this event and implement the appropriate logic themselves.
        /// 
        /// TODO: IList doesn't implement change events, therefore the presenter will 
        /// just reset the Value. A better implementation would fire fine-grained 
        /// add/delete Events instead.
        event EventHandler UserAddRequest;
    }

    /// <summary>
    /// The "master" control handling a complete Object
    /// </summary>
    public interface IObjectControl : IBasicControl
    {
        Kistl.API.IDataObject Value { get; set; }
        /// <summary>
        /// Is fired when the user requests that the object should be saved
        /// </summary>
        event EventHandler UserSaveRequest;
        /// <summary>
        /// Is fired when the user requests that the object should be deleted
        /// </summary>
        event EventHandler UserDeleteRequest;
    }


    /// <summary>
    /// Possible sizes for controls. These are measured realtively 
    /// to total horizontal space up to a useful (renderer-, medium- 
    /// and user-dependant maximum)
    /// </summary>
    public enum FieldSize
    {
        OneThird,
        Half,
        TwoThird,
        Full,
        FitContent
    }

}
