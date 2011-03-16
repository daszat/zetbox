
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

    [ViewModelDescriptor]
    public class CompoundObjectPropertyViewModel : ValueViewModel<CompoundObjectViewModel, ICompoundObject>, IValueViewModel<CompoundObjectViewModel>
    {
        public new delegate CompoundObjectPropertyViewModel Factory(IKistlContext dataCtx, IValueModel mdl);


        public CompoundObjectPropertyViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IValueModel mdl)
            : base(appCtx, dataCtx, mdl)
        {
            CompoundObjectModel = (ICompoundObjectValueModel)mdl;
        }

        #region Public Interface

        public ICompoundObjectValueModel CompoundObjectModel { get; private set; }
        public CompoundObject ReferencedType { get { return CompoundObjectModel.CompoundObjectDefinition; } }

        public override string Name
        {
            get { return Value == null ? "(null)" : "CompoundObject: " + Value.Name; }
        }
        #endregion

        protected override void ParseValue(string str, out string error)
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
                _valueCache = CompoundObjectViewModel.Fetch(ViewModelFactory, DataContext, obj);
            }
            _valueCacheInititalized = true;
        }
    }
}
