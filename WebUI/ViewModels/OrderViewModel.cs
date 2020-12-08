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

        public DateTime PickUpTime { get; set; }
        public DateTime OrderedTime { get; set; }
        public double TotalPrice { get; set; }
        public UserViewModel Customer { get; set; }
        public UserViewModel Employee { get; set; }
        public List<OrderLineViewModel> OrderLineList { get; set; }

        public OrderViewModel()
        {
            OrderLineList = new List<OrderLineViewModel>();
        }

        public void AddOrderLine(OrderLineViewModel orderLine)
        {
            OrderLineList.Add(orderLine);
        }

    }
}