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
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.App.Test;

    [ViewModelDescriptor]
    public class TestObjClassViewModel
        : DataObjectViewModel
    {
        public new delegate TestObjClassViewModel Factory(IZetboxContext dataCtx, ViewModel parent, TestObjClass obj);

        private readonly TestObjClass _obj;

        public TestObjClassViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            TestObjClass obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            _obj = obj;
        }

        protected override List<PropertyGroupViewModel> CreatePropertyGroups()
        {
            var result = base.CreatePropertyGroups();

            result.Add(ViewModelFactory.CreateViewModel<CustomPropertyGroupViewModel.Factory>()
                .Invoke(DataContext, this, "Stack 1", "Stack 1", new[] 
                {
                    ViewModelFactory.CreateViewModel<StackPanelViewModel.Factory>().Invoke(DataContext, this, "Stack", new [] 
                    {
                        ViewModelFactory.CreateViewModel<GroupBoxViewModel.Factory>().Invoke(DataContext, this, "Grp 1", new []
                        {
                            PropertyModelsByName["StringProp"],
                            PropertyModelsByName["MyIntProperty"],
                            PropertyModelsByName["ObjectProp"],
                        }),
                        ViewModelFactory.CreateViewModel<GroupBoxViewModel.Factory>().Invoke(DataContext, this, "Grp 2", new []
                        {
                            PropertyModelsByName["StringProp"],
                            PropertyModelsByName["TestEnumProp"],
                            PropertyModelsByName["TestEnumWithDefault"],
                        }),
                    }),
                })
            );

            result.Add(ViewModelFactory.CreateViewModel<CustomPropertyGroupViewModel.Factory>()
               .Invoke(DataContext, this, "Grid 1", "Grid 1", new[] 
                {
                    ViewModelFactory.CreateViewModel<GridPanelViewModel.Factory>().Invoke(DataContext, this, "Grid", new [] 
                    {
                        new GridPanelViewModel.Cell(0, 0, ViewModelFactory.CreateViewModel<GroupBoxViewModel.Factory>().Invoke(DataContext, this, "Grp 1", new []
                        {
                            PropertyModelsByName["StringProp"],
                            PropertyModelsByName["MyIntProperty"],
                            PropertyModelsByName["ObjectProp"],
                        })),
                        new GridPanelViewModel.Cell(0, 1, ViewModelFactory.CreateViewModel<GroupBoxViewModel.Factory>().Invoke(DataContext, this, "Grp 2", new []
                        {
                            PropertyModelsByName["StringProp"],
                            PropertyModelsByName["TestEnumProp"],
                            PropertyModelsByName["TestEnumWithDefault"],
                        })),
                    }),
                })
           );

            return result;
        }

        protected override PropertyGroupViewModel CreatePropertyGroup(string tag, string translatedTag, SortedDictionary<string, ViewModel> lst)
        {
            return base.CreatePropertyGroup(tag, translatedTag, lst);
        }
    }
}
