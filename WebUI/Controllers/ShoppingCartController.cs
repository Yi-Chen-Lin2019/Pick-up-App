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

        // POST: ShoppingCart/Send
        [HttpPost]
        public async Task<ActionResult> PostOrder()
        {
            //TODO remove authentication from here

            LocalService serviceA = new LocalService();
            Token token = await serviceA.Authenticate("superadmin@pickup.com", "Pwd123!");
            LocalService service = new LocalService();

            //TODO read the selected pickup time
            OrderViewModel.Current.PickUpTime = DateTime.Now;
            Order order = OrderViewModel.Current.fromViewModelToOrderModel();
            await service.PostOrder(order);

            return this.Json(new { success = true, text = "Order placed correctly." });
        }

        // GET: ShoppingCart/Details/5
        public ActionResult Details()
        {
            return View((OrderViewModel)OrderViewModel.Current);
        }

        // GET: ShoppingCart/Create
        [HttpPost]
        public ActionResult AddToCart(int productId, double price, string name, string image)
        {
            shoppingCart = OrderViewModel.Current;

            ProductViewModel product = new ProductViewModel();

            product.id = productId;
            product.name = name;
            product.image = image;
            product.price = price;

            bool isInCart = false;
            foreach(OrderLineViewModel order in shoppingCart.OrderLineList)
            {
                if(order.product.id == product.id) { order.quantity++; isInCart = true; break; }
            }
            if (!isInCart)
            {
                OrderLineViewModel orderLine = new OrderLineViewModel(1, product);
                shoppingCart.OrderLineList.Add(orderLine);
            }
            


            Session["Cart"] = shoppingCart;
            return this.Json(new { success = true, text = "Item added to your shopping cart" });
        }


        // GET: ShoppingCart/Delete/5
        [HttpPost]
        public ActionResult RemoveFromCart(int productId)
        {
            shoppingCart = OrderViewModel.Current;
            shoppingCart.OrderLineList.Remove(shoppingCart.OrderLineList.Find(x => x.product.id == productId));


            Session["Cart"] = shoppingCart;
            return this.Json(new { success = true, text = "Item removed from your shopping cart" });
        }
    }
}
