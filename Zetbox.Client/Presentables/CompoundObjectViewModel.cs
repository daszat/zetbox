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
    using System.Collections.ObjectModel;
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
    using Zetbox.API.Common;

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

        private ICompoundObject _object;
        public ICompoundObject Object { get { return _object; } }

        #region Property Management

        private List<Property> _propertyList = null;
        private void FetchPropertyList()
        {
            if (_propertyList == null)
            {
                // load properties from MetaContext
                var cpDef = _object.GetCompoundObjectDefinition(FrozenContext);
                _propertyList = new List<Property>();
                foreach (Property p in cpDef.Properties)
                {
                    _propertyList.Add(p);
                }
            }
        }

        private ReadOnlyProjectedList<Property, BaseValueViewModel> _propertyModelList;
        /// <summary>
        /// A read only list of all known BaseValueViewModels fetched from the ObjectClass.
        /// </summary>
        public IReadOnlyList<BaseValueViewModel> PropertyModels
        {
            get
            {
                if (_propertyModelList == null)
                {
                    FetchPropertyModels();
                    _propertyModelList = new ReadOnlyProjectedList<Property, BaseValueViewModel>(
                        _propertyList,
                        p => _propertyModels[p],
                        m => null); //m.Property);
                    OnPropertyModelsCreated();
                }
                return _propertyModelList;
            }
        }

        /// <summary>
        /// Called after the PropertyModels list has been created.
        /// </summary>
        protected virtual void OnPropertyModelsCreated()
        {
        }

        private LookupDictionary<Property, Property, BaseValueViewModel> _propertyModels;
        private void FetchPropertyModels()
        {
            if (_propertyModels == null)
            {
                FetchPropertyList();
                _propertyModels = new LookupDictionary<Property, Property, BaseValueViewModel>(
                    _propertyList,
                    k => k,
                    v =>
                    {
                        var result = BaseValueViewModel.Fetch(ViewModelFactory, DataContext, this, v, v.GetPropertyValueModel(Object));
                        result.IsReadOnly = IsReadOnly;
                        return result;
                    });
            }
        }

        private LookupDictionary<string, Property, BaseValueViewModel> _propertyModelsByName;
        /// <summary>
        /// Dictionary of BaseValueViewModels with the property name as the key.
        /// </summary>
        public LookupDictionary<string, Property, BaseValueViewModel> PropertyModelsByName
        {
            get
            {
                if (_propertyModelsByName == null)
                {
                    FetchPropertyModels();
                    _propertyModelsByName = new LookupDictionary<string, Property, BaseValueViewModel>(
                        _propertyList,
                        k => k.Name,
                        v => _propertyModels[v]
                    );
                    OnPropertyModelsByNameCreated();
                }
                return _propertyModelsByName;
            }
        }

        /// <summary>
        /// Called after the PropertyModelsByName dictionary has been created.
        /// </summary>
        protected virtual void OnPropertyModelsByNameCreated()
        {
        }

        private ReadOnlyCollection<PropertyGroupViewModel> _propertyGroups;

        /// <summary>
        /// A read only collection of property groups. See CreatePropertyGroups for more information.
        /// </summary>
        public ReadOnlyCollection<PropertyGroupViewModel> PropertyGroups
        {
            get
            {
                if (_propertyGroups == null)
                {
                    _propertyGroups = new ReadOnlyCollection<PropertyGroupViewModel>(CreatePropertyGroups());

                }
                return _propertyGroups;
            }
        }

        /// <summary>
        /// Creates the property groups list.
        /// </summary>
        /// <remarks>
        /// Property groups are created based on the properties summary tags. Due to the fact, 
        /// that properties can have more than one summary
        /// tag, properties may appear in more than one property group. Properties with no
        /// summary tags appears in the "Uncategorised" group. Note, that currently
        /// summary tags may not contain spaces as the space is defined as the seperator.
        /// You can override this method to add custom property groups.
        /// </remarks>
        /// <returns>List of property groups</returns>
        protected virtual List<PropertyGroupViewModel> CreatePropertyGroups()
        {
            FetchPropertyModels();
            var isAdmin = CurrentPrincipal != null ? CurrentPrincipal.IsAdministrator() : false;
            var zbBaseModule = FrozenContext.GetQuery<Module>().Single(m => m.Name == "ZetboxBase");

            return _propertyList
                        .SelectMany(p => (String.IsNullOrEmpty(p.CategoryTags) ? Properties.Resources.Uncategorised : p.CategoryTags)
                                            .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                                            .Select(s => new { Category = s.Trim(), SortKey = GetCategorySortKey(s.Trim()), Property = p }))
                        .Where(x => isAdmin || x.Category != "Hidden")
                        .GroupBy(x => x.SortKey, x => x)
                        .OrderBy(group => group.Key)
                        .Select(group =>
                        {
                            var tag = group.First().Category;
                            var lst = group.Select(p => _propertyModels[p.Property]).Cast<ViewModel>().ToList();

                            var translatedTag = Assets.GetString(zbBaseModule, ZetboxAssetKeys.CategoryTags, tag);
                            if (string.IsNullOrWhiteSpace(translatedTag))
                            {
                                translatedTag = Assets.GetString(group.First().Property.Module, ZetboxAssetKeys.CategoryTags, tag, tag);
                            }

                            if (lst.Count == 1)
                            {
                                return (PropertyGroupViewModel)ViewModelFactory.CreateViewModel<SinglePropertyGroupViewModel.Factory>().Invoke(
                                    DataContext, this, tag, translatedTag, lst);
                            }
                            else
                            {
                                return (PropertyGroupViewModel)ViewModelFactory.CreateViewModel<MultiplePropertyGroupViewModel.Factory>().Invoke(
                                    DataContext, this, tag, translatedTag, lst);
                            }
                        })
                        .ToList();
        }

        private string GetCategorySortKey(string cat)
        {
            switch (cat)
            {
                case "Summary": return "1|Summary";
                case "Main": return "2|Main";
                default: return "3|" + cat;
                case "Meta": return "4|Meta";
            }
        }

        public LookupDictionary<string, PropertyGroupViewModel, PropertyGroupViewModel> PropertyGroupsByName
        {
            get
            {
                return new LookupDictionary<string, PropertyGroupViewModel, PropertyGroupViewModel>(PropertyGroups, mdl => mdl.Title, mdl => mdl);
            }
        }

        private PropertyGroupViewModel _selectedPropertyGroup;
        public PropertyGroupViewModel SelectedPropertyGroup
        {
            get
            {
                if (_selectedPropertyGroup == null && PropertyGroups.Count > 0)
                {
                    var main = PropertyGroupsByName.ContainsKey("Main") ? PropertyGroupsByName["Main"] : null;
                    var summary = PropertyGroupsByName.ContainsKey("Summary") ? PropertyGroupsByName["Summary"] : null;
                    _selectedPropertyGroup = main ?? summary ?? PropertyGroups.FirstOrDefault();
                }
                return _selectedPropertyGroup;
            }
            set
            {
                // only accept new value if it is a contained model
                // Do not accept null's
                if (value != null && (PropertyGroupsByName.ContainsKey(value.Name) && PropertyGroupsByName[value.Name] == value))
                {
                    _selectedPropertyGroup = value;
                    OnPropertyChanged("SelectedPropertyGroup");
                    OnPropertyChanged("SelectedPropertyGroupName");
                }
            }
        }
        public string SelectedPropertyGroupName
        {
            get
            {
                return SelectedPropertyGroup == null ? null : SelectedPropertyGroup.Name;
            }
            set
            {
                if (PropertyGroupsByName.ContainsKey(value))
                {
                    SelectedPropertyGroup = PropertyGroupsByName[value];
                }
            }
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

        protected bool isReadOnlyStore = false;
        /// <summary>
        /// Specifies, that the underlying object should be read only. Note: this sets every property to read only true. 
        /// In constructors use isReadOnlyStore instead. It will be propergated down later.
        /// </summary>
        public virtual bool IsReadOnly
        {
            get
            {
                if (DataContext.IsElevatedMode) return false;
                return isReadOnlyStore;
            }
            set
            {
                if (isReadOnlyStore != value)
                {
                    isReadOnlyStore = value;
                    if (_propertyModels != null)
                    {
                        foreach (var e in _propertyModels)
                        {
                            e.Value.IsReadOnly = isReadOnlyStore;
                        }
                    }
                    OnPropertyChanged("IsReadOnly");
                }
            }
        }
    }
}
