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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor]
    public class DataTypeViewModel
        : DataObjectViewModel
    {
        public new delegate DataTypeViewModel Factory(IZetboxContext dataCtx, ViewModel parent, DataType dt);

        private readonly DataType _dataType;

        public DataTypeViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            DataType dt)
            : base(appCtx, dataCtx, parent, dt)
        {
            _dataType = dt;
        }

        public override string Name
        {
            get
            {
                if (_dataType.Module != null)
                    return Assets.GetString(_dataType.Module, ZetboxAssetKeys.DataTypes, ZetboxAssetKeys.ConstructNameKey(_dataType), _dataType.Name);
                else
                    return _dataType.Name;
            }
        }

        public string Description
        {
            get
            {
                if (_dataType.Module != null)
                    return Assets.GetString(_dataType.Module, ZetboxAssetKeys.DataTypes, ZetboxAssetKeys.ConstructDescriptionKey(_dataType), _dataType.Description);
                else
                    return _dataType.Description;
            }
        }

        public override System.Drawing.Image Icon
        {
            get
            {
                if(base.Icon == null)
                    Task.Run(async () => base.Icon = await IconConverter.ToImage(_dataType.DefaultIcon));
                return base.Icon;
            }
        }

        protected override PropertyGroupViewModel CreatePropertyGroup(string tag, string translatedTag, PropertyGroupCollection lst)
        {
            if (tag == "GUI")
            {
                return ViewModelFactory.CreateViewModel<CustomPropertyGroupViewModel.Factory>()
                    .Invoke(
                        DataContext,
                        this,
                        tag,
                        translatedTag,
                        new[] {
                            ViewModelFactory.CreateViewModel<StackPanelViewModel.Factory>()
                                .Invoke(
                                    DataContext,
                                    this,
                                    tag,
                                    new[] {
                                        ViewModelFactory.CreateViewModel<GroupBoxViewModel.Factory>().Invoke(DataContext, this, "Settings",
                                            lst.GetWithKeys().Where(kv => !kv.Key.StartsWith("Show"))
                                                .Select(kv => kv.Value)),
                                        ViewModelFactory.CreateViewModel<GroupBoxViewModel.Factory>().Invoke(DataContext, this, "List",
                                            lst.GetWithKeys().Where(kv => kv.Key.StartsWith("Show"))
                                                .Select(kv => kv.Value)
                                                .Concat(new[] {
                                                    ViewModelFactory.CreateViewModel<LabeledViewContainerViewModel.Factory>().Invoke(DataContext, this, "Preview", "", ViewModelFactory.CreateViewModel<PropertiesPrewiewViewModel.Factory>().Invoke(DataContext, this, _dataType))
                                                })),
                                    })
                        });
            }
            else
            {
                return base.CreatePropertyGroup(tag, translatedTag, lst);
            }
        }

        public string DescribedType
        {
            get
            {
                return _dataType.GetDataTypeString();
            }
        }

        protected override void OnObjectPropertyChanged(string propName)
        {
            base.OnObjectPropertyChanged(propName);
            switch (propName)
            {
                case "Name":
                case "Module":
                    OnPropertyChanged("Name");
                    OnPropertyChanged("DescribedType");
                    break;
                case "Description":
                    OnPropertyChanged("Description");
                    break;
            }
        }
    }
}
