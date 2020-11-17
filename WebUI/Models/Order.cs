using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class Order
    {

        public static Order Current
        {
            get
            {
                var cart = HttpContext.Current.Session["Cart"] as Order;
                if (null == cart)
                {
                    cart = new Order();
                    HttpContext.Current.Session["Cart"] = cart;
                }
                return cart;
            }
        }

        public DateTime pickUpTime { get; set; }
        public double totalPrice { get; set; }
        public List<SNProduct> snProductList { get; set; }
        public List<OrderLine> orderLineList { get; set; }

        public Order()
        {
            snProductList = new List<SNProduct>();
            orderLineList = new List<OrderLine>();
        }

        public void AddSNProduct(SNProduct snProduct)
        {
            snProductList.Add(snProduct);
        }

        public void AddOrderLine(OrderLine orderLine)
        {
            orderLineList.Add(orderLine);
        }

    }
}