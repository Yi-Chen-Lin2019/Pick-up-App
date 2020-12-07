using Newtonsoft.Json;
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
        public Order()
        {
            //SnProductList = new List<SNProduct>();
            OrderLineList = new List<OrderLine>();
        }
        public Order(int OrderId, String OrderStatus, DateTime PickUpTime, DateTime OrderedTime, decimal TotalPrice, byte[] RowId, Int64 RowIdBig)
        {
            this.OrderId = OrderId;
            this.OrderStatus = OrderStatus;
            this.PickUpTime = PickUpTime;
            this.OrderedTime = OrderedTime;
            this.TotalPrice = TotalPrice;
            this.RowId = RowId;
            this.RowIdBig = RowIdBig;

            //SnProductList = new List<SNProduct>();
            OrderLineList = new List<OrderLine>();
        }
        public Order(String OrderStatus)
        {
            this.OrderStatus = OrderStatus;

            //SnProductList = new List<SNProduct>();
            OrderLineList = new List<OrderLine>();
        }

        public enum StatusEnum
        {
            Error,
            Received,
            Confirmed,
            InProgress,
            Ready,
            PickedUp
        }
        StatusEnum status;

        /*
        public void AddSNProduct(SNProduct snProduct)
        {
            SnProductList.Add(snProduct);
        }
        */
        public void AddOrderLine(OrderLine orderLine)
        {
            OrderLineList.Add(orderLine);
        }

        public int OrderId { get; set; }

        //public List<SNProduct> SnProductList { get; set; }

        public List<OrderLine> OrderLineList { get; set; }

        public Person Customer { get; set; }
        public Person Employee { get; set; }

        public DateTime PickUpTime { get; set; }
        public DateTime OrderedTime { get; set; }

        public decimal TotalPrice { get; set; }

        public String OrderStatus
        {
            get
            {
                return status.ToString();
            }
            set
            {
                try
                {
                    this.status = (StatusEnum)Enum.Parse(typeof(StatusEnum), value, true);
                }
                catch (ArgumentException) { this.status = StatusEnum.Error; }
            }
        }
        [JsonIgnore]
        public byte[] RowId { get; set; }
        [JsonIgnore]
        public Int64 RowIdBig { get; set; }

    }
}
