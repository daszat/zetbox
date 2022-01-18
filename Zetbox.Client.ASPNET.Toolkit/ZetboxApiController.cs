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
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using System.IO;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public class ZetboxApiController : Controller
    {
        private ZetboxContextHttpScope _contextScope;
        protected IZetboxContext DataContext
        {
            get
            {
                return _contextScope.Context;
            }
        }
        protected IViewModelFactory ViewModelFactory { get; private set; }

        public ZetboxApiController(IViewModelFactory vmf, ZetboxContextHttpScope contextScope)
        {
            _contextScope = contextScope;
            this.ViewModelFactory = vmf;
        }

        protected T ExtractIDataObjectFromBody<T>() where T : class, IPersistenceObject
        {
            var data = DataContext.Internals().CreateUnattached<T>();
            Request.Body.Position = 0;
            var sr = new StreamReader(Request.Body);
            var json = sr.ReadToEnd();
            JsonConvert.PopulateObject(json, data);
            return data;
        }
    }
}
