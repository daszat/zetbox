using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.ASPNET.Controllers
{
    public class EnumTestController : ZetboxController
    {
        public EnumTestController(IViewModelFactory vmf, ZetboxContextHttpScope contextScope)
            : base(vmf, contextScope)
        {

        }
        //
        // GET: /EnumTest/

        public ActionResult Index()
        {
            return View(ViewModelFactory.CreateViewModel<SearchViewModel<Zetbox.App.Test.PropertyEnumTest>.Factory>().Invoke(DataContext, null));
        }

        //
        // GET: /EnumTest/Details/5

        public ActionResult Details(int id)
        {
            var vmdl = ViewModelFactory.CreateViewModel<DataObjectEditViewModel<Zetbox.App.Test.PropertyEnumTest>.Factory>().Invoke(DataContext, null);
            vmdl.ID = id;
            return View(vmdl);
        }

        //
        // GET: /EnumTest/Create

        public ActionResult Create()
        {
            var vmdl = ViewModelFactory.CreateViewModel<DataObjectEditViewModel<Zetbox.App.Test.PropertyEnumTest>.Factory>().Invoke(DataContext, null);
            return View(vmdl);
        }

        //
        // POST: /EnumTest/Create

        [HttpPost]
        public ActionResult Create(DataObjectEditViewModel<Zetbox.App.Test.PropertyEnumTest> mdl)
        {
            DataContext.SubmitChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /EnumTest/Edit/5

        public ActionResult Edit(int id)
        {
            var vmdl = ViewModelFactory.CreateViewModel<DataObjectEditViewModel<Zetbox.App.Test.PropertyEnumTest>.Factory>().Invoke(DataContext, null);
            vmdl.ID = id;
            return View(vmdl);
        }

        //
        // POST: /EnumTest/Edit/5

        [HttpPost]
        public ActionResult Edit(DataObjectEditViewModel<Zetbox.App.Test.PropertyEnumTest> mdl)
        {
            DataContext.SubmitChanges();
            return RedirectToAction("Details", new { id = mdl.ID });
        }
    }
}
