using Model;
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
    public class ShoppingCartController : Controller
    {
        public OrderViewModel shoppingCart { get; set; }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return Details();
        }

        [HttpPost]
        public async Task<ActionResult> PostOrder(String day, int hour, int minute)
        {
            LocalService service = new LocalService();

            //Making pickUpTime
            DateTime pickUpTime = new DateTime();
            pickUpTime = DateTime.Now;
            TimeSpan ts = new TimeSpan(hour, minute, 0);
            pickUpTime = pickUpTime.Date + ts;
            switch (day)
            {
                case "Tomorrow":
                    pickUpTime = pickUpTime.AddDays(1);
                    break;

                case "Days2":
                    pickUpTime = pickUpTime.AddDays(2);
                    break;

                case "Days3":
                    pickUpTime = pickUpTime.AddDays(3);
                    break;

                case "Days4":
                    pickUpTime = pickUpTime.AddDays(4);
                    break;

                case "Days5":
                    pickUpTime = pickUpTime.AddDays(5);
                    break;

                default:
                    break;

            }
            OrderViewModel.Current.PickUpTime = pickUpTime;

            var response = await service.PostOrder(OrderViewModel.Current);
            if (response.IsSuccessStatusCode)
            {
                Session["Cart"] = null;
                return this.Json(new { success = true, text = "Order placed correctly." });
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                Session["Cart"] = null;
                return this.Json(new { success = false, text = "Insufficient product quantity, please try again." });
            }
            else
            {
                return this.Json(new { success = false, text = response.ReasonPhrase });
            }
        }

        // GET: ShoppingCart/Details/5
        public ActionResult Details()
        {
            return View((OrderViewModel)OrderViewModel.Current);
        }

        // GET: ShoppingCart/Create
        [HttpPost]
        public ActionResult AddToCart(int productId, double price, string name, string ImageUrl)
        {
            shoppingCart = OrderViewModel.Current;

            ProductViewModel product = new ProductViewModel();

            product.ProductId = productId;
            product.ProductName = name;
            product.ImageUrl = ImageUrl;
            product.ProductPrice = price;

            bool isInCart = false;
            foreach(OrderLineViewModel order in shoppingCart.OrderLineList)
            {
                if(order.Product.ProductId == product.ProductId) { order.Quantity++; isInCart = true; break; }
            }
            if (!isInCart)
            {
                OrderLineViewModel orderLine = new OrderLineViewModel(1, product);
                shoppingCart.OrderLineList.Add(orderLine);
            }
            


            Session["Cart"] = shoppingCart;
            return this.Json(new { success = true, text = "Item added to your shopping cart" });
        }


        // POST: ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int productId)
        {
            shoppingCart = OrderViewModel.Current;
            shoppingCart.OrderLineList.Remove(shoppingCart.OrderLineList.Find(x => x.Product.ProductId == productId));


            Session["Cart"] = shoppingCart;
            return this.Json(new { success = true, text = "Item removed from your shopping cart" });
        }

        // POST: ShoppingCart/UpdateQuantity/1
        [HttpPost]
        public ActionResult UpdateQuantity(int productId, int quantity)
        {
            OrderViewModel shoppingCart = OrderViewModel.Current;
            foreach(OrderLineViewModel i in shoppingCart.OrderLineList)
            {
                if (i.Product.ProductId == productId)
                {
                    if (i.Quantity > 0) { i.Quantity = quantity; Session["Cart"] = shoppingCart; return this.Json(new { success = true }); }
                }
            }
            return this.Json(new { success = false });
        }
    }
}
