// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.API.Async;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.GUI;

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

    public class InputAcceptedEventArgs<TValue>
        : EventArgs
    {
        public InputAcceptedEventArgs(TValue oldVal, TValue newVal)
        {
            this.OldValue = oldVal;
            this.NewValue = newVal;
        }

        /// <summary>
        /// 
        /// </summary>
        public TValue OldValue { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public TValue NewValue { get; private set; }
    }

    public abstract class BaseValueViewModel : ViewModel, IValueViewModel, IFormattedValueViewModel, IDataErrorInfo, ILabeledViewModel
    {
        public new delegate BaseValueViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public static BaseValueViewModel Fetch(IViewModelFactory f, IZetboxContext dataCtx, ViewModel parent, Property prop, IValueModel mdl)
        {
            return (BaseValueViewModel)dataCtx.GetViewModelCache(f.PerfCounter).LookupOrCreate(mdl, () => f.CreateViewModel<BaseValueViewModel.Factory>(prop).Invoke(dataCtx, parent, mdl));
        }

        public static BaseValueViewModel Fetch(IViewModelFactory f, IZetboxContext dataCtx, ViewModel parent, Method m, IValueModel mdl)
        {
            return (BaseValueViewModel)dataCtx.GetViewModelCache(f.PerfCounter).LookupOrCreate(mdl, () => f.CreateViewModel<BaseValueViewModel.Factory>(m).Invoke(dataCtx, parent, mdl));
        }

        public static BaseValueViewModel Fetch(IViewModelFactory f, IZetboxContext dataCtx, ViewModel parent, ViewModelDescriptor desc, IValueModel mdl)
        {
            return (BaseValueViewModel)dataCtx.GetViewModelCache(f.PerfCounter).LookupOrCreate(mdl, () => f.CreateViewModel<BaseValueViewModel.Factory>(desc).Invoke(dataCtx, parent, mdl));
        }

        public static BaseValueViewModel Fetch(IViewModelFactory f, IZetboxContext dataCtx, ViewModel parent, BaseParameter param, IValueModel mdl)
        {
            return (BaseValueViewModel)dataCtx.GetViewModelCache(f.PerfCounter).LookupOrCreate(mdl, () => f.CreateViewModel<BaseValueViewModel.Factory>(param).Invoke(dataCtx, parent, mdl));
        }

        public BaseValueViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent)
        {
            this.ValueModel = mdl;
            dataCtx.IsElevatedModeChanged += new EventHandler(Context_IsElevatedModeChanged);

            ValidationManager.Register(this);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ValidationManager.Unregister(this);
        }

        void Context_IsElevatedModeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("IsReadOnly");
            OnHighlightChanged();
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
                // Reflect readonly only on changable context
                if (!DataContext.IsReadonly && (!IsEnabled || IsReadOnly)) return Highlight.Deactivated;
                if (Parent != null && Parent.Highlight != Highlight.None) return Parent.Highlight;
                return Highlight.None;
            }
        }

        public override Highlight HighlightAsync
        {
            get
            {
                if (DataContext.IsElevatedMode && !IsReadOnly) return Highlight.Bad; // May be true for calculated properties                
                // Reflect readonly only on changable context
                if (!DataContext.IsReadonly && (!IsEnabled || IsReadOnly)) return Highlight.Deactivated;
                if (Parent != null && Parent.HighlightAsync != Highlight.None) return Parent.HighlightAsync;
                return Highlight.None;
            }
        }

        public abstract void Focus();
        public abstract void Blur();

        #region Utilities and UI callbacks
        protected virtual void NotifyValueChanged()
        {
            OnPropertyChanged("ValueAsync");
            OnPropertyChanged("Value");
            OnHighlightChanged();
            OnFormattedValueChanged();
            OnPropertyChanged("IsNull");
            OnPropertyChanged("HasValue");
            OnPropertyChanged("Name");
            OnErrorChanged();
        }

        protected virtual void OnErrorChanged()
        {
            Validate();
            OnPropertyChanged("Error");
        }

        protected override string GetHelpText()
        {
            return ValueModel.HelpText;
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
                    OnHighlightChanged();
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
                        () => AllowNullInput && !IsReadOnly,
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
            set
            {
                ValueModel.SetUntypedValue(value);
            }
        }
        #endregion

        #region IFormattedValueViewModel Members

        public abstract string FormattedValue { get; set; }
        public abstract string FormattedValueAsync { get; }

        protected virtual void OnFormattedValueChanged()
        {
            OnPropertyChanged("FormattedValue");
            OnPropertyChanged("FormattedValueAsync");
        }

        #endregion

        #region IDataErrorInfo Members

        protected override bool NeedsValidation
        {
            get
            {
                return !IsReadOnly && base.NeedsValidation;
            }
        }

        protected override void DoValidate()
        {
            base.DoValidate();
            if (!ValueModel.ReportErrors) return;
            if (!NeedsValidation) return;

            ValueModel.Validate();
            var error = ValueModel.Error;

            if (IsValid && !string.IsNullOrEmpty(error))
            {
                ValidationError.AddError(error);
            }
        }

        string IDataErrorInfo.Error
        {
            get
            {
                if (ValidationError != null)
                {
                    return ValidationError.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                return ((IDataErrorInfo)this).Error;
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

        public new delegate ValueViewModel<TValue, TModel> Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public ValueViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
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

        protected abstract System.Threading.Tasks.Task<TValue> GetValueFromModelAsync();

        /// <summary>
        /// Writes the specified value to the model, circumventing the state machine.
        /// </summary>
        protected abstract void SetValueToModel(TValue value);

        public virtual TValue Value
        {
            get
            {
                return GetValueFromModelAsync().Result;
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

        public abstract TValue ValueAsync { get; set; }

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
                        return FormatValue(this.GetValueFromModelAsync().Result);
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
                switch (State)
                {
                    case ValueViewModelState.Blurred_UnmodifiedValue:
                    case ValueViewModelState.ImplicitFocus_WritingModel:
                        TryParseValue(value);
                        break;
                    case ValueViewModelState.Blurred_PartialUserInput:
                    case ValueViewModelState.ImplicitFocus_PartialUserInput:
                    case ValueViewModelState.Focused_PartialUserInput:
                    case ValueViewModelState.Focused_UnmodifiedValue:
                    case ValueViewModelState.Focused_WritingModel:
                        if (_partialUserInput != value)
                        {
                            TryParseValue(value);
                        }
                        break;
                    default:
                        throw new InvalidOperationException(string.Format("Unexpected State {0}", State));
                }
            }
        }

        private void TryParseValue(string value)
        {
            var oldPartialUserInputError = _partialUserInputError;
            var parseResult = ParseValue(value);
            if (parseResult.HasErrors)
            {
                OnPartialInput(value, parseResult.Error);
            }
            else if (!AllowNullInput && parseResult.Value == null)
            {
                OnPartialInput(value, ValueViewModelResources.ErrorEmptyValue);
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

        private string _formattedValueAsyncCache;
        public override string FormattedValueAsync
        {
            get
            {
                GetValueFromModelAsync()
                    .OnResult(t =>
                    {
                        var tmp = FormattedValue;
                        if (_formattedValueAsyncCache != tmp)
                        {
                            _formattedValueAsyncCache = tmp;
                            OnPropertyChanged("FormattedValueAsync");
                        }
                    });
                return _formattedValueAsyncCache;
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
        /// Part of the ValueViewModel state machine as described in the ZetboxGuide. 
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
                    Zetbox.API.Utils.Logging.Client.WarnFormat("Model changed while {0} has focus; ignoring.", this.GetType().FullName);
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

        public event EventHandler<InputAcceptedEventArgs<TValue>> InputAccepted;
        /// <summary>
        /// Part of the ValueViewModel state machine as described in the ZetboxGuide. 
        /// This method is called everytime valid input is received and handles 
        /// the VI event.
        /// </summary>
        protected virtual void OnValidInput(string formattedValue, TValue value)
        {
            Logging.Client.DebugFormat("Received valid input [{0}] interpreted as [{1}]", formattedValue, value);
            var oldValue = Value;

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
            OnInputAccepted(oldValue, value);
        }

        protected virtual void OnInputAccepted(TValue oldValue, TValue newValue)
        {
            var temp = InputAccepted;
            if (temp != null)
            {
                temp(this, new InputAcceptedEventArgs<TValue>(oldValue, newValue));
            }
        }

        /// <summary>
        /// Part of the ValueViewModel state machine as described in the ZetboxGuide. 
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
        /// Part of the ValueViewModel state machine as described in the ZetboxGuide. 
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
                        _partialUserInput = FormatValue(this.GetValueFromModelAsync().Result);
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
        /// Part of the ValueViewModel state machine as described in the ZetboxGuide. 
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

        #region Validation
        protected override void DoValidate()
        {
            base.DoValidate();
            if (!ValueModel.ReportErrors) return;
            if (!NeedsValidation) return;

            if (IsValid && !string.IsNullOrEmpty(_partialUserInputError))
            {
                ValidationError.Errors.Add(_partialUserInputError);
            }
        }
        #endregion
    }

    public class NullableStructValueViewModel<TValue> : ValueViewModel<Nullable<TValue>, Nullable<TValue>>
        where TValue : struct
    {
        public new delegate NullableStructValueViewModel<TValue> Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public NullableStructValueViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
        }

        public override TValue? ValueAsync
        {
            get
            {
                return this.Value;
            }
            set
            {
                this.Value = value;
            }
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
                        Error = ValueViewModelResources.ErrorEmptyValue
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
                    Error = ValueViewModelResources.ErrorConvertingType
                };
            }
        }

        protected override System.Threading.Tasks.Task<TValue?> GetValueFromModelAsync()
        {
            return Task.FromResult<TValue?>(_illegalNullInput ? null : ValueModel.Value);
        }

        private bool _illegalNullInput = false;

        protected override void SetValueToModel(TValue? value)
        {
            if (!AllowNullInput && value == null)
            {
                _illegalNullInput = true;
                return;
            }
            _illegalNullInput = false;
            ValueModel.Value = value;
        }

        protected override void DoValidate()
        {
            base.DoValidate();
            if (!ValueModel.ReportErrors) return;
            if (!NeedsValidation) return;

            if (IsValid && _illegalNullInput)
            {
                ValidationError.Errors.Add(ValueViewModelResources.ErrorEmptyValue);
            }
        }
    }

    public class ClassValueViewModel<TValue> : ValueViewModel<TValue, TValue>
        where TValue : class
    {
        public new delegate ClassValueViewModel<TValue> Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public ClassValueViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
        }

        public override TValue ValueAsync
        {
            get
            {
                return this.Value;
            }
            set
            {
                this.Value = value;
            }
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

        protected override System.Threading.Tasks.Task<TValue> GetValueFromModelAsync()
        {
            return Task.FromResult<TValue>(ValueModel.Value);
        }

        protected override void SetValueToModel(TValue value)
        {
            ValueModel.Value = value;
        }
    }

    [ViewModelDescriptor]
    public class EnumerationValueViewModel : NullableStructValueViewModel<int>
    {
        public new delegate EnumerationValueViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public EnumerationValueViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
            this.EnumModel = (IEnumerationValueModel)mdl;
        }

        public IEnumerationValueModel EnumModel { get; private set; }

        private ObservableCollection<KeyValuePair<int?, string>> _possibleValues = null;
        private ReadOnlyObservableCollection<KeyValuePair<int?, string>> _possibleValuesRO = null;
        public ReadOnlyObservableCollection<KeyValuePair<int?, string>> PossibleValues
        {
            get
            {
                if (_possibleValues == null)
                {
                    var entries = EnumModel
                        .Enumeration
                        .EnumerationEntries
                        .Where(i => i.NotSelectable != true)
                        .Select(ee => new KeyValuePair<int?, string>(ee.Value, ee.GetLabel()));

                    _possibleValues = new ObservableCollection<KeyValuePair<int?, string>>(
                        new[] { new KeyValuePair<int?, string>(null, String.Empty) }
                        .Concat(entries)
                        .ToList()
                    );
                    _possibleValuesRO = new ReadOnlyObservableCollection<KeyValuePair<int?, string>>(_possibleValues);
                    EnsureValuePossible(Value);
                }
                return _possibleValuesRO;
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

        private void EnsureValuePossible(int? value)
        {
            if (_possibleValues != null && value != null)
            {
                // Add if not found
                if (!_possibleValues.Any(i => i.Key == value))
                {
                    _possibleValues.Add(new KeyValuePair<int?, string>(value, EnumModel.Enumeration.EnumerationEntries.FirstOrDefault(i => i.Value == value.Value)?.GetLabel()));
                }
            }
        }

        protected override async System.Threading.Tasks.Task<int?> GetValueFromModelAsync()
        {
            var result = await base.GetValueFromModelAsync();
            EnsureValuePossible(result);
            return result;
        }

        protected override void SetValueToModel(int? value)
        {
            base.SetValueToModel(value);
            EnsureValuePossible(value);
        }
    }

    [ViewModelDescriptor]
    public class StringValueViewModel : ClassValueViewModel<string>
    {
        public new delegate StringValueViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);
        public StringValueViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
        }
    }

    [ViewModelDescriptor]
    public class MultiLineStringValueViewModel
        : StringValueViewModel
    {
        public new delegate MultiLineStringValueViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public MultiLineStringValueViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
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
                if (string.IsNullOrEmpty(Value)) return string.Empty;
                return Value
                        .MaxLength(ShortTextLength, "...")
                        .Replace("\r", "")
                        .Replace('\n', ' ');
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
                    _EditCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.pen_png.Find(FrozenContext));
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

    [ViewModelDescriptor]
    public class EmailStringValueViewModel
        : StringValueViewModel
    {
        public new delegate EmailStringValueViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        private readonly IInteractiveMailSender _mailSender;

        public EmailStringValueViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl, IInteractiveMailSender mailSender = null)
            : base(dependencies, dataCtx, parent, mdl)
        {
            _mailSender = mailSender;
        }

        private ICommandViewModel _SendMailCommand = null;
        public ICommandViewModel SendMailCommand
        {
            get
            {
                if (_SendMailCommand == null)
                {
                    _SendMailCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        ValueViewModelResources.SendMailCommand_Name,
                        ValueViewModelResources.SendMailCommand_Tooltip,
                        () => SendMail(),
                        () => _mailSender != null && !string.IsNullOrWhiteSpace(Value) && IsMailaddress(Value),
                        SendMailReason);
                    _SendMailCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.pen_png.Find(FrozenContext));
                }
                return _SendMailCommand;
            }
        }

        private static bool IsMailaddress(string addr)
        {
            try
            {
                // see http://stackoverflow.com/q/528090/4918
                var parsed = new MailAddress(addr);
                GC.KeepAlive(parsed);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SendMail()
        {
            if (_mailSender != null && IsMailaddress(Value))
            {
                try
                {
                    var msg = new System.Net.Mail.MailMessage();
                    msg.To.Add(Value);
                    _mailSender.Send(msg);
                }
                catch
                {
                    // Do nothing.
                }
            };
        }

        public string SendMailReason()
        {
            if (_mailSender == null) return ValueViewModelResources.NoMailSender;
            if (string.IsNullOrWhiteSpace(Value)) return ValueViewModelResources.NoMailAddress;
            if (!IsMailaddress(Value)) return ValueViewModelResources.InvalidMailFormat;
            return string.Empty;
        }
    }

    [ViewModelDescriptor]
    public class NullableGuidPropertyViewModel : NullableStructValueViewModel<Guid>
    {
        public new delegate NullableGuidPropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public NullableGuidPropertyViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
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

    [ViewModelDescriptor]
    public class NullableDecimalPropertyViewModel : NullableStructValueViewModel<decimal>
    {
        public new delegate NullableDecimalPropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public NullableDecimalPropertyViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
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

    [ViewModelDescriptor]
    public class NullableIntPropertyViewModel : NullableStructValueViewModel<int>
    {
        public new delegate NullableIntPropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public NullableIntPropertyViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
        }
    }

    [ViewModelDescriptor]
    public class NullableDoublePropertyViewModel : NullableStructValueViewModel<double>
    {
        public new delegate NullableDoublePropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public NullableDoublePropertyViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
        }
    }

    [ViewModelDescriptor]
    public class NullableDateTimePropertyViewModel
        : NullableStructValueViewModel<DateTime>
    {
        private class NullableTimePartPropertyViewModel : NullableStructValueViewModel<TimeSpan>
        {
            public new delegate NullableTimePartPropertyViewModel Factory(IZetboxContext dataCtx, NullableDateTimePropertyViewModel parent, IValueModel mdl);

            public NullableTimePartPropertyViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, NullableDateTimePropertyViewModel parent, IValueModel mdl)
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
                    if (string.IsNullOrEmpty(str))
                    {
                        result.Value = null;
                        result.Error = ValueViewModelResources.TimeIsRequired;
                    }
                    else if (null != (matches = Regex.Matches(str, @"^(\d?\d):?(\d\d)$")))
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
                            result.Error = ValueViewModelResources.ErrorConvertingType;
                        }
                    }
                    else
                    {
                        result.Error = ValueViewModelResources.ErrorConvertingType;
                    }
                }
                return result;
            }

            protected override System.Threading.Tasks.Task<TimeSpan?> GetValueFromModelAsync()
            {
                return System.Threading.Tasks.Task.Run<TimeSpan?>(async () =>
                {
                    var val = await Parent.GetValueFromModelAsync();
                    if (val == null) return null;
                    return val.Value.TimeOfDay;
                });
            }

            protected override void SetValueToModel(TimeSpan? value)
            {
                if (Parent.GetValueFromModelAsync().Result == null && value == null)
                {
                    Parent.SetValueToModel(null);
                }
                else
                {
                    Parent.SetValueToModel(GetNewValue(value));
                    Parent.OnPropertyChanged("TimePart");
                    Parent.OnPropertyChanged("TimePartString");
                }
            }

            private DateTime? GetNewValue(TimeSpan? value)
            {
                DateTime? result;
                var date = (Parent.GetValueFromModelAsync().Result ?? DateTime.MinValue).Date;
                if (date == DateTime.MinValue.Date)
                {
                    if (value == null || value == TimeSpan.Zero)
                    {
                        result = null;
                    }
                    else
                    {
                        result = date + (value ?? TimeSpan.Zero);
                    }
                }
                else
                {
                    result = date + (value ?? TimeSpan.Zero);
                }
                return result;
            }

            protected override void OnErrorChanged()
            {
                base.OnErrorChanged();
                Parent.OnErrorChanged();
            }

            protected override void OnInputAccepted(TimeSpan? oldValue, TimeSpan? newValue)
            {
                base.OnInputAccepted(oldValue, newValue);
                var oldDate = GetNewValue(oldValue);
                var newDate = GetNewValue(newValue);
                Parent.OnInputAccepted(oldDate, newDate);
            }
        }

        private class NullableDatePartPropertyViewModel : NullableStructValueViewModel<DateTime>
        {
            public new delegate NullableDatePartPropertyViewModel Factory(IZetboxContext dataCtx, NullableDateTimePropertyViewModel parent, IValueModel mdl);

            public NullableDatePartPropertyViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, NullableDateTimePropertyViewModel parent, IValueModel mdl)
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

            protected override System.Threading.Tasks.Task<DateTime?> GetValueFromModelAsync()
            {
                return System.Threading.Tasks.Task.Run<DateTime?>(async () =>
                {
                    var modelValue = await Parent.GetValueFromModelAsync();
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
                });
            }

            protected override void SetValueToModel(DateTime? value)
            {
                if (Parent.GetValueFromModelAsync() == null && value == null)
                {
                    Parent.SetValueToModel(null);
                }
                else
                {
                    Parent.SetValueToModel(GetNewValue(value));
                    Parent.OnPropertyChanged("TimePart");
                    Parent.OnPropertyChanged("TimePartString");
                }
            }

            private DateTime? GetNewValue(DateTime? value)
            {
                DateTime? result;
                var time = (Parent.GetValueFromModelAsync().Result ?? DateTime.MinValue).TimeOfDay;
                if (time == DateTime.MinValue.TimeOfDay)
                {
                    if (value == DateTime.MinValue)
                    {
                        result = null;
                    }
                    else
                    {
                        result = value;
                    }
                }
                else
                {
                    result = (value ?? DateTime.MinValue) + time;
                }
                return result;
            }

            protected override void OnErrorChanged()
            {
                base.OnErrorChanged();
                Parent.OnErrorChanged();
            }

            protected override void OnInputAccepted(DateTime? oldValue, DateTime? newValue)
            {
                base.OnInputAccepted(oldValue, newValue);
                var oldDate = GetNewValue(oldValue);
                var newDate = GetNewValue(newValue);
                Parent.OnInputAccepted(oldDate, newDate);
            }
        }

        public new delegate NullableDateTimePropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public NullableDateTimePropertyViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
            DateTimeModel = (IDateTimeValueModel)mdl;
            DateTimeModel.PropertyChanged += new PropertyChangedEventHandler(DateTimeModel_PropertyChanged);
            _timePartViewModel = ViewModelFactory.CreateViewModel<NullableTimePartPropertyViewModel.Factory>().Invoke(DataContext, this, mdl);
            _datePartViewModel = ViewModelFactory.CreateViewModel<NullableDatePartPropertyViewModel.Factory>().Invoke(DataContext, this, mdl);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _timePartViewModel.Dispose();
            _datePartViewModel.Dispose();
        }

        void DateTimeModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "DateTimeStyle":
                    OnPropertyChanged("DatePartVisible");
                    OnPropertyChanged("TimePartVisible");
                    NotifyValueChanged();
                    break;
            }
        }

        private DateTimeStyles? _dateTimeStyle;
        public DateTimeStyles DateTimeStyle
        {
            get
            {
                return _dateTimeStyle ?? DateTimeModel.DateTimeStyle;
            }
            set
            {
                if (_dateTimeStyle != value)
                {
                    _dateTimeStyle = value;
                    OnPropertyChanged("DateTimeStyle");
                    OnPropertyChanged("TimePartVisible");
                    OnPropertyChanged("TimePartVisible");
                    NotifyValueChanged();
                }
            }
        }

        public IDateTimeValueModel DateTimeModel { get; private set; }
        private readonly NullableTimePartPropertyViewModel _timePartViewModel;
        private readonly NullableDatePartPropertyViewModel _datePartViewModel;

        protected override string FormatValue(DateTime? value)
        {
            if (value == null) return string.Empty;
            switch (DateTimeStyle)
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
                return DateTimeStyle == DateTimeStyles.Date || DateTimeStyle == DateTimeStyles.DateTime;
            }
        }

        public bool TimePartVisible
        {
            get
            {
                return DateTimeStyle == DateTimeStyles.Time || DateTimeStyle == DateTimeStyles.DateTime;
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

        protected override void DoValidate()
        {
            base.DoValidate();
            if (!ValueModel.ReportErrors) return;
            if (!NeedsValidation) return;

            if (IsValid)
            {
                if (DatePartVisible)
                {
                    _datePartViewModel.Validate();
                    if (!_datePartViewModel.IsValid)
                    {
                        ValidationError.AddErrors(_datePartViewModel.ValidationError.Errors);

                        if ((!AllowNullInput && !_datePartViewModel.Value.HasValue)
                           || (_timePartViewModel.Value.HasValue && !_datePartViewModel.Value.HasValue))
                        {
                            // Date is null
                            ValidationError.Errors.Add(ValueViewModelResources.DateIsRequired);
                        }
                        // Don't add Children as they never apear outside this viewmodel
                    }
                }
                if (TimePartVisible)
                {
                    _timePartViewModel.Validate();
                    if (!_timePartViewModel.IsValid)
                    {
                        ValidationError.AddErrors(_timePartViewModel.ValidationError.Errors);

                        if ((!AllowNullInput && !_timePartViewModel.Value.HasValue)
                            || (_datePartViewModel.Value.HasValue && !_timePartViewModel.Value.HasValue))
                        {
                            // both no null input allowed or there is a datepart => error
                            ValidationError.Errors.Add(ValueViewModelResources.TimeIsRequired);
                        }
                        // Don't add Children as they never apear outside this viewmodel
                    }
                }
            }
        }
    }

    [ViewModelDescriptor]
    public class NullableBoolPropertyViewModel : NullableStructValueViewModel<bool>
    {
        public new delegate NullableBoolPropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public NullableBoolPropertyViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
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

        private System.Drawing.Image _customIcon;
        public override System.Drawing.Image Icon
        {
            get
            {
                if (_customIcon != null)
                {
                    return _customIcon;
                }
                else if (base.Value == true)
                {
                    return IconConverter.ToImage(BoolModel.TrueIcon);
                }
                else if (base.Value == false)
                {
                    return IconConverter.ToImage(BoolModel.FalseIcon);
                }
                else
                {
                    return IconConverter.ToImage(BoolModel.NullIcon);
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

    [ViewModelDescriptor]
    public class NullableMonthPropertyViewModel
        : NullableDateTimePropertyViewModel
    {
        public new delegate NullableMonthPropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public NullableMonthPropertyViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {

        }

        private int? _year;
        public int? Year
        {
            get
            {
                UpdateValueCache(GetValueFromModelAsync().Result);
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
                UpdateValueCache(GetValueFromModelAsync().Result);
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
            var oldDate = GetValueFromModelAsync().Result;
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
