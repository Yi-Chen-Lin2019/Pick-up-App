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

        [HttpPost]
        public ActionResult OrderAgain(int orderId)
        {
            Boolean result = false;
            foreach(OrderViewModel i in UserViewModel.Current.Orders)
            {
                if (i.OrderId == orderId) { Session["Cart"] = i; result = true; }
            }

            if (result)
            {
                return this.Json(new { success = true, text = "Cart is now ready to order again." });
            }
            else
            {
                return this.Json(new { success = false, text = "Failure." });
            }
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel register)
        {

            LocalService service = new LocalService();
            var result = await service.Register(register);

            if (result)
            {
                ViewBag.message = "Registration was successful. Please confirm an email in order to log in.";
                return View("Info");
            }
            else
            {
                ViewBag.message = "Registration failed. Please try again.";
                return View("Info");
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



        // GET: Account/Edit
        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            UserViewModel userViewModel;
            try
            {
                LocalService service = new LocalService();
                userViewModel = await service.GetUserInfo();
                Session["User"] = userViewModel;
            }
            catch
            {
                throw new Exception("not logged in or couldn't be found");
            }
            return View(userViewModel);
        }

        // POST: Account/Edit
        [HttpPost]
        public async Task<ActionResult> Edit(UserViewModel userViewModel)
        {
            try
            {
                LocalService service = new LocalService();

                if(await service.PutUserInfo(userViewModel.transformToPerson()))
                {
                    Session["User"] = userViewModel;
                }
                else
                {
                    throw new Exception("failed to update");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(userViewModel);
        }

    }
}
