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

namespace Zetbox.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.API.Async;

    [ViewModelDescriptor]
    public class CompoundObjectPropertyViewModel : ValueViewModel<CompoundObjectViewModel, ICompoundObject>, IValueViewModel<CompoundObjectViewModel>
    {
        public new delegate CompoundObjectPropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public CompoundObjectPropertyViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
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

        protected override ZbTask<CompoundObjectViewModel> GetValueFromModel()
        {
            return new ZbTask<CompoundObjectViewModel>(ZbTask.Synchron, () =>
            {
                if (!_valueCacheInititalized)
                {
                    UpdateValueCache();
                }
                return _valueCache;
            });
        }

        protected override void SetValueToModel(CompoundObjectViewModel value)
        {
            if (value == null) throw new ArgumentNullException("value", "Cannot set a CompoundObject property to null");

            _valueCache = value;
            _valueCacheInititalized = true;
            ValueModel.Value = value.Object;
        }

        private void UpdateValueCache()
        {
            _valueCache = CompoundObjectViewModel.Fetch(ViewModelFactory, DataContext, this, ValueModel.Value);
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

        public override void ClearValue()
        {
            ValueModel.Value = DataContext.CreateCompoundObject(DataContext.GetInterfaceType(ReferencedType.GetDataType()));
        }
    }
}
