using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        public Order shoppingCart { get; set; }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return Details();
        }

        // GET: ShoppingCart/Details/5
        public ActionResult Details()
        {
            return View((Order)Order.Current);
        }

        // GET: ShoppingCart/Create
        [HttpPost]
        public ActionResult AddToCart(int productId, double price, string name, string image, int quantity, bool isSNProduct)
        {


            shoppingCart = Order.Current;

            Product product;
            if (isSNProduct)
            {
                product = new SNProduct();
            }
            else
            {
                product = new Product();
            }

            product.id = productId;
            product.name = name;
            product.image = image;
            product.price = price;


            OrderLine orderLine = new OrderLine(quantity, product);

            if (product is SNProduct)
            {
                shoppingCart.snProductList.Add((SNProduct)product);
            }
            else
            {
                shoppingCart.orderLineList.Add(orderLine);
            }


            Session["Cart"] = shoppingCart;
            return this.Json(new { success = true, text = "Item added to your shopping cart" });
        }


        // GET: ShoppingCart/Delete/5
        [HttpPost]
        public ActionResult RemoveFromCart(int productId)
        {
            shoppingCart = Order.Current;
            shoppingCart.orderLineList.Remove(shoppingCart.orderLineList.Find(x => x.product.id == productId));
            shoppingCart.snProductList.Remove(shoppingCart.snProductList.Find(x => x.id == productId));


            Session["Cart"] = shoppingCart;
            return this.Json(new { success = true, text = "Item removed from your shopping cart" });
        }
    }
}
