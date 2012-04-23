
namespace Kistl.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.GUI;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.GUI;
    using Kistl.API.Utils;

    public enum ValueViewModelState
    {
        Blurred_UnmodifiedValue = 1,
        ImplicitFocus_WritingModel,
        ImplicitFocus_PartialUserInput,
        Focused_UnmodifiedValue,
        Focused_WritingModel,
        Focused_PartialUserInput,
        Blurred_PartialUserInput,
    }

    public abstract class BaseValueViewModel : ViewModel, IValueViewModel, IFormattedValueViewModel, IDataErrorInfo, ILabeledViewModel
    {
        public new delegate BaseValueViewModel Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public static BaseValueViewModel Fetch(IViewModelFactory f, IKistlContext dataCtx, ViewModel parent, Property prop, IValueModel mdl)
        {
            return (BaseValueViewModel)dataCtx.GetViewModelCache(f.PerfCounter).LookupOrCreate(mdl, () => f.CreateViewModel<BaseValueViewModel.Factory>(prop).Invoke(dataCtx, parent, mdl));
        }

        public static BaseValueViewModel Fetch(IViewModelFactory f, IKistlContext dataCtx, ViewModel parent, Method m, IValueModel mdl)
        {
            return (BaseValueViewModel)dataCtx.GetViewModelCache(f.PerfCounter).LookupOrCreate(mdl, () => f.CreateViewModel<BaseValueViewModel.Factory>(m).Invoke(dataCtx, parent, mdl));
        }

        public static BaseValueViewModel Fetch(IViewModelFactory f, IKistlContext dataCtx, ViewModel parent, ViewModelDescriptor desc, IValueModel mdl)
        {
            return (BaseValueViewModel)dataCtx.GetViewModelCache(f.PerfCounter).LookupOrCreate(mdl, () => f.CreateViewModel<BaseValueViewModel.Factory>(desc).Invoke(dataCtx, parent, mdl));
        }

        public static BaseValueViewModel Fetch(IViewModelFactory f, IKistlContext dataCtx, ViewModel parent, BaseParameter param, IValueModel mdl)
        {
            return (BaseValueViewModel)dataCtx.GetViewModelCache(f.PerfCounter).LookupOrCreate(mdl, () => f.CreateViewModel<BaseValueViewModel.Factory>(param).Invoke(dataCtx, parent, mdl));
        }

        public BaseValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent)
        {
            this.ValueModel = mdl;
            dataCtx.IsElevatedModeChanged += new EventHandler(Context_IsElevatedModeChanged);
        }

        void Context_IsElevatedModeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("IsReadOnly");
            OnPropertyChanged("Highlight");
        }

        public IValueModel ValueModel { get; private set; }

        public override string Name
        {
            get { return ValueModel.Label; }
        }

        public override App.GUI.ControlKind RequestedKind
        {
            get
            {
                return base.RequestedKind ?? ValueModel.RequestedKind;
            }
            set
            {
                base.RequestedKind = value;
            }
        }

        public override Highlight Highlight
        {
            get
            {
                if (DataContext.IsElevatedMode && !IsReadOnly) return Highlight.Bad; // May be true for calculated properties                
                if (Parent != null && Parent.Highlight != null) return Parent.Highlight;
                if (!IsEnabled || IsReadOnly) return Highlight.Deactivated;
                return null;
            }
        }

        public abstract void Focus();
        public abstract void Blur();

        #region Utilities and UI callbacks
        protected virtual void NotifyValueChanged()
        {
            OnPropertyChanged("Value");
            OnPropertyChanged("Highlight");
            OnFormattedValueChanged();
            OnPropertyChanged("IsNull");
            OnPropertyChanged("HasValue");
            OnPropertyChanged("Name");
        }
        #endregion

        #region IValueViewModel Members

        public abstract bool HasValue { get; }

        public virtual bool IsNull
        {
            get { return !HasValue; }
        }

        public virtual bool AllowNullInput
        {
            get { return ValueModel.AllowNullInput; }
        }

        public virtual string Label
        {
            get { return ValueModel.Label; }
        }

        public virtual string ToolTip
        {
            get { return ValueModel.Description; }
        }

        private bool _IsReadOnly;
        public virtual bool IsReadOnly
        {
            get
            {
                if (DataContext.IsElevatedMode && !ValueModel.IsReadOnly) return false; // If in elevated mode the model still say it's readonly, we should beleve it.
                return ValueModel.IsReadOnly || _IsReadOnly;
            }
            set
            {
                if (_IsReadOnly != value)
                {
                    _IsReadOnly = value;
                    OnPropertyChanged("IsReadOnly");
                    OnPropertyChanged("Highlight");
                }
            }
        }

        public virtual void ClearValue()
        {
            ValueModel.ClearValue();
        }

        private ICommandViewModel _ClearValueCommand = null;
        public virtual ICommandViewModel ClearValueCommand
        {
            get
            {
                if (_ClearValueCommand == null)
                {
                    _ClearValueCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        ValueViewModelResources.ClearValueCommand_Name,
                        ValueViewModelResources.ClearValueCommand_Tooltip,
                        () => ClearValue(),
                        () => AllowNullInput && !DataContext.IsReadonly && !IsReadOnly,
                        null);
                    //_ClearValueCommand.Icon = FrozenContext.FindPersistenceObject<Icon>(NamedObjects.
                }
                return _ClearValueCommand;
            }
        }

        public object UntypedValue
        {
            get
            {
                return ValueModel.GetUntypedValue();
            }
        }

        #endregion

        #region IFormattedValueViewModel Members

        public abstract string FormattedValue
        {
            get;
            set;
        }

        protected virtual void OnFormattedValueChanged()
        {
            OnPropertyChanged("FormattedValue");
        }

        #endregion

        #region IDataErrorInfo Members

        public virtual string Error
        {
            get
            {
                return ValueModel.Error;
            }
        }

        public virtual string this[string columnName]
        {
            get
            {
                return ValueModel[columnName];
            }
        }

        #endregion

        #region ILabeledViewModel Members
        public ViewModel Model
        {
            get
            {
                return this;
            }
        }

        public bool Required
        {
            get { return !this.AllowNullInput; }
        }

        #endregion
    }

    public class StateChangedEventArgs
        : EventArgs
    {
        public StateChangedEventArgs(ValueViewModelState oldState, ValueViewModelState newState)
        {
            this.OldState = oldState;
            this.NewState = newState;
        }

        public ValueViewModelState OldState { get; private set; }
        public ValueViewModelState NewState { get; private set; }

        public override string ToString()
        {
            return String.Format("StateChange {0} -> {1}", OldState, NewState);
        }
    }

    public abstract class ValueViewModel<TValue, TModel> : BaseValueViewModel, IValueViewModel<TValue>
    {
        protected class ParseResult<TResultValue>
        {
            public bool HasErrors { get { return !String.IsNullOrEmpty(Error); } }
            public string Error { get; set; }
            public TResultValue Value { get; set; }
        }

        public new delegate ValueViewModel<TValue, TModel> Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public ValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
            this.ValueModel = (IValueModel<TModel>)mdl;
            this.State = ValueViewModelState.Blurred_UnmodifiedValue;
            this.ValueModel.PropertyChanged += Model_PropertyChanged;
        }

        void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnValueModelPropertyChanged(e);
        }

        protected virtual void OnValueModelPropertyChanged(PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Value":
                    // MC1 && MC2
                    if (State == ValueViewModelState.Blurred_UnmodifiedValue)
                    {
                        OnModelChanged();
                    }
                    break;
                case "Error":
                    OnErrorChanged();
                    break;
            }
        }

        public new IValueModel<TModel> ValueModel { get; private set; }

        #region IValueViewModel<TValue> Members

        protected abstract TValue GetValueFromModel();

        /// <summary>
        /// Writes the specified value to the model, circumventing the state machine.
        /// </summary>
        protected abstract void SetValueToModel(TValue value);

        public virtual TValue Value
        {
            get
            {
                return GetValueFromModel();
            }
            set
            {
                if (State == ValueViewModelState.Focused_PartialUserInput)
                {
                    // replace partial user input with newly formatted input
                    _partialUserInput = FormatValue(value);
                    _partialUserInputError = null;
                }
                OnValidInput(_partialUserInput, value);
            }
        }

        #endregion

        public override bool HasValue
        {
            get { return ValueModel.Value != null; }
        }

        #region IFormattedValueViewModel Members

        private string _partialUserInputError;
        private string _partialUserInput;

        /// <summary>
        /// Parse the given string and return the parsed Value and/or Error.
        /// </summary>
        /// <remarks>
        /// ParseValue is allowed to be called in any state of the input state machine 
        /// and should not touch the ViewModel's internal state.
        /// </remarks>
        /// <param name="str">string to parse</param>
        /// <returns>The result of the parse.</returns>
        protected abstract ParseResult<TValue> ParseValue(string str);

        /// <summary>
        /// Formats the specified value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// FormatValue is allowed to be called in any state of the input state machine 
        /// and should not touch the ViewModel's internal state.
        /// </para>
        /// <para>
        /// The result of this function should be roundtrippable with ParseValue.
        /// </para>
        /// </remarks>
        /// <param name="value">value to format</param>
        /// <returns>The result of the formatting.</returns>
        protected virtual string FormatValue(TValue value)
        {
            return value != null ? value.ToString() : String.Empty;
        }

        public override string FormattedValue
        {
            get
            {
                switch (State)
                {
                    case ValueViewModelState.Blurred_UnmodifiedValue:
                    case ValueViewModelState.ImplicitFocus_WritingModel:
                        return FormatValue(this.GetValueFromModel());
                    case ValueViewModelState.Blurred_PartialUserInput:
                    case ValueViewModelState.ImplicitFocus_PartialUserInput:
                    case ValueViewModelState.Focused_PartialUserInput:
                    case ValueViewModelState.Focused_UnmodifiedValue:
                    case ValueViewModelState.Focused_WritingModel:
                        return _partialUserInput;
                    default:
                        throw new InvalidOperationException(string.Format("Unexpected State {0}", State));
                }
            }
            set
            {
                if (_partialUserInput != value)
                {
                    var oldPartialUserInputError = _partialUserInputError;
                    var parseResult = ParseValue(value);
                    if (parseResult.HasErrors)
                    {
                        OnPartialInput(value, parseResult.Error);
                    }
                    else if (!AllowNullInput && parseResult.Value == null)
                    {
                        OnPartialInput(value, "Wert muss gesetzt sein");
                    }
                    else
                    {
                        OnValidInput(value, parseResult.Value);
                    }

                    if (_partialUserInputError != oldPartialUserInputError)
                    {
                        OnErrorChanged();
                    }
                    // implicit via state machine
                    //OnFormattedValueChanged("FormattedValue");
                }
            }
        }

        protected virtual void OnErrorChanged()
        {
            OnPropertyChanged("Error");
            if (!string.IsNullOrEmpty(this.Error))
            {
                // Register with a IContextViewModel
                var ctxVmdl = ViewModelFactory.GetWorkspace(DataContext) as IContextViewModel;
                if (ctxVmdl != null)
                {
                    ctxVmdl.RegisterError(this);
                }
            }
        }

        #endregion

        #region State Machine

        private ValueViewModelState _state;
        protected ValueViewModelState State
        {
            get { return _state; }
            private set
            {
                if (_state != value)
                {
                    var oldState = _state;
                    _state = value;
                    OnStateChanged(oldState, value);
                }
            }
        }

        protected virtual void OnStateChanged(ValueViewModelState oldState, ValueViewModelState newState)
        {
            Logging.Client.DebugFormat("State Change of {0} from {1} -> {2}", this.GetType().Name, oldState, newState); // Do not log name, can be called through the constructor
            if (StateChanged != null && oldState != newState)
            {
                StateChanged(this, new StateChangedEventArgs(oldState, newState));
            }
        }

        public event StateChangedEventHandler StateChanged;
        public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs args);


        /// <summary>
        /// Part of the ValueViewModel state machine as described in the KistlGuide. 
        /// This method is called everytime the model changed and handles the MC1, MC2,
        /// and MC3 events.
        /// </summary>
        protected virtual void OnModelChanged()
        {
            Logging.Client.DebugFormat("Model Change on {0}.{1}", this.GetType().Name, this.Name);
            switch (State)
            {
                case ValueViewModelState.Blurred_UnmodifiedValue:
                    // MC1
                    NotifyValueChanged();
                    break;
                case ValueViewModelState.ImplicitFocus_WritingModel:
                    // ignore notifications from model while writing to it.
                    break;
                case ValueViewModelState.Focused_UnmodifiedValue:
                    Kistl.API.Utils.Logging.Client.WarnFormat("Model changed while {0} has focus; ignoring.", this.GetType().FullName);
                    break;
                // case F/WM
                // MC3
                //NotifyValueChanged();
                //State = F/UV;
                //break;
                default:
                    // MC2 
                    throw new InvalidOperationException(string.Format("Unexpected State {0}", State));
            }
        }

        /// <summary>
        /// Part of the ValueViewModel state machine as described in the KistlGuide. 
        /// This method is called everytime valid input is received and handles 
        /// the VI event.
        /// </summary>
        protected virtual void OnValidInput(string formattedValue, TValue value)
        {
            Logging.Client.DebugFormat("Received valid input [{0}] interpreted as [{1}]", formattedValue, value);
            switch (State)
            {
                case ValueViewModelState.Blurred_UnmodifiedValue:
                    try
                    {
                        State = ValueViewModelState.ImplicitFocus_WritingModel;
                        _partialUserInput = null;
                        _partialUserInputError = null;
                        SetValueToModel(value);
                        NotifyValueChanged();
                    }
                    finally
                    {
                        State = ValueViewModelState.Blurred_UnmodifiedValue;
                    }
                    break;
                case ValueViewModelState.ImplicitFocus_PartialUserInput:
                    try
                    {
                        State = ValueViewModelState.ImplicitFocus_WritingModel;
                        _partialUserInput = null;
                        _partialUserInputError = null;
                        SetValueToModel(value);
                        NotifyValueChanged();
                    }
                    finally
                    {
                        State = ValueViewModelState.Blurred_UnmodifiedValue;
                    }
                    break;
                case ValueViewModelState.Focused_PartialUserInput:
                case ValueViewModelState.Focused_UnmodifiedValue:
                    try
                    {
                        State = ValueViewModelState.Focused_WritingModel;
                        _partialUserInput = formattedValue;
                        _partialUserInputError = null;
                        SetValueToModel(value);
                        NotifyValueChanged();
                    }
                    finally
                    {
                        State = ValueViewModelState.Focused_UnmodifiedValue;
                    }
                    break;
                default:
                    throw new InvalidOperationException(string.Format("Unexpected State {0}", State));
            }
        }

        /// <summary>
        /// Part of the ValueViewModel state machine as described in the KistlGuide. 
        /// This method is called everytime partial input is received and handles 
        /// the PI event.
        /// </summary>
        protected virtual void OnPartialInput(string partialInput, string errorMessage)
        {
            Logging.Client.DebugFormat("Received partial input [{0}]; error message is [{1}]", partialInput, errorMessage);
            switch (State)
            {
                case ValueViewModelState.Blurred_UnmodifiedValue:
                case ValueViewModelState.ImplicitFocus_PartialUserInput:
                    try
                    {
                        _partialUserInput = partialInput;
                        _partialUserInputError = errorMessage;
                        OnFormattedValueChanged();
                    }
                    finally
                    {
                        State = ValueViewModelState.ImplicitFocus_PartialUserInput;
                    }
                    break;
                case ValueViewModelState.Focused_UnmodifiedValue:
                case ValueViewModelState.Focused_PartialUserInput:
                    try
                    {
                        _partialUserInput = partialInput;
                        _partialUserInputError = errorMessage;
                        OnFormattedValueChanged();
                    }
                    finally
                    {
                        State = ValueViewModelState.Focused_PartialUserInput;
                    }
                    break;
                case ValueViewModelState.Blurred_PartialUserInput:
                    // TODO: should start implicit focus
                    throw new InvalidOperationException("Rejecting new PartialInput while being blurred");
                default:
                    throw new InvalidOperationException(string.Format("Unexpected State {0}", State));
            }
        }

        /// <summary>
        /// Part of the ValueViewModel state machine as described in the KistlGuide. 
        /// This method is called everytime the control receives focus and handles 
        /// the F event.
        /// </summary>
        protected virtual void OnFocus()
        {
            switch (State)
            {
                case ValueViewModelState.Blurred_UnmodifiedValue:
                    try
                    {
                        _partialUserInput = FormatValue(this.GetValueFromModel());
                    }
                    finally
                    {
                        State = ValueViewModelState.Focused_UnmodifiedValue;
                    }
                    break;
                case ValueViewModelState.ImplicitFocus_PartialUserInput: // confused with focus
                case ValueViewModelState.Focused_PartialUserInput: // confused with focus
                case ValueViewModelState.Blurred_PartialUserInput:
                    State = ValueViewModelState.Focused_PartialUserInput;
                    break;
                case ValueViewModelState.Focused_UnmodifiedValue:
                    // Do nothing and stay
                    // confused with focus
                    break;
                default:
                    throw new InvalidOperationException(string.Format("Unexpected State {0}", State));
            }
        }

        /// <summary>
        /// Part of the ValueViewModel state machine as described in the KistlGuide. 
        /// This method is called everytime the control looses focus and handles 
        /// the B1,and B2 events.
        /// </summary>
        protected virtual void OnBlur()
        {
            switch (State)
            {
                case ValueViewModelState.Focused_UnmodifiedValue:
                    State = ValueViewModelState.Blurred_UnmodifiedValue;
                    OnFormattedValueChanged();
                    break;
                case ValueViewModelState.Blurred_PartialUserInput: // confused with focus
                case ValueViewModelState.Focused_PartialUserInput:
                    State = ValueViewModelState.Blurred_PartialUserInput;
                    break;
                case ValueViewModelState.Blurred_UnmodifiedValue:
                    // Do nothing and stay
                    // confused with focus
                    break;
                default:
                    throw new InvalidOperationException(string.Format("Unexpected State {0}", State));
            }
        }

        public override void Blur()
        {
            OnBlur();
        }

        public override void Focus()
        {
            OnFocus();
        }

        #endregion

        #region IDataErrorInfo Members

        public override string Error
        {
            get
            {
                var baseError = base.Error;
                if (string.IsNullOrEmpty(baseError))
                {
                    return _partialUserInputError;
                }
                else
                {
                    return baseError + "\n" + _partialUserInputError;
                }
            }
        }

        public override string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "FormattedValue":
                        return Error;
                    default:
                        return base[columnName];
                }
            }
        }

        #endregion
    }

    public class NullableStructValueViewModel<TValue> : ValueViewModel<Nullable<TValue>, Nullable<TValue>>
        where TValue : struct
    {
        public new delegate NullableStructValueViewModel<TValue> Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public NullableStructValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
        }

        protected override ParseResult<TValue?> ParseValue(string str)
        {
            try
            {
                var parsedValue = String.IsNullOrEmpty(str) ? null : (Nullable<TValue>)System.Convert.ChangeType(str, typeof(TValue));
                if (!AllowNullInput && parsedValue == null)
                {
                    return new ParseResult<TValue?>()
                    {
                        Error = string.Format("{0}: {1}", this.Label, ValueViewModelResources.ErrorEmptyValue)
                    };
                }
                return new ParseResult<TValue?>()
                {
                    Value = parsedValue
                };
            }
            catch
            {
                return new ParseResult<TValue?>()
                {
                    Error = string.Format("{0}: {1}", this.Label, ValueViewModelResources.ErrorConvertingType)
                };
            }
        }

        protected override TValue? GetValueFromModel()
        {
            return ValueModel.Value;
        }

        protected override void SetValueToModel(TValue? value)
        {
            ValueModel.Value = value;
        }
    }

    public class ClassValueViewModel<TValue> : ValueViewModel<TValue, TValue>
        where TValue : class
    {
        public new delegate ClassValueViewModel<TValue> Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public ClassValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
        }

        protected override ParseResult<TValue> ParseValue(string str)
        {
            try
            {
                return new ParseResult<TValue>()
                {
                    Value = String.IsNullOrEmpty(str) ? null : (TValue)System.Convert.ChangeType(str, typeof(TValue))
                };
            }
            catch
            {
                return new ParseResult<TValue>()
                {
                    Error = string.Format("{0}: {1}", this.Label, ValueViewModelResources.ErrorConvertingType)
                };
            }
        }

        protected override TValue GetValueFromModel()
        {
            return ValueModel.Value;
        }

        protected override void SetValueToModel(TValue value)
        {
            ValueModel.Value = value;
        }
    }

    public class EnumerationValueViewModel : NullableStructValueViewModel<int>
    {
        public new delegate EnumerationValueViewModel Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public EnumerationValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
            this.EnumModel = (IEnumerationValueModel)mdl;
        }

        public IEnumerationValueModel EnumModel { get; private set; }

        private ReadOnlyCollection<KeyValuePair<int?, string>> _PossibleValues = null;
        public ReadOnlyCollection<KeyValuePair<int?, string>> PossibleValues
        {
            get
            {
                if (_PossibleValues == null)
                {
                    this._PossibleValues = new ReadOnlyCollection<KeyValuePair<int?, string>>(
                        new[] { new KeyValuePair<int?, string>(null, String.Empty) }
                        .Concat(EnumModel.GetEntries().Select(kv => new KeyValuePair<int?, string>(kv.Key, kv.Value)))
                        .ToList()
                    );
                }
                return _PossibleValues;
            }
        }

        protected override void NotifyValueChanged()
        {
            base.NotifyValueChanged();
            OnPropertyChanged("SelectedItem");
        }

        public KeyValuePair<int?, string> SelectedItem
        {
            get
            {
                return PossibleValues.FirstOrDefault(i => i.Key == Value);
            }
            set
            {
                // Value = value.Key;
                FormattedValue = value.Value;
            }
        }

        protected override string FormatValue(int? value)
        {
            if (value == null) return string.Empty;
            // This hurts, but looks funny
            // Don't die on invalid values
            return PossibleValues.FirstOrDefault(key => key.Key == value.Value).Value;
        }

        protected override ParseResult<int?> ParseValue(string str)
        {
            int? value = null;
            string error = null;
            if (string.IsNullOrEmpty(str))
            {
                value = null;
            }
            else
            {
                // Don't die on invalid values
                var item = PossibleValues.FirstOrDefault(v => string.Compare(v.Value, str, true) == 0);
                if (item.Key != null)
                {
                    value = item.Key.Value;
                }
                else
                {
                    error = string.Format("{0}: {1}", this.Label, ValueViewModelResources.ErrorConvertingType);
                }
            }

            return new ParseResult<int?>()
            {
                Value = value,
                Error = error
            };
        }

        public override string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "SelectedItem":
                        return Error;
                    default:
                        return base[columnName];
                }
            }
        }
    }

    public class MultiLineStringValueViewModel
        : ClassValueViewModel<string>
    {
        public new delegate MultiLineStringValueViewModel Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public MultiLineStringValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Value")
            {
                base.OnPropertyChanged("ShortText");
            }
        }

        private int _ShortTextLength = 50;
        public int ShortTextLength
        {
            get
            {
                return _ShortTextLength;
            }
            set
            {
                if (_ShortTextLength != value)
                {
                    _ShortTextLength = value;
                    OnPropertyChanged("ShortTextLength");
                    OnPropertyChanged("ShortText");
                }
            }
        }

        public string ShortText
        {
            get
            {
                if (!string.IsNullOrEmpty(Value) && Value.Length > ShortTextLength)
                {
                    return Value.Replace("\r", "").Replace('\n', ' ').Substring(0, ShortTextLength) + "...";
                }
                else
                {
                    return Value;
                }
            }
        }

        private ICommandViewModel _EditCommand = null;
        public ICommandViewModel EditCommand
        {
            get
            {
                if (_EditCommand == null)
                {
                    _EditCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        ValueViewModelResources.EditCommand_Name,
                        ValueViewModelResources.EditCommand_Tooltip,
                        () => Edit(),
                        () => !IsReadOnly,
                        null);
                }
                return _EditCommand;
            }
        }

        public void Edit()
        {
            ViewModelFactory.ShowDialog(
                    ViewModelFactory.CreateViewModel<MultiLineEditorDialogViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        Value,
                        (v) => Value = v));
        }
    }

    public class NullableGuidPropertyViewModel : NullableStructValueViewModel<Guid>
    {
        public new delegate NullableGuidPropertyViewModel Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public NullableGuidPropertyViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
        }

        protected override ParseResult<Guid?> ParseValue(string str)
        {
            Guid? value = null;
            string error = null;

            try
            {
                value = new Guid(str);
            }
            catch
            {
                error = string.Format("{0}: {1}", this.Label, ValueViewModelResources.ErrorConvertingType);
            }

            return new ParseResult<Guid?>()
            {
                Value = value,
                Error = error
            };
        }
    }

    public class NullableDecimalPropertyViewModel : NullableStructValueViewModel<decimal>
    {
        public new delegate NullableDecimalPropertyViewModel Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public NullableDecimalPropertyViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
            DecimalModel = (IDecimalValueModel)mdl;
        }

        public IDecimalValueModel DecimalModel { get; private set; }

        protected override ValueViewModel<decimal?, decimal?>.ParseResult<decimal?> ParseValue(string str)
        {
            var result = base.ParseValue(str);
            if (!result.HasErrors && result.Value.HasValue && DecimalModel.Precision.HasValue)
            {
                var maxMagnitude = DecimalModel.Precision.Value - (DecimalModel.Scale ?? 0);
                var maxValue = (decimal)Math.Pow(10, maxMagnitude);
                if (Math.Abs(result.Value.Value) > maxValue)
                {
                    result.Error = string.Format(ValueViewModelResources.DecimalOutOfRange, result.Value.Value, maxMagnitude);
                }
            }
            return result;
        }

        protected override string FormatValue(decimal? value)
        {
            if (DecimalModel.Scale.HasValue && value.HasValue)
            {
                var format = string.Format("N{0}", DecimalModel.Scale.Value);
                return value.Value.ToString(format);
            }
            return base.FormatValue(value);
        }
    }

    public class NullableDateTimePropertyViewModel
        : NullableStructValueViewModel<DateTime>
    {
        private class NullableTimePartPropertyViewModel : NullableStructValueViewModel<TimeSpan>
        {
            public new delegate NullableTimePartPropertyViewModel Factory(IKistlContext dataCtx, NullableDateTimePropertyViewModel parent, IValueModel mdl);

            public NullableTimePartPropertyViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, NullableDateTimePropertyViewModel parent, IValueModel mdl)
                : base(dependencies, dataCtx, parent, mdl)
            {
            }

            public new NullableDateTimePropertyViewModel Parent
            {
                get
                {
                    return (NullableDateTimePropertyViewModel)base.Parent;
                }
            }

            protected override string FormatValue(TimeSpan? value)
            {
                return value == null ? String.Empty : String.Format("{0:00}:{1:00}", value.Value.Hours, value.Value.Minutes);
            }

            protected override ParseResult<TimeSpan?> ParseValue(string str)
            {
                ParseResult<TimeSpan?> result = new ParseResult<TimeSpan?>();
                if (AllowNullInput && string.IsNullOrEmpty(str))
                {
                    result.Value = null;
                }
                else
                {
                    MatchCollection matches;
                    if (null != (matches = Regex.Matches(str, @"^(\d?\d):?(\d\d)$")))
                    {
                        int hours, minutes;
                        if (matches.Count == 1
                            && matches[0].Groups.Count == 3
                            && Int32.TryParse(matches[0].Groups[1].Captures[0].Value, out hours)
                            && Int32.TryParse(matches[0].Groups[2].Captures[0].Value, out minutes)
                            && hours >= 0 && hours < 24
                            && minutes >= 0 && minutes < 60)
                        {
                            result.Value = new TimeSpan(hours, minutes, 0);
                        }
                        else
                        {
                            result.Error = string.Format("{0}: {1}", this.Label, ValueViewModelResources.ErrorConvertingType);
                        }
                    }
                    else
                    {
                        result.Error = string.Format("{0}: {1}", this.Label, ValueViewModelResources.ErrorConvertingType);
                    }
                }
                return result;
            }

            protected override TimeSpan? GetValueFromModel()
            {
                var val = Parent.GetValueFromModel();
                if (val == null) return null;
                return val.Value.TimeOfDay;
            }

            protected override void SetValueToModel(TimeSpan? value)
            {
                if (Parent.GetValueFromModel() == null && value == null)
                {
                    Parent.SetValueToModel(null);
                }
                else
                {
                    var date = (Parent.GetValueFromModel() ?? DateTime.MinValue).Date;
                    if (date == DateTime.MinValue.Date)
                    {
                        if (value == null || value == TimeSpan.Zero)
                        {
                            Parent.SetValueToModel(null);
                        }
                        else
                        {
                            Parent.SetValueToModel(date + (value ?? TimeSpan.Zero));
                        }
                        Parent.OnPropertyChanged("TimePart");
                        Parent.OnPropertyChanged("TimePartString");
                    }
                    else
                    {
                        Parent.SetValueToModel(date + (value ?? TimeSpan.Zero));
                    }
                }
            }

            protected override void OnErrorChanged()
            {
                base.OnErrorChanged();
                Parent.OnErrorChanged();
            }
        }

        private class NullableDatePartPropertyViewModel : NullableStructValueViewModel<DateTime>
        {
            public new delegate NullableDatePartPropertyViewModel Factory(IKistlContext dataCtx, NullableDateTimePropertyViewModel parent, IValueModel mdl);

            public NullableDatePartPropertyViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, NullableDateTimePropertyViewModel parent, IValueModel mdl)
                : base(dependencies, dataCtx, parent, mdl)
            {
            }

            public new NullableDateTimePropertyViewModel Parent
            {
                get
                {
                    return (NullableDateTimePropertyViewModel)base.Parent;
                }
            }

            protected override ParseResult<DateTime?> ParseValue(string str)
            {
                var val = base.ParseValue(str);
                if (!AllowNullInput && val.Value == DateTime.MinValue)
                {
                    val.Error = ValueViewModelResources.DateIsRequired;
                }

                return val;
            }

            protected override string FormatValue(DateTime? value)
            {
                return value == null ? String.Empty : value.Value.ToShortDateString();
            }

            protected override DateTime? GetValueFromModel()
            {
                var modelValue = Parent.GetValueFromModel();
                if (modelValue.HasValue)
                {
                    var val = modelValue.Value;
                    if (val.Date == DateTime.MinValue.Date)
                    {
                        return null;
                    }
                    else
                    {
                        return val.Date;
                    }
                }
                else
                {
                    return null;
                }
            }

            protected override void SetValueToModel(DateTime? value)
            {
                if (Parent.GetValueFromModel() == null && value == null)
                {
                    Parent.SetValueToModel(null);
                }
                else
                {
                    var time = (Parent.GetValueFromModel() ?? DateTime.MinValue).TimeOfDay;
                    if (time == DateTime.MinValue.TimeOfDay)
                    {
                        if (value == DateTime.MinValue)
                        {
                            Parent.SetValueToModel(null);
                        }
                        else
                        {
                            Parent.SetValueToModel(value);
                        }
                        Parent.OnPropertyChanged("TimePart");
                        Parent.OnPropertyChanged("TimePartString");
                    }
                    else
                    {
                        Parent.SetValueToModel((value ?? DateTime.MinValue) + time);
                    }
                }
            }

            protected override void OnErrorChanged()
            {
                base.OnErrorChanged();
                Parent.OnErrorChanged();
            }
        }

        public new delegate NullableDateTimePropertyViewModel Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public NullableDateTimePropertyViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
            DateTimeModel = (IDateTimeValueModel)mdl;
            _timePartViewModel = ViewModelFactory.CreateViewModel<NullableTimePartPropertyViewModel.Factory>().Invoke(DataContext, this, mdl);
            _datePartViewModel = ViewModelFactory.CreateViewModel<NullableDatePartPropertyViewModel.Factory>().Invoke(DataContext, this, mdl);
        }

        public IDateTimeValueModel DateTimeModel { get; private set; }
        private readonly NullableTimePartPropertyViewModel _timePartViewModel;
        private readonly NullableDatePartPropertyViewModel _datePartViewModel;

        protected override string FormatValue(DateTime? value)
        {
            if (value == null) return string.Empty;
            switch (DateTimeModel.DateTimeStyle)
            {
                case DateTimeStyles.Date:
                    return value.Value.ToShortDateString();
                case DateTimeStyles.Time:
                    return value.Value.ToShortTimeString();
                default:
                    return value.Value.ToString();
            }
        }

        public bool DatePartVisible
        {
            get
            {
                return DateTimeModel.DateTimeStyle == DateTimeStyles.Date || DateTimeModel.DateTimeStyle == DateTimeStyles.DateTime;
            }
        }

        public bool TimePartVisible
        {
            get
            {
                return DateTimeModel.DateTimeStyle == DateTimeStyles.Time || DateTimeModel.DateTimeStyle == DateTimeStyles.DateTime;
            }
        }

        public DateTime? DatePart
        {
            get
            {
                return _datePartViewModel.Value;
            }
            set
            {
                _datePartViewModel.Value = value;
                OnPropertyChanged("DatePart");
            }
        }

        public TimeSpan? TimePart
        {
            get
            {
                return _timePartViewModel.Value;
            }
            set
            {
                _timePartViewModel.Value = value;
                OnPropertyChanged("TimePart");
            }
        }

        public string TimePartString
        {
            get
            {
                return _timePartViewModel.FormattedValue;
            }
            set
            {
                _timePartViewModel.FormattedValue = value;
                OnPropertyChanged("TimePartString");
            }
        }

        public string DatePartString
        {
            get
            {
                return _datePartViewModel.FormattedValue;
            }
            set
            {
                _datePartViewModel.FormattedValue = value;
                OnPropertyChanged("DatePartString");
            }
        }

        protected override void NotifyValueChanged()
        {
            base.NotifyValueChanged();
            OnPropertyChanged("DatePart");
            OnPropertyChanged("TimePart");
        }

        protected override void OnFormattedValueChanged()
        {
            base.OnFormattedValueChanged();
            OnPropertyChanged("TimePartString");
            OnPropertyChanged("DatePartString");
        }

        protected override void OnBlur()
        {
            _timePartViewModel.Blur();
            _datePartViewModel.Blur();
            base.OnBlur();
        }

        protected override void OnFocus()
        {
            _timePartViewModel.Focus();
            _datePartViewModel.Focus();
            base.OnFocus();
        }

        public override string Error
        {
            get
            {
                var errors = new[] { base.Error, GetDatePartError(), GetTimePartError() };
                return string.Join("\n", errors.Where(i => !string.IsNullOrEmpty(i)).ToArray());
            }
        }

        private string GetTimePartError()
        {
            // both no null input allowed or there is a datepart => error
            var timeIsNull = (!AllowNullInput && !_timePartViewModel.Value.HasValue)
                || (_datePartViewModel.Value.HasValue && !_timePartViewModel.Value.HasValue)
                ? ValueViewModelResources.TimeIsRequired
                : string.Empty;
            return string.Join("\n", new string[] { _timePartViewModel.Error, timeIsNull }.Where(s => !string.IsNullOrEmpty(s)).ToArray());
        }

        private string GetDatePartError()
        {
            var dateIsNull = (!AllowNullInput && !_datePartViewModel.Value.HasValue)
                || (_timePartViewModel.Value.HasValue && !_datePartViewModel.Value.HasValue)
                ? ValueViewModelResources.DateIsRequired
                : string.Empty;
            return string.Join("\n", new string[] { _datePartViewModel.Error, dateIsNull }.Where(s => !string.IsNullOrEmpty(s)).ToArray());
        }

        public override string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "TimePartString":
                        return GetTimePartError();
                    case "DatePartString":
                        return GetDatePartError();
                    default:
                        return base[columnName];
                }
            }
        }
    }

    public class NullableBoolPropertyViewModel : NullableStructValueViewModel<bool>
    {
        public new delegate NullableBoolPropertyViewModel Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public NullableBoolPropertyViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
            BoolModel = (IBoolValueModel)mdl;
            usingDefinedPresentation =
                   BoolModel.FalseIcon != null || !string.IsNullOrEmpty(BoolModel.FalseLabel)
                || BoolModel.TrueIcon != null || !string.IsNullOrEmpty(BoolModel.TrueLabel)
                || BoolModel.NullIcon != null || !string.IsNullOrEmpty(BoolModel.NullLabel);
        }

        public IBoolValueModel BoolModel { get; private set; }

        protected readonly bool usingDefinedPresentation;

        protected override void NotifyValueChanged()
        {
            base.NotifyValueChanged();
            OnPropertyChanged("Icon");
        }

        private KeyValuePair<bool?, string>[] _possibleValues;
        public KeyValuePair<bool?, string>[] PossibleValues
        {
            get
            {
                if (_possibleValues == null)
                {
                    if (!usingDefinedPresentation)
                    {
                        _possibleValues = new[] { 
                            new KeyValuePair<bool?, string>(null, ValueViewModelResources.Null),
                            new KeyValuePair<bool?, string>(true, ValueViewModelResources.True),
                            new KeyValuePair<bool?, string>(false, ValueViewModelResources.False)
                        };
                    }
                    else
                    {
                        _possibleValues = new[] { 
                            new KeyValuePair<bool?, string>(null, BoolModel.NullLabel),
                            new KeyValuePair<bool?, string>(true, BoolModel.TrueLabel),
                            new KeyValuePair<bool?, string>(false, BoolModel.FalseLabel)
                        };
                    }
                }
                return _possibleValues;
            }
        }

        public override string FormattedValue
        {
            get
            {
                if (!usingDefinedPresentation)
                {
                    if (base.Value == true)
                    {
                        return ValueViewModelResources.True;
                    }
                    else if (base.Value == false)
                    {
                        return ValueViewModelResources.False;
                    }
                    else
                    {
                        return ValueViewModelResources.Null;
                    }
                }
                else
                {
                    if (base.Value == true)
                    {
                        return BoolModel.TrueLabel;
                    }
                    else if (base.Value == false)
                    {
                        return BoolModel.FalseLabel;
                    }
                    else
                    {
                        return BoolModel.NullLabel;
                    }
                }
            }
            set
            {
                throw new NotSupportedException("Parsing boolen in different languages is not supported now");
            }
        }

        private Icon _customIcon;
        public override Icon Icon
        {
            get
            {
                if (_customIcon != null)
                {
                    return _customIcon;
                }
                else if (base.Value == true)
                {
                    return BoolModel.TrueIcon;
                }
                else if (base.Value == false)
                {
                    return BoolModel.FalseIcon;
                }
                else
                {
                    return BoolModel.NullIcon;
                }
            }
            set
            {
                if (_customIcon != value)
                {
                    _customIcon = value;
                    OnPropertyChanged("Icon");
                }
            }
        }
    }

    public class NullableMonthPropertyViewModel
        : NullableDateTimePropertyViewModel
    {
        public new delegate NullableMonthPropertyViewModel Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public NullableMonthPropertyViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {

        }

        private int? _year;
        public int? Year
        {
            get
            {
                UpdateValueCache(GetValueFromModel());
                return _year;
            }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    base.Value = GetValueFromComponents();
                    OnPropertyChanged("Year");
                }
            }
        }

        private int? _month;
        public int? Month
        {
            get
            {
                UpdateValueCache(GetValueFromModel());
                return _month;
            }
            set
            {
                if (_month != value)
                {
                    _month = value;
                    base.Value = GetValueFromComponents();
                    OnPropertyChanged("Month");
                }
            }
        }

        private bool _cacheInititalized = false;
        private void UpdateValueCache(DateTime? value)
        {
            if (!_cacheInititalized)
            {
                ResetYearMonthCache(value);
            }
        }

        private void ResetYearMonthCache(DateTime? value)
        {
            _year = value != null ? value.Value.Year : (int?)null;
            _month = value != null ? value.Value.Month : (int?)null;
            _cacheInititalized = true;
        }

        protected override void OnValidInput(string formattedValue, DateTime? value)
        {
            ResetYearMonthCache(value);
            base.OnValidInput(formattedValue, value);
        }

        private DateTime? GetValueFromComponents()
        {
            var oldDate = GetValueFromModel();
            var localDateValid = Year != null && Month != null && Year > 0 && Month >= 1 && Month <= 12;

            if (localDateValid)
            {
                if (oldDate != null)
                {
                    return new DateTime(Year.Value, Month.Value, oldDate.Value.Day)
                        + oldDate.Value.TimeOfDay;
                }
                else
                {
                    return new DateTime(Year.Value, Month.Value, 1);
                }
            }
            else
            {
                return oldDate;
            }
        }

        //protected override DateTime? GetValueFromModel()
        //{
        //    return GetValueFromComponents();
        //}

        protected override void SetValueToModel(DateTime? value)
        {
            base.SetValueToModel(value);
            ResetYearMonthCache(value);
            OnPropertyChanged("Year");
            OnPropertyChanged("Month");
        }

        protected override void NotifyValueChanged()
        {
            base.NotifyValueChanged();
            OnPropertyChanged("Year");
            OnPropertyChanged("Month");
        }

        public class MonthViewModel
        {
            public MonthViewModel()
            {
            }

            public MonthViewModel(int month, string name)
            {
                this.Month = month;
                this.Name = name;
            }

            public int Month { get; set; }
            public string Name { get; set; }
        }

        private static IList<MonthViewModel> _Months = null;
        public IEnumerable<MonthViewModel> Months
        {
            get
            {
                if (_Months == null)
                {
                    _Months = new List<MonthViewModel>();
                    _Months.Add(new MonthViewModel());
                    for (int i = 1; i <= 12; i++)
                    {
                        var dt = new DateTime(2000, i, 1);
                        _Months.Add(new MonthViewModel(i, dt.ToString("MMMM")));
                    }
                }
                return _Months;
            }
        }
    }
}
