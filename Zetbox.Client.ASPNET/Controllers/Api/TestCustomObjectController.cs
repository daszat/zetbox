using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Zetbox.App.Projekte;
using Zetbox.App.Test;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.ASPNET.Controllers.Api
{
    public class TestCustomObjectController : ZetboxApiController
    {
        public TestCustomObjectController(IViewModelFactory vmf, ZetboxContextHttpScope contextScope) : base(vmf, contextScope)
        {
        }

        // GET api/project
        public IEnumerable<TestCustomObject> Get(string name = null, int limit = 1000)
        {
            var qry = DataContext.GetQuery<TestCustomObject>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                qry = qry.Where(i => i.PersonName.ToLower().Contains(name.ToLower()));
            }

            qry = qry.OrderBy(i => i.PersonName).ThenBy(i => i.ID);

            return qry.ToArray();
        }

        // GET api/project/5
        public TestCustomObject Get(int id)
        {
            return DataContext.Find<TestCustomObject>(id);
        }
    }
}