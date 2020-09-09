using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zetbox.Client.ASPNET.ViewModels;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.ASPNET.Controllers
{
    public class ProjektController : ZetboxController
    {
        public ProjektController(IViewModelFactory vmf, ZetboxContextHttpScope contextScope) : base(vmf, contextScope)
        {
        }

        #region GetViewModels
        private MvcProjektListViewModel GetListViewModel()
        {
            var vmdl = ViewModelFactory.CreateViewModel<MvcProjektListViewModel.Factory>().Invoke(DataContext, null);
            return vmdl;
        }

        private MvcProjektViewModel GetViewModel(int id)
        {
            var vmdl = ViewModelFactory.CreateViewModel<MvcProjektViewModel.Factory>().Invoke(DataContext, null);
            vmdl.ID = id;
            return vmdl;
        }
        #endregion

        public IActionResult Index()
        {
            var vmdl = GetListViewModel();

            //dynamic args = Session["SearchProjekt"];
            //if (args != null)
            //{
            //    vmdl.Page = args.Page;
            //    vmdl.NamePart = args.NamePart;
            //}

            return View(vmdl);
        }

        [HttpPost]
        public ActionResult Index(MvcProjektListViewModel vmdl)
        {
            //Session["SearchProjekt"] = new
            //{
            //    vmdl.Page,
            //    vmdl.NamePart,
            //};

            vmdl.InvalidateResult();
            return View(vmdl);
        }

        public ActionResult Create()
        {
            var vmdl = GetViewModel(0);
            return View(vmdl);
        }

        [HttpPost]
        public ActionResult Create(MvcProjektViewModel vmdl)
        {
            var obj = vmdl.Object;

            Validate(vmdl);
            if (ModelState.IsValid)
            {
                DataContext.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(vmdl);
        }

        public ActionResult Edit(int ID)
        {
            var vmdl = GetViewModel(ID);
            return View(vmdl);
        }

        [HttpPost]
        public ActionResult Edit(int ID, string action, MvcProjektViewModel vmdl)
        {
            var obj = vmdl.Object;
            switch (action)
            {
                case "Delete":
                    DataContext.Delete(obj);
                    DataContext.SubmitChanges();
                    return RedirectToAction("Index");
            }

            Validate(vmdl);
            if (ModelState.IsValid)
            {
                DataContext.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(vmdl);
        }
    }
}
