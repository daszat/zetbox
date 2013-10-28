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

namespace Zetbox.Client.Presentables.ObjectBrowser
{
    [ViewModelDescriptor]
    public class ObjectClassViewModel : DataObjectViewModel
    {
        public new delegate ObjectClassViewModel Factory(IZetboxContext dataCtx, ViewModel parent, ObjectClass cls);

        public ObjectClassViewModel(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, ObjectClass cls)
            : base(appCtx, dataCtx, parent, cls)
        {
            _cls = cls;
            cls.PropertyChanged += ModulePropertyChanged;
        }

        private ObjectClass _cls;

        public override string Name
        {
            get
            {
                if (_cls.Module != null)
                    return Assets.GetString(_cls.Module, ZetboxAssetKeys.DataTypes, ZetboxAssetKeys.ConstructNameKey(_cls), _cls.Name);
                else
                    return _cls.Name;
            }
        }

        public override System.Drawing.Image Icon
        {
            get { return IconConverter.ToImage(_cls.DefaultIcon); }
        }

        #region public interface

        private List<object> _children;
        public IEnumerable Children
        {
            get
            {
                if (_children == null)
                {
                    _children = ObjectClasses
                        .Cast<object>()
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
                    _objectClassesCache = LoadObjectClasses();
                }
                return _objectClassesCache;
            }
        }

        private InstanceListViewModel _dashboardViewModel;
        public ViewModel DashboardViewModel
        {
            get
            {
                if (_dashboardViewModel == null)
                {
                    _dashboardViewModel = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, this, _cls, null);
                    _dashboardViewModel.AllowAddNew = true;
                    _dashboardViewModel.AllowDelete = true;
                    _dashboardViewModel.ViewMethod = Zetbox.App.GUI.InstanceListViewMethod.Details;
                    _dashboardViewModel.Commands.Add(ViewModelFactory.CreateViewModel<EditDataObjectClassCommand.Factory>().Invoke(DataContext, this, _cls));

                }
                return _dashboardViewModel;
            }
        }

        #endregion

        #region Utilities and UI callbacks

        private List<ObjectClassViewModel> LoadObjectClasses()
        {
            return FrozenContext.GetQuery<ObjectClass>()
                .Where(c => c.BaseObjectClass != null && c.BaseObjectClass.ExportGuid == _cls.ExportGuid)
                .OrderBy(c => c.Name)
                .ToList()
                .Select(c => ViewModelFactory.CreateViewModel<ObjectClassViewModel.Factory>().Invoke(DataContext, this, c))
                .ToList();
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
    }
}
