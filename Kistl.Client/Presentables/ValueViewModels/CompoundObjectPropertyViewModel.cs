
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
        public new delegate CompoundObjectPropertyViewModel Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public CompoundObjectPropertyViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent,
            IValueModel mdl)
            : base(appCtx, dataCtx, parent, mdl)
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

        protected override ParseResult<CompoundObjectViewModel> ParseValue(string str)
        {
            throw new NotSupportedException();
        }

        private bool _valueCacheInititalized = false;
        private CompoundObjectViewModel _valueCache;

        protected override CompoundObjectViewModel GetValueFromModel()
        {
            if (!_valueCacheInititalized)
            {
                UpdateValueCache();
            }
            return _valueCache;
        }

        protected override void SetValueToModel(CompoundObjectViewModel value)
        {
            _valueCache = value;
            _valueCacheInititalized = true;
            ValueModel.Value = value != null ? value.Object : null;
        }

        private void UpdateValueCache()
        {
            var obj = ValueModel.Value;
            if (obj == null)
            {
                // if it's null, create one
                // TODO: This may be a subject to change
                // We still don't know, how to handle nullable CompoundObjects!!!
                obj = DataContext.CreateCompoundObject(DataContext.GetInterfaceType(ReferencedType.GetDataType()));
            }
            _valueCache = CompoundObjectViewModel.Fetch(ViewModelFactory, DataContext, this, obj);
            _valueCacheInititalized = true;
        }

        public override bool IsReadOnly
        {
            get
            {
                return base.IsReadOnly;
            }
            set
            {
                base.IsReadOnly = value;
                foreach (var propMdl in Value.PropertyModels)
                {
                    propMdl.IsReadOnly = value;
                }
            }
        }
    }
}
