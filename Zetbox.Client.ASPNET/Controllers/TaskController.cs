using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zetbox.Client.Presentables;
using Zetbox.Client.ASPNET.Models;

namespace Zetbox.Client.ASPNET.Controllers
{
    public class TaskController : ZetboxController
    {
        public TaskController(IViewModelFactory vmf, ZetboxContextHttpScope contextScope)
            : base(vmf, contextScope)
        {
        }
        //
        // GET: /Task/Details/5

        public ActionResult Details(int id)
        {
            var vmdl = ViewModelFactory.CreateViewModel<TaskEditViewModel.Factory>().Invoke(DataContext, null);
            vmdl.ID = id;
            return View(vmdl);
        }

        //
        // GET: /Task/Create

        public ActionResult Create(int project)
        {
            var vmdl = ViewModelFactory.CreateViewModel<TaskEditViewModel.Factory>().Invoke(DataContext, null);
            vmdl.ProjectID = project;
            return View(vmdl);
        }

        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create(int project, TaskEditViewModel mdl)
        {
            mdl.Object.Projekt = DataContext.Find<Zetbox.App.Projekte.Projekt>(project);
            DataContext.SubmitChanges();
            return RedirectToAction("Details", "Project", new { id = project });
        }

        //
        // GET: /Task/Edit/5

        public ActionResult Edit(int id)
        {
            var vmdl = ViewModelFactory.CreateViewModel<TaskEditViewModel.Factory>().Invoke(DataContext, null);
            vmdl.ID = id;
            return View(vmdl);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(TaskEditViewModel mdl)
        {
            DataContext.SubmitChanges();
            return RedirectToAction("Details", new { id = mdl.ID });
        }

        //
        // GET: /Task/Delete/5

        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        //
        // POST: /Task/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            throw new NotImplementedException();
        }
    }
}
