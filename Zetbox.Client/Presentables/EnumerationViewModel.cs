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
    using Zetbox.Client.Presentables.GUI;

    [ViewModelDescriptor]
    public class EnumerationViewModel : DataTypeViewModel
    {
        public new delegate EnumerationViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Enumeration enumeration);

        public EnumerationViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Enumeration enumeration)
            : base(appCtx, dataCtx, parent, enumeration)
        {
            _enumeration = enumeration;
        }

        public InterfaceType GetDescribedInterfaceType()
        {
            return _enumeration.GetDescribedInterfaceType();
        }

        private Enumeration _enumeration;

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
                                    })
                        });
            }
            else if(tag == "Properties" || tag == "Methods" || tag == "DataModel")
            {
                return null;
            }
            else
            {
                return base.CreatePropertyGroup(tag, translatedTag, lst);
            }
        }
    }
}
