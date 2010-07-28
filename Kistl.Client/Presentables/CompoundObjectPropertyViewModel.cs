namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    [ViewModelDescriptor("GUI", DefaultKind = "Kistl.App.GUI.CompoundObjectPropertyKind", Description = "Viewmodel for editing a CompoundObject Property")]
    public class CompoundObjectPropertyViewModel : PropertyModel<CompoundObjectViewModel>, IValueModel<CompoundObjectViewModel>
    {
        public new delegate CompoundObjectPropertyViewModel Factory(IKistlContext dataCtx, INotifyingObject obj, CompoundObjectProperty prop);


        public CompoundObjectPropertyViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            INotifyingObject obj, CompoundObjectProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            AllowNullInput = prop.IsNullable();
            ReferencedType = prop.CompoundObjectDefinition;
        }

        #region Public Interface

        public CompoundObject ReferencedType
        {
            get;
            protected set;
        }

        public bool HasValue
        {
            get
            {
                return _valueCache != null;
            }
            set
            {
                if (!value)
                    Value = null;
            }
        }

        public bool IsNull
        {
            get
            {
                return _valueCache == null;
            }
            set
            {
                if (value)
                    Value = null;
            }
        }

        #region IClearableValue Members

        public void ClearValue()
        {
            if (AllowNullInput) Value = null;
            else throw new InvalidOperationException();
        }

        private ICommand _ClearValueCommand = null;
        public ICommand ClearValueCommand
        {
            get
            {
                if (_ClearValueCommand == null)
                {
                    _ClearValueCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>()
                        .Invoke(DataContext, "Clear value", "Sets the value to nothing", () => ClearValue(), () => AllowNullInput);
                }
                return _ClearValueCommand;
            }
        }

        #endregion


        private CompoundObjectViewModel _valueCache;
        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public CompoundObjectViewModel Value
        {
            get { return _valueCache; }
            set
            {
                _valueCache = value;

                var newPropertyValue = _valueCache == null ? null : _valueCache.Object;

                if (!object.Equals(Object.GetPropertyValue<object>(Property.Name), newPropertyValue))
                {
                    Object.SetPropertyValue<object>(Property.Name, newPropertyValue);
                    CheckConstraints();

                    OnPropertyChanged("Value");
                    OnPropertyChanged("HasValue");
                    OnPropertyChanged("IsNull");
                }
            }
        }

        public override string Name
        {
            get { return Value == null ? "(null)" : "CompoundObject: " + Value.Name; }
        }
        #endregion

        #region Utilities and UI callbacks

        protected override void UpdatePropertyValue()
        {
            var newValue = Object.GetPropertyValue<ICompoundObject>(Property.Name) ?? DataContext.CreateCompoundObject(DataContext.GetInterfaceType(((CompoundObjectProperty)Property).CompoundObjectDefinition.GetDataType()));
            var newModel = ModelFactory.CreateViewModel<CompoundObjectViewModel.Factory>(newValue).Invoke(DataContext, newValue);
            if (Value != newModel)
            {
                Value = newModel;
            }
        }

        #endregion

    }
}
