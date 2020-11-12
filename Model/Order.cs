using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using System.Text;

namespace Model
{
    public class Order
    {
        enum StatusEnum
        {
            Error,
            Received,
            Confirmed,
            InProgress,
            Ready,
            PickedUp
        }
        DateTime pickUpTime;
        StatusEnum status;
        double totalPrice;
        List<SNProduct> snProductList;
        List<OrderLine> orderLineList;
        Person customer;
        Person employee;

        public Order(String status, Person customer, Person employee)
        {
            try
            {
                this.status = (StatusEnum)Enum.Parse(typeof(StatusEnum), status, true);
            }
            catch (ArgumentException) { this.status = StatusEnum.Error; }

            this.customer = customer;
            this.employee = employee;

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

        public List<SNProduct> SnProductList { get { return snProductList; } set { snProductList = value; } }

        public List<OrderLine> OrderLineList { get { return orderLineList; } set { orderLineList = value; } }

        public Person Customer { get; set; }

        public Person Employee { get; set; }

        public DateTime PickUpTime { get; set; }

        public double TotalPrice { get; set; }

        public String Status 
        {
            get{
                return status.ToString();
            }
            set{
                try
                {
                    this.status = (StatusEnum)Enum.Parse(typeof(StatusEnum), value, true);
                }
                catch (ArgumentException) { this.status = StatusEnum.Error; }
            }
        }

    }
}
