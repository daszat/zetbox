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

namespace Zetbox.Client.ASPNET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Autofac;
    using Zetbox.Client.Presentables;

    public interface IZetboxModelBinder : IModelBinder
    { 
    }

    public class ZetboxModelBinder : DefaultModelBinder, IZetboxModelBinder
    {
        IViewModelFactory _vmf;
        
        public ZetboxModelBinder(IViewModelFactory vmf)
        {
            _vmf = vmf;
        }

        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            var scope = DependencyResolver.Current.GetService<ZetboxContextHttpScope>();
            return _vmf.CreateViewModel<ViewModel.Factory>(modelType).Invoke(scope.Context, null);
        }
    }
}
