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
    public class TestCustomObjectViewModel
        : DataObjectViewModel
    {
        public new delegate TestCustomObjectViewModel Factory(IZetboxContext dataCtx, ViewModel parent, TestCustomObject obj);

        private readonly TestCustomObject _obj;

        public TestCustomObjectViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            TestCustomObject obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            _obj = obj;
        }

        protected override PropertyGroupViewModel CreatePropertyGroup(string tag, string translatedTag, SortedDictionary<string, ViewModel> lst)
        {
            return base.CreatePropertyGroup(tag, translatedTag, lst);
        }
    }
}
