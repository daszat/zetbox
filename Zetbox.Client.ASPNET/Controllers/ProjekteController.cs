using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.ASPNET.Controllers
{
    public class ProjekteController : ZetboxController
    {
        public ProjekteController(IViewModelFactory vmf, ZetboxContextHttpScope contextScope) : base(vmf, contextScope)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
