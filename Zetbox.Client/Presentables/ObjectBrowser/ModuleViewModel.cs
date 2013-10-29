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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.API.Configuration;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Client.Presentables.ZetboxBase;
using Zetbox.API.Common;
using Zetbox.Client.Presentables.ValueViewModels;

namespace Zetbox.Client.Presentables.ObjectBrowser
{
    [ViewModelDescriptor]
    public class ModuleViewModel : DataObjectViewModel
    {
        public class TreeNodeSimpleObjects : ViewModel
        {
            public new delegate TreeNodeSimpleObjects Factory(IZetboxContext dataCtx, ViewModel parent);

            public TreeNodeSimpleObjects(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent)
                : base(dependencies, dataCtx, parent)
            {
            }

            public override string Name
            {
                get
                {
                    return WorkspaceViewModelResources.SimpleObjects;
                }
            }
            public IEnumerable Children { get; set; }

            public ViewModel DashboardViewModel
            {
                get
                {
                    return null;
                }
            }
        }

        public new delegate ModuleViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Module mdl);

        public ModuleViewModel(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, Module mdl)
            : base(appCtx, dataCtx, parent, mdl)
        {
            _module = mdl;
            _module.PropertyChanged += ModulePropertyChanged;
            this.IsReadOnly = true;
        }

        public override string Name
        {
            get
            {
                return Assets.GetString(_module, ZetboxAssetKeys.Modules, ZetboxAssetKeys.ConstructNameKey(_module), _module.Name);
            }
        }

        #region public interface

        private List<object> _children;
        public IEnumerable Children
        {
            get
            {
                if (_children == null)
                {
                    var simple = ViewModelFactory.CreateViewModel<TreeNodeSimpleObjects.Factory>().Invoke(DataContext, this);
                    simple.Children = this.SimpleObjectClasses;
                    _children = ObjectClasses
                        .Cast<object>()
                        .Concat(new[] {  simple })
                        .ToList();
                }
                return _children;
            }
        }

        private List<ObjectClassViewModel> _objectClassesCache = null;
        public IEnumerable<ObjectClassViewModel> ObjectClasses
        {
            get
            {
                if (_objectClassesCache == null)
                {
                    _objectClassesCache = LoadObjectClasses(false);
                }
                return _objectClassesCache;
            }
        }

        private List<ObjectClassViewModel> _simpleObjectClassesCache = null;
        public IEnumerable<ObjectClassViewModel> SimpleObjectClasses
        {
            get
            {
                if (_simpleObjectClassesCache == null)
                {
                    _simpleObjectClassesCache = LoadObjectClasses(true);
                }
                return _simpleObjectClassesCache;
            }
        }

        #endregion

        #region Utilities and UI callbacks

        private List<ObjectClassViewModel> LoadObjectClasses(bool simpleObjects)
        {
            return FrozenContext.GetQuery<ObjectClass>()
                .Where(c => c.Module.ExportGuid == _module.ExportGuid && c.BaseObjectClass == null && c.IsSimpleObject == simpleObjects)
                .OrderBy(c => c.Name)
                .ToList()
                .Select(c => ViewModelFactory.CreateViewModel<ObjectClassViewModel.Factory>().Invoke(DataContext, this, c))
                .ToList();
        }

        public ViewModel DashboardViewModel
        {
            get 
            {
                return this;
            }
        }

        #endregion

        #region PropertyChanged event handlers

        private void ModulePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Name": OnPropertyChanged("Name"); break;
            }
        }

        #endregion

        private Module _module;
    }
}
