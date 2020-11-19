using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderRepository : IOrderRepository
    {
        IDbConnection conn;
        public OrderRepository()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
        public List<Order> GetAllOrders()
        {
            //Gets ID of all orders, then gets each order by this id
            conn.Open();
            List<Order> result = new List<Order>();
            List<int> ids = conn.Query<int>("SELECT [OrderId] FROM [Order]").ToList();
            conn.Close();
            foreach (int i in ids)
            {
                result.Add(GetOrderById(i));
            }

            return result;
        }

        public Order GetOrderById(int id)
        {
            ProductRepository prodR = new ProductRepository();
            PersonRepository persR = new PersonRepository();
            conn.Open();
            Order result = conn.Query<Order>("SELECT [OrderId], [OrderStatus], [PickUpTime], [OrderedTime], [TotalPrice] FROM [Order] WHERE OrderId =@OrderId", new { OrderId = id }).SingleOrDefault();
            if (result == null) { conn.Close(); return null; }

            List<String> snList = conn.Query<String>("SELECT [SerialNumber] FROM [SNProduct] WHERE OrderId =@OrderId", new { OrderId = id }).ToList();
            List<int> orderLineList = conn.Query<int>("SELECT [OrderLineId] FROM [OrderLine] WHERE OrderId =@OrderId", new { OrderId = id }).ToList();
            conn.Close();

            result.Customer = persR.GetPersonById(conn.Query<int>("SELECT [CustomerId] FROM [Order] WHERE OrderId =@OrderId", new { OrderId = id }).SingleOrDefault());
            result.Employee = persR.GetPersonById(conn.Query<int>("SELECT [EmployeeId] FROM [Order] WHERE OrderId =@OrderId", new { OrderId = id }).SingleOrDefault());

            foreach(String sn in snList)
            {
                result.AddSNProduct(prodR.GetSNProductBySerialNumber(sn));
            }
            foreach(int orderLineId in orderLineList)
            {
                int noSnId = conn.Query<int>("SELECT [NoSNProductId] FROM [OrderLine] WHERE OrderLineId =@OrderLineId", new { OrderLineId = orderLineId }).SingleOrDefault();
                OrderLine ol = conn.Query<OrderLine>("SELECT [OrderLineId], [Quantity], [OrderId] FROM [OrderLine] WHERE OrderLineId =@OrderLineId", new { OrderLineId = orderLineId }).SingleOrDefault();
                ol.NoSNProduct = conn.Query<NoSNProduct>("SELECT [NoSNProduct].[NoSNProductId], [Product].[ProductId], [Product].[ProductName], [Product].[Barcode], [Product].[ProductPrice], [Product].[StockQuantity] FROM [Product] INNER JOIN [NoSNProduct] ON [Product].[ProductId] = [NoSNProduct].[ProductId] WHERE [NoSNProduct].[NoSNProductId] = @NoSNProductId",
                new { NoSNProductId = noSnId }).SingleOrDefault();
                result.AddOrderLine(ol);
            }

            return result;
        }

        public List<Order> GetOrdersByStatus(String status)
        {
            //Gets ID of all orders, then gets each order by this id
            conn.Open();
            List<Order> result = new List<Order>();
            List<int> ids = conn.Query<int>("SELECT [OrderId] FROM [Order] WHERE [OrderStatus] =@OrderStatus", new { OrderStatus = status}).ToList();
            conn.Close();
            foreach (int i in ids)
            {
                result.Add(GetOrderById(i));
            }

            return result;
        }

        public Order InsertOrder(Order order)
        {
            conn.Open();
            int rowsAffected = conn.Execute("INSERT INTO [Order] VALUES(@PickUpTime, @OrderedTime, @OrderStatus, @TotalPrice, @CustomerId, @EmployeeId)",
                new { PickUpTime = order.PickUpTime, OrderedTime = order.OrderedTime, OrderStatus = order.OrderStatus, TotalPrice = order.TotalPrice, CustomerId = order.Customer.PersonId, EmployeeId = order.Employee.PersonId }) ;
            int id = conn.Query<int>("SELECT @@IDENTITY").SingleOrDefault();
            order.OrderId = id;
            conn.Close();
            
            ProductRepository pr = new ProductRepository();
            foreach(SNProduct product in order.SnProductList)
            {
                product.OrderId = id;
                pr.UpdateSNProduct(product);
            }
            foreach(OrderLine orderLine in order.OrderLineList)
            {
                orderLine.OrderId = id;
                conn.Open();
                conn.Execute("INSERT INTO [OrderLine] VALUES(@Quantity, @OrderId, @NoSNProductId)",
                    new { Quantity = orderLine.Quantity, OrderId = orderLine.OrderId, NoSNProductId = orderLine.NoSNProduct.NoSNProductId });
                conn.Close();
            }

            if (rowsAffected >= 1) { return order; }
            else { return null; }
        }

        public bool UpdateOrder(Order order)
        {
            ProductRepository prodR = new ProductRepository();
            Order oldOrder = GetOrderById(order.OrderId);
            conn.Open();

            int rowsAffected = conn.Execute("UPDATE [Order] SET PickUpTime=@PickUpTime, OrderStatus=@OrderStatus, TotalPrice=@TotalPrice, CustomerId=@CustomerId, EmployeeId=@EmployeeId WHERE OrderId = @OrderId",
                new { PickUpTime = order.PickUpTime, OrderStatus = order.OrderStatus, TotalPrice = order.TotalPrice, CustomerId = order.Customer.PersonId, EmployeeId = order.Employee.PersonId, OrderId = order.OrderId });

            conn.Close();

            //Check and update new OrderLines
            if (oldOrder.OrderLineList.Count != order.OrderLineList.Count)
            {
                List<OrderLine> newOrderLines = new List<OrderLine>(order.OrderLineList);
                foreach(OrderLine ol in order.OrderLineList)
                {
                    if (ol.OrderLineId > 0) 
                    { 
                        newOrderLines.Remove(ol);
                        //Update orderLines that were already inserted (Quantity might have changed)
                        conn.Open();
                        conn.Execute("UPDATE [OrderLine] SET Quantity = @Quantity WHERE OrderLineId = @OrderLineId", 
                            new { Quantity = ol.Quantity, OrderLineId = ol.OrderLineId });
                        conn.Close();
                    }
                    //It will leave inside newOrderLines only new OrderLines, later to insert
                }
                //Insert all newOrderLines
                foreach (OrderLine orderLine in newOrderLines)
                {
                    orderLine.OrderId = order.OrderId;
                    conn.Open();
                    conn.Execute("INSERT INTO [OrderLine] VALUES(@Quantity, @OrderId, @NoSNProductId)",
                        new { Quantity = orderLine.Quantity, OrderId = orderLine.OrderId, NoSNProductId = orderLine.NoSNProduct.NoSNProductId });
                    conn.Close();
                }
            }

            //Check and update new SnProducts
            if(oldOrder.SnProductList.Count != order.SnProductList.Count)
            {
                List<SNProduct> newSnProducts = new List<SNProduct>(order.SnProductList);
                foreach (SNProduct snp in oldOrder.SnProductList)
                {
                    foreach (SNProduct snp2 in newSnProducts)
                    {
                        if (snp.SNProductId == snp2.SNProductId) { newSnProducts.Remove(snp2); break; }
                        //It will leave inside newSnProducts only new SnProducts, later to update
                    }
                }
                //Update all newSnProduct
                foreach(SNProduct snProduct in newSnProducts)
                {
                    snProduct.OrderId = order.OrderId;
                    prodR.UpdateSNProduct(snProduct);
                }
            }
            

            if (rowsAffected >= 1) { return true; }
            else { return false; }
        }
    }
}
