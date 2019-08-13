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
    public class TaskController : ZetboxApiController
    {
        public TaskController(IViewModelFactory vmf, ZetboxContextHttpScope contextScope) : base(vmf, contextScope)
        {
        }

        // GET api/project
        public IEnumerable<Task> Get(string name = null, int limit = 1000)
        {
            var qry = DataContext.GetQuery<Task>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                qry = qry.Where(i => i.Name.ToLower().Contains(name.ToLower()));
            }

            qry = qry.OrderBy(i => i.Name).ThenBy(i => i.ID);

            return qry.ToArray();
        }

        // GET api/project/5
        public Task Get(int id)
        {
            return DataContext.Find<Task>(id);
        }
    }
}