
namespace Kistl.Client.Presentables.ValueViewModels
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
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor("GUI", DefaultKind = "Kistl.App.GUI.CompoundObjectPropertyKind", Description = "Viewmodel for editing a CompoundObject Property")]
    public class CompoundObjectPropertyViewModel : ValueViewModel<CompoundObjectViewModel, ICompoundObject>, IValueViewModel<CompoundObjectViewModel>
    {
        public new delegate CompoundObjectPropertyViewModel Factory(IKistlContext dataCtx, IValueModel mdl);


        public CompoundObjectPropertyViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IValueModel mdl)
            : base(appCtx, dataCtx, mdl)
        {
            // ReferencedType = prop.CompoundObjectDefinition;
        }

        #region Public Interface

        public CompoundObject ReferencedType
        {
            get;
            protected set;
        }


        public override string Name
        {
            get { return Value == null ? "(null)" : "CompoundObject: " + Value.Name; }
        }
        #endregion

        #region Utilities and UI callbacks

        //protected override void UpdatePropertyValue()
        //{
        //    var newValue = Object.GetPropertyValue<ICompoundObject>(Property.Name) ?? DataContext.CreateCompoundObject(DataContext.GetInterfaceType(((CompoundObjectProperty)Property).CompoundObjectDefinition.GetDataType()));
        //    var newModel = ModelFactory.CreateViewModel<CompoundObjectViewModel.Factory>(newValue).Invoke(DataContext, newValue);
        //    if (Value != newModel)
        //    {
        //        Value = newModel;
        //    }
        //}

        #endregion


        protected override void ParseValue(string str)
        {
            throw new NotSupportedException();
        }

        private bool _valueCacheInititalized = false;
        private CompoundObjectViewModel _valueCache;

        /// <summary>
        /// Gets or sets the value of the property presented by this model
        /// </summary>
        public override CompoundObjectViewModel Value
        {
            get
            {
                if (!_valueCacheInititalized)
                {
                    UpdateValueCache();
                }
                return _valueCache;
            }
            set
            {
                _valueCache = value;
                _valueCacheInititalized = true;
                ValueModel.Value = value != null ? value.Object : null;
            }
        }

        private void UpdateValueCache()
        {
            var obj = ValueModel.Value;
            if (obj != null)
            {
                _valueCache = ModelFactory.CreateViewModel<CompoundObjectViewModel.Factory>().Invoke(DataContext, ValueModel.Value);
            }
            _valueCacheInititalized = true;
        }
    }
}
