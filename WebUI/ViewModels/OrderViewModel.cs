using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.ViewModels
{
    public class OrderViewModel
    {

        public static OrderViewModel Current
        {
            get
            {
                var cart = HttpContext.Current.Session["Cart"] as OrderViewModel;
                if (null == cart)
                {
                    cart = new OrderViewModel();
                    HttpContext.Current.Session["Cart"] = cart;
                }
                return cart;
            }
        }

        public DateTime pickUpTime { get; set; }
        public double totalPrice { get; set; }
        public List<SNProductViewModel> snProductList { get; set; }
        public List<OrderLineViewModel> orderLineList { get; set; }

        public OrderViewModel()
        {
            snProductList = new List<SNProductViewModel>();
            orderLineList = new List<OrderLineViewModel>();
        }

        public void AddSNProduct(SNProductViewModel snProduct)
        {
            snProductList.Add(snProduct);
        }

        public void AddOrderLine(OrderLineViewModel orderLine)
        {
            orderLineList.Add(orderLine);
        }

    }
}