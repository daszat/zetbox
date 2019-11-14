﻿// This file is part of zetbox.
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
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using System.Web.Http;
    using System.IO;

    public class ZetboxApiController : ApiController
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
            var body = Request.Content.ReadAsStreamAsync().Result;
            body.Position = 0;
            var sr = new StreamReader(body);
            var json = sr.ReadToEnd();
            Newtonsoft.Json.JsonConvert.PopulateObject(json, data);
            return data;
        }
    }
}
