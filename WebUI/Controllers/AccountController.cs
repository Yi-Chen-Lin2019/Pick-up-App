using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebUI.ServiceLayer;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel register)
        {
            LocalService service = new LocalService();
            var result = await service.Register(register);

            if (result) {
                return this.Json(new { success = true, text = "Welcome!" });
            }
            else
            {
                return this.Json(new { success = false, text = "Register fail!" });

            }

        }
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        // POST: /Home/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            LocalService service = new LocalService();
            var result = await service.Authenticate(model.Email, model.Password);
            if (result != null)
            {
                return this.Json(new { success = true, text = "Welcome" });
            }
            else
            {
                return View(model);
            }

        }



        // POST: Account/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
