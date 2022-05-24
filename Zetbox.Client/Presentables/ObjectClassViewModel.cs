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
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.Client.Models;
    using System.Threading.Tasks;

    [ViewModelDescriptor]
    public class ObjectClassViewModel : DataTypeViewModel
    {
        public new delegate ObjectClassViewModel Factory(IZetboxContext dataCtx, ViewModel parent, ObjectClass cls);

        public ObjectClassViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            ObjectClass cls)
            : base(appCtx, dataCtx, parent, cls)
        {
            _class = cls;
        }

        public InterfaceType GetDescribedInterfaceType()
        {
            return _class.GetDescribedInterfaceType();
        }

        private ObjectClass _class;

        protected override List<PropertyGroupViewModel> CreatePropertyGroups()
        {
            var result = base.CreatePropertyGroups();

            var relListMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(
                DataContext,
                this,
                typeof(Relation).GetObjectClass(FrozenContext),
                () => DataContext.GetQuery<Relation>().Where(i => i.A.Type == Object || i.B.Type == Object));
            relListMdl.EnableAutoFilter = false;
            relListMdl.Commands.Add(ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "New Relation", "Creates a new Relation", CreateRelation, null, null));

            var propGrpMdl = ViewModelFactory.CreateViewModel<CustomPropertyGroupViewModel.Factory>().Invoke(DataContext, this, "Relations", "Relations", new ViewModel[] { relListMdl });
            result.Add(propGrpMdl);
            return result;
        }

        public async Task CreateRelation()
        {
            var rel = await _class.CreateRelation();
            await ViewModelFactory.ShowModel(DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, rel), true);
        }
    }
}
