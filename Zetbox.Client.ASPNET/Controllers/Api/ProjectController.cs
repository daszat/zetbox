using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Zetbox.API;
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

        // POST api/projects
        public Projekt Post([FromBody]Projekt data)
        {
            data = ExtractIDataObjectFromBody<Projekt>();

            var obj = DataContext.Create<Projekt>();
            obj.ApplyChangesFrom(data);
            obj.ReloadReferences();

            DataContext.SubmitChanges();

            return obj;
        }

        // PUT api/projects/5
        public Projekt Put(int id, [FromBody]Projekt data)
        {
            data = ExtractIDataObjectFromBody<Projekt>();

            var obj = DataContext.Find<Projekt>(id);

            obj.ApplyChangesFrom(data);
            obj.ReloadReferences();

            DataContext.SubmitChanges();

            return obj;
        }

        // DELETE api/projects/5
        public void Delete(int id)
        {
            var obj = DataContext.Find<Projekt>(id);
            DataContext.Delete(obj);

            DataContext.SubmitChanges();
        }
    }
}