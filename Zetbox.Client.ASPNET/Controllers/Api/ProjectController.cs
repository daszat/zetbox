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
    public class ProjectController : ZetboxApiController
    {
        public ProjectController(IViewModelFactory vmf, ZetboxContextHttpScope contextScope) : base(vmf, contextScope)
        {
        }

        // GET api/project
        public IEnumerable<Projekt> Get(string name = null, int limit = 1000)
        {
            var qry = DataContext.GetQuery<Projekt>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                qry = qry.Where(i => i.Name.ToLower().Contains(name.ToLower()));
            }

            qry = qry.OrderBy(i => i.Name).ThenBy(i => i.ID);

            return qry.ToArray();
        }

        // GET api/project/5
        public Projekt Get(int id)
        {
            return DataContext.Find<Projekt>(id);
        }
    }
}