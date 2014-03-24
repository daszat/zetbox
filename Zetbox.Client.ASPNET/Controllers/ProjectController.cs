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

        public ActionResult Index(int? page)
        {
            var vmdl = ViewModelFactory.CreateViewModel<ProjectSearchViewModel.Factory>().Invoke(DataContext, null);
            vmdl.Page = page ?? vmdl.Page;
            return View(vmdl);
        }

        [HttpPost]
        public ActionResult Index(ProjectSearchViewModel mdl)
        {
            return View(mdl);
        }

        //
        // GET: /Project/
        public ActionResult Create()
        {
            var vmdl = ViewModelFactory.CreateViewModel<DataObjectEditViewModel<Zetbox.App.Projekte.Projekt>.Factory>().Invoke(DataContext, null);
            return View(vmdl);
        }

        //
        // GET: /Project/
        [HttpPost]
        public ActionResult Create(DataObjectEditViewModel<Zetbox.App.Projekte.Projekt> vmdl)
        {
            TryValidateModel(vmdl);
            if (ModelState.IsValid)
            {
                DataContext.SubmitChanges();
                return RedirectToAction("Details", new { id = vmdl.Object.ID });
            }
            else
            {
                return View(vmdl);
            }
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
        public ActionResult Edit(DataObjectEditViewModel<Zetbox.App.Projekte.Projekt> vmdl)
        {
            TryValidateModel(vmdl);
            if (ModelState.IsValid)
            {
                DataContext.SubmitChanges();
                return RedirectToAction("Details", new { id = vmdl.ID });
            }
            else
            {
                return View(vmdl);
            }
        }

        public ActionResult Details(int id)
        {
            var vmdl = ViewModelFactory.CreateViewModel<DataObjectEditViewModel<Zetbox.App.Projekte.Projekt>.Factory>().Invoke(DataContext, null);
            vmdl.ID = id;
            return View(vmdl);
        }
    }
}
