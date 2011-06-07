
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.ValueViewModels;
    
    public class CompoundObjectViewModel 
        : ViewModel
    {
        public new delegate CompoundObjectViewModel Factory(IKistlContext dataCtx, ViewModel parent, ICompoundObject obj);

        public static CompoundObjectViewModel Fetch(IViewModelFactory f, IKistlContext dataCtx, ViewModel parent, ICompoundObject obj)
        {
            return (CompoundObjectViewModel)dataCtx.GetViewModelCache().LookupOrCreate(obj, () => f.CreateViewModel<CompoundObjectViewModel.Factory>(obj).Invoke(dataCtx, parent, obj));
        }

        public CompoundObjectViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent,
            ICompoundObject obj)
            : base(appCtx, dataCtx, parent)
        {
            _object = obj;
            _object.PropertyChanged += ObjectPropertyChanged;
        }

        private void ObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // propagate updates for IDataErrorInfo
            OnPropertyChanged("PropertyModels");
            OnPropertyChanged("PropertyModelsByName");
            OnPropertyChanged("PropertyGroups");
            OnPropertyChanged("PropertyGroupsByName");
            // all updates done
        }

        #region Public Interface

        private ICompoundObject _object;
        public ICompoundObject Object { get { return _object; } }

        private ReadOnlyProjectedList<Property, BaseValueViewModel> _propertyModels;
        public IReadOnlyList<BaseValueViewModel> PropertyModels
        {
            get
            {
                if (_propertyModels == null)
                {
                    _propertyModels = new ReadOnlyProjectedList<Property, BaseValueViewModel>(
                        FetchPropertyList().ToList(),
                        property => BaseValueViewModel.Fetch(ViewModelFactory, DataContext, this, property, property.GetPropertyValueModel(Object)),
                        m => null); //m.Property);
                }
                return _propertyModels;
            }
        }
        private LookupDictionary<string, Property, ViewModel> _propertyModelsByName;
        public LookupDictionary<string, Property, ViewModel> PropertyModelsByName
        {
            get
            {
                if (_propertyModelsByName == null)
                {
                    _propertyModelsByName = new LookupDictionary<string, Property, ViewModel>(FetchPropertyList().ToList(), 
                        prop => prop.Name,
                        prop => BaseValueViewModel.Fetch(ViewModelFactory, DataContext, this, prop, prop.GetPropertyValueModel(Object)));
                }
                return _propertyModelsByName;
            }
        }

        private IEnumerable<Property> FetchPropertyList()
        {
            // load properties from MetaContext
            var cls = _object.GetCompoundObjectDefinition(FrozenContext);
            var props = new List<Property>();
            foreach (Property p in cls.Properties)
            {
                props.Add(p);
            }

            return props;
        }
        #endregion

        public override string Name
        {
            get { return ""; }
        }
    }
}
