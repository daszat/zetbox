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
using System.Collections;

namespace Zetbox.Client.Presentables.ObjectBrowser
{
    public class ModuleViewModel : DataObjectViewModel
    {
        public class TreeNodeSimpleObjects
        {
            public string Name
            {
                get
                {
                    return "Simple Objects";
                }
            }
            public IEnumerable Children { get; set; }
        }

        public new delegate ModuleViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Module mdl);

        private Func<IZetboxContext> _ctxFactory;

        public ModuleViewModel(
            IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory, IZetboxContext dataCtx, ViewModel parent,
            Module mdl)
            : base(appCtx, dataCtx, parent, mdl)
        {
            _ctxFactory = ctxFactory;
            _module = mdl;
            _module.PropertyChanged += ModulePropertyChanged;
        }

        #region public interface

        public IEnumerable Children
        {
            get
            {
                return ObjectClasses
                    .Cast<object>()
                    .Concat(new[] { new TreeNodeSimpleObjects() { Children = this.SimpleObjectClasses } });
            }
        }

        private List<InstanceListViewModel> _objectClassesCache = null;
        public IEnumerable<InstanceListViewModel> ObjectClasses
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

        private List<InstanceListViewModel> _simpleObjectClassesCache = null;
        public IEnumerable<InstanceListViewModel> SimpleObjectClasses
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

        private List<InstanceListViewModel> LoadObjectClasses(bool simpleObjects)
        {
            var result = new List<InstanceListViewModel>();
            var datatypes = FrozenContext.GetQuery<ObjectClass>()
                .Where(dt => dt.Module.ExportGuid == _module.ExportGuid && dt.IsSimpleObject == simpleObjects)
                .OrderBy(dt => dt.Name)
                .ToList();
            foreach (var cls in datatypes)
            {
                var mdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, this, _ctxFactory, cls, null);
                mdl.AllowAddNew = true;
                mdl.AllowDelete = true;
                mdl.ViewMethod = Zetbox.App.GUI.InstanceListViewMethod.Details;
                mdl.Commands.Add(ViewModelFactory.CreateViewModel<EditDataObjectClassCommand.Factory>().Invoke(DataContext, this, cls));
                result.Add(mdl);
            }

            return result;
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
