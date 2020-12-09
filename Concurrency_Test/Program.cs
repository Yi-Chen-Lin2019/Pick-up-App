using BusinessLayer;
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
            lostUpdateProductQuantityTest();
            //lostUpdateOrderTest();
        }

        private static void lostUpdateOrderTest()
        {
            OrderRepository orderR = new OrderRepository();

            Console.WriteLine("Order status at the start is: "
                + orderR.GetOrderById(1).OrderStatus);
            Thread a = new Thread(new ThreadStart(ConfirmSameOrderEmployeeA));
            Thread b = new Thread(new ThreadStart(DenySameOrderEmployeeB));
            a.Start();
            b.Start();
            a.Join();
            b.Join();
            OrderRepository orderRAfter = new OrderRepository();
            Console.WriteLine("Order status at the end is: "
                + orderR.GetOrderById(1).OrderStatus);
            Console.ReadLine();
        }

        private static void DenySameOrderEmployeeB()
        {
            OrderRepository orderR = new OrderRepository();
            Order order = orderR.GetOrderById(1);
            Thread.Sleep(6000);
            order.OrderStatus = "Error";
            orderR.UpdateOrder(order);
            Console.WriteLine("Thread deny: Order status is: "
                + orderR.GetOrderById(1).OrderStatus);
        }

        private static void ConfirmSameOrderEmployeeA()
        {
            OrderRepository orderR = new OrderRepository();
            Order order = orderR.GetOrderById(1);
            Thread.Sleep(2000);
            order.OrderStatus = "Confirmed";
            orderR.UpdateOrder(order);
            Console.WriteLine("Thread confirm: Order status is: "
               + orderR.GetOrderById(1).OrderStatus);

        }
        public static void lostUpdateProductQuantityTest()
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
            Console.WriteLine("Two customer have buy 50 bottles of milk individually (-100), the remaining stock: "
                + prodRAfter.GetProductById(5).StockQuantity);
            Console.WriteLine("Two customer have buy 50 boxes of sour cream individually (-100), the remaining stock: "
                + prodRAfter.GetProductById(6).StockQuantity);
            Console.ReadLine();
        }

        private static void BuySameProductClientA()
        {
            ProductManagement pm = new ProductManagement();
            var sn = pm.GetAllProducts();
            


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
                    Product = prodR.GetProductById(5),
                    Quantity = 50
                },

                new OrderLine()
                {
                    Product = prodR.GetProductById(6),
                    Quantity = 50
                }
            };
            order.OrderLineList = orderLines;


            var customer = new Person() { UserName = "superadmin@pickup.com" };
            order.Customer = customer;


            //Time and Total Price
            order.OrderedTime = DateTime.Now;
            order.PickUpTime = DateTime.Now;
            order.PickUpTime.AddHours(2);
            order.PickUpTime.AddDays(1);


            Thread.Sleep(5000);

            Order retrievedOrder = null;

            try
            {
                retrievedOrder = ordR.InsertOrder(order);
                Console.WriteLine("Thread a order ID: " + retrievedOrder.OrderId);
                Console.WriteLine("Total price: " + retrievedOrder.TotalPrice);
            }
            catch (OutOfStockException se)
            {
                Console.WriteLine(se.Message ); 
            }
            
            

            

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
                    Product = prodR.GetProductById(5),
                    Quantity = 50
                },

                new OrderLine()
                {
                    Product = prodR.GetProductById(6),
                    Quantity = 50
                }
            };
            order.OrderLineList = orderLines;

            var customer = new Person() { UserName = "superadmin@pickup.com" };
            order.Customer = customer;
            //order.Employee = perR.GetPersonById(2);


            //Time and Total Price
            order.OrderedTime = DateTime.Now;
            order.PickUpTime = DateTime.Now;
            order.PickUpTime.AddHours(2);
            order.PickUpTime.AddDays(1);

            Order retrievedOrder = null;

            try
            {
                retrievedOrder = ordR.InsertOrder(order);
                Console.WriteLine("Thread b order ID: " + retrievedOrder.OrderId);
                Console.WriteLine("Total price: " + retrievedOrder.TotalPrice);
            }
            catch (OutOfStockException se)
            {
                Console.WriteLine(se.Message);
            }


            //Act
            //Order retrievedOrder = ordR.InsertOrder(order);
            

        }
    }
}
