using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Zetbox.App.Projekte;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.ASPNET.Controllers.Api
{
    public class KundeController : ZetboxApiController
    {
        public KundeController(IViewModelFactory vmf, ZetboxContextHttpScope contextScope) : base(vmf, contextScope)
        {
        }

        // GET api/project
        public IEnumerable<Kunde> Get(string name = null, int limit = 1000)
        {
            var qry = DataContext.GetQuery<Kunde>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                qry = qry.Where(i => i.Kundenname.ToLower().Contains(name.ToLower()));
            }

            qry = qry.OrderBy(i => i.Kundenname).ThenBy(i => i.ID);

            return qry.ToArray();
        }

        // GET api/project/5
        public Kunde Get(int id)
        {
            return DataContext.Find<Kunde>(id);
        }
    }
}