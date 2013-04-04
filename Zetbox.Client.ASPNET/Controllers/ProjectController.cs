using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zetbox.Client.Presentables;
using Zetbox.Client.ASPNET.Models;

namespace Zetbox.Client.ASPNET.Controllers
{
    public class ProjectController : ZetboxController
    {
        public ProjectController(IViewModelFactory vmf, ZetboxContextHttpScope contextScope)
            : base(vmf, contextScope)
        {
        }

        public ActionResult Index()
        {
            return View(ViewModelFactory.CreateViewModel<ProjectSearchViewModel.Factory>().Invoke(DataContext, null));
        }

        [HttpPost]
        public ActionResult Index(ProjectSearchViewModel mdl, string foo)
        {
            return View(mdl);
        }

        //
        // GET: /Project/
        public ActionResult Edit(int id)
        {
            var vmdl = ViewModelFactory.CreateViewModel<DataObjectEditViewModel<Zetbox.App.Projekte.Projekt>.Factory>().Invoke(DataContext, null);
            vmdl.ID = id;
            return View(vmdl);
        }
        //
        // GET: /Project/
        [HttpPost]
        public ActionResult Edit(DataObjectEditViewModel<Zetbox.App.Projekte.Projekt> mdl)
        {
            DataContext.SubmitChanges();
            return RedirectToAction("Details", new { id = mdl.ID });
        }

        public ActionResult Details(int id)
        {
            var vmdl = ViewModelFactory.CreateViewModel<DataObjectEditViewModel<Zetbox.App.Projekte.Projekt>.Factory>().Invoke(DataContext, null);
            vmdl.ID = id;
            return View(vmdl);
        }
    }
}
