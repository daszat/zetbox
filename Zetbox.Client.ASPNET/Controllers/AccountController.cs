using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Client.Presentables;
using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace Zetbox.Client.ASPNET.Controllers
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Benutzername")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Password { get; set; }

        [Display(Name = "Eingeloggt bleiben?")]
        public bool RememberMe { get; set; }
    }

    [Authorize]
    public class AccountController: ZetboxController
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(AccountController));

        private readonly Func<IZetboxContext> _ctxFactory;
        private readonly Func<IZetboxServerContext> _srvCtxFactory;

        public AccountController(IViewModelFactory vmf, ZetboxContextHttpScope contextScope, Func<IZetboxContext> ctxFactory, Func<IZetboxServerContext> srvCtxFactory)
            : base(vmf, contextScope)
        {
            _ctxFactory = ctxFactory;
            _srvCtxFactory = srvCtxFactory;
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var zbIdentity = DataContext.GetQuery<Identity>().SingleOrDefault(i => i.UserName.ToLower() == model.UserName.ToLower());
                if (zbIdentity == null)
                {
                    ModelState.AddModelError("", "Kein Eintrag unter diesem Benutzernamen und Passwort gefunden.");
                    return View(model);
                }

                if (string.IsNullOrWhiteSpace(zbIdentity.Password))
                {
                    ModelState.AddModelError("", "Kein Eintrag unter diesem Benutzernamen und Passwort gefunden.");
                    return View(model);
                }

                if (!BCrypt.Net.BCrypt.Verify(model.Password, zbIdentity.Password) == true)
                {
                    ModelState.AddModelError("", "Kein Eintrag unter diesem Benutzernamen und Passwort gefunden.");
                    return View(model);
                }

                _log.InfoFormat("User {0} logged in", model.UserName);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim("FullName", zbIdentity.DisplayName.IfNullOrWhiteSpace(model.UserName)),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties { };

                await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);   
                return Redirect("/");
            }

            _log.WarnFormat("User {0} failed logging in", model.UserName);
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Kein Eintrag unter diesem Benutzernamen und Passwort gefunden.");
            return View(model);
        }
    }
}
