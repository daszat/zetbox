using System;
using System.Collections.Generic;

using Kistl.API;

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
        event /* TODO UserInput<TYPE>*/EventHandler UserInput;
    }

    /// <summary>
    /// a control that displays a reference to a value.
    /// </summary>
    /// <typeparam name="TYPE"></typeparam>
    // TODO: perhaps better called "ISingleSelectControl"?
    public interface IReferenceControl<TYPE> : IValueControl<TYPE>
    {
        /// <summary>
        /// The ObjectType of the listed objects
        /// </summary>
        ObjectType ObjectType { get; set; }
        IList<IDataObject> ItemsSource { get; set; }
    }

    public interface IObjectReferenceControl : IReferenceControl<IDataObject>
    {
    }

    // TODO: perhaps better called "IMultiSelectControl"?
    public interface IObjectListControl : IReferenceControl<IList<IDataObject>>
    {
    }

    /// <summary>
    /// The "master" control handling a complete Object
    /// </summary>
    public interface IObjectControl : IBasicControl
    {
        Kistl.API.IDataObject Value { get; set; }
        event /* TODO UserInput<Kistl.API.IDataObject>*/EventHandler UserInput;
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
