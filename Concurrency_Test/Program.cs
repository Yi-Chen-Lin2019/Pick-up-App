using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Concurrency_Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ProductRepository prodR = new ProductRepository();
            Console.WriteLine("Original stock of product ID 5 (Milk): "
                + prodR.GetProductById(5).StockQuantity);
            Console.WriteLine("Original stock of product ID 6 (Sour Cream): "
                + prodR.GetProductById(6).StockQuantity);
            Thread a = new Thread(new ThreadStart(BuySameProductCustomerA));
            Thread b = new Thread(new ThreadStart(BuySameProductCustomerB));
            a.Start();
            b.Start();
            a.Join();
            b.Join();
            ProductRepository prodRAfter = new ProductRepository();
            Console.WriteLine("Two customer has buy 50 bottles of milk individually (-100), the remaining stock: "
                + prodRAfter.GetProductById(5).StockQuantity);
            Console.WriteLine("Two customer has buy 50 boxes of sour cream individually (-100), the remaining stock: "
                + prodRAfter.GetProductById(6).StockQuantity);
            Console.ReadLine();
        }

        private static void BuySameProductCustomerA()
        {
            PersonRepository perR = new PersonRepository();
            ProductRepository prodR = new ProductRepository();
            OrderRepository ordR = new OrderRepository();

            Order order = new Order("Received");


            List<OrderLine> orderLines = new List<OrderLine>()
            {
                new OrderLine(){
                    NoSNProduct = prodR.GetNoSNProductByProductId(5)[0],
                    Quantity = 50
                },

                new OrderLine()
                {
                    NoSNProduct = prodR.GetNoSNProductByProductId(6)[0],
                    Quantity = 50
                }
            };
            order.OrderLineList = orderLines;


            order.Customer = perR.GetPersonById(1);
            order.Employee = perR.GetPersonById(2);


            //Time and Total Price
            order.OrderedTime = DateTime.Now;
            order.PickUpTime = DateTime.Now;
            order.PickUpTime.AddHours(2);
            order.PickUpTime.AddDays(1);


            Thread.Sleep(5000);

            Order retrievedOrder = null;
            //try
            //{
            retrievedOrder = ordR.InsertOrder(order);
            //}
            //catch (Exception e)
            //{
            //    Thread.Sleep(5000);
            //    retrievedOrder = ordR.InsertOrder(order);
            //}

            Console.WriteLine("Thread a order ID: " + retrievedOrder.OrderId);
            Console.WriteLine("Total price: " + retrievedOrder.TotalPrice);

        }

        private static void BuySameProductCustomerB()
        {
            PersonRepository perR = new PersonRepository();
            ProductRepository prodR = new ProductRepository();
            OrderRepository ordR = new OrderRepository();

            Order order = new Order("Received");


            List<OrderLine> orderLines = new List<OrderLine>()
            {
                new OrderLine(){
                    NoSNProduct = prodR.GetNoSNProductByProductId(5)[0],
                    Quantity = 50
                },

                new OrderLine()
                {
                    NoSNProduct = prodR.GetNoSNProductByProductId(6)[0],
                    Quantity = 50
                }
            };
            order.OrderLineList = orderLines;


            order.Customer = perR.GetPersonById(2);
            order.Employee = perR.GetPersonById(2);


            //Time and Total Price
            order.OrderedTime = DateTime.Now;
            order.PickUpTime = DateTime.Now;
            order.PickUpTime.AddHours(2);
            order.PickUpTime.AddDays(1);




            //Act
            Order retrievedOrder = ordR.InsertOrder(order);
            Console.WriteLine("Thread b order ID: " + retrievedOrder.OrderId);
            Console.WriteLine("Total price: " + retrievedOrder.TotalPrice);

        }
    }
}
