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

        public async Task<ActionResult> orders()
        {
            await GetUserOrders();
            return View((UserViewModel)UserViewModel.Current);
        }
        public ActionResult user()
        {
            return View((UserViewModel)UserViewModel.Current);
        }

        [HttpGet]
        public async Task<ActionResult> GetUserOrders()
        {
            LocalService service = new LocalService();
            Boolean result = await service.GetUserOrders();

            if (result)
            {
                return this.Json(new { success = true, text = "Orders loaded correctly." });
            }
            else
            {
                return this.Json(new { success = false, text = "Couldn't get the orders." });
            }
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                LocalService service = new LocalService();
                var result = await service.Register(register);

                if (result)
                {
                    ViewBag.Message = "Check your email and confirm your account, you must be confirmed "
                            + "before you can log in.";
                    return View("Info");
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    return this.Json(new { success = false, text = "Register fail!" });
                }

            }
            // If we got this far, something failed, redisplay form
            return View(register);
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
                return View("~/Views/Shared/Error.cshtml");
            }

            LocalService service = new LocalService();
            try
            {
                var result = await service.Authenticate(model.Email, model.Password);
                if (result != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
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
