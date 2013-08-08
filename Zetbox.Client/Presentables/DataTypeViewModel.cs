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
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

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
                    return Assets.GetString(_dataType.Module, "ZetboxBase.DataTypes", _dataType.Module.Namespace + "." + _dataType.Name, _dataType.Name);
                else
                    return _dataType.Name;
            }
        }

        public string Description
        {
            get
            {
                if (_dataType.Module != null)
                    return Assets.GetString(_dataType.Module, "ZetboxBase.DataTypes", _dataType.Module.Namespace + "." + _dataType.Name + "_description", _dataType.Description);
                else
                    return _dataType.Description;
            }
        }

        protected override List<PropertyGroupViewModel> CreatePropertyGroups()
        {
            var result = base.CreatePropertyGroups();

            if (_dataType is ObjectClass || _dataType is CompoundObject)
            {
                var singleMdl = result.Single(n => n.Name == "Properties");
                var preview = ViewModelFactory.CreateViewModel<PropertiesPrewiewViewModel.Factory>().Invoke(DataContext, this, _dataType);
                var lblMdl = ViewModelFactory.CreateViewModel<LabeledViewContainerViewModel.Factory>().Invoke(DataContext, this, "Preview", "", preview);
                var grpMdl = ViewModelFactory.CreateViewModel<MultiplePropertyGroupViewModel.Factory>().Invoke(DataContext, this, "Properties", singleMdl.PropertyModels.Concat(new[] { lblMdl }).ToArray());

                var idx = result.IndexOf(singleMdl);
                result.Remove(singleMdl);
                result.Insert(idx, grpMdl);
            }

            return result;
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
