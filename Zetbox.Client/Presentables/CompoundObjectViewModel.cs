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

namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ValueViewModels;
    
    public class CompoundObjectViewModel 
        : ViewModel
    {
        public new delegate CompoundObjectViewModel Factory(IZetboxContext dataCtx, ViewModel parent, ICompoundObject obj);

        public static CompoundObjectViewModel Fetch(IViewModelFactory f, IZetboxContext dataCtx, ViewModel parent, ICompoundObject obj)
        {
            return (CompoundObjectViewModel)dataCtx.GetViewModelCache(f.PerfCounter).LookupOrCreate(obj, () => f.CreateViewModel<CompoundObjectViewModel.Factory>(obj).Invoke(dataCtx, parent, obj));
        }

        public CompoundObjectViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
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
            OnPropertyChanged("Name");
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
            get { return _object.ToString(); }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
