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
            throw new NotImplementedException();
        }

        public Order GetOrderById()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrdersByStatus()
        {
            throw new NotImplementedException();
        }

        public bool InsertOrder(Order order)
        {
            conn.Open();
            int rowsAffected = conn.Execute("INSERT INTO [Order] VALUES(@PickUpTime, @OrderStatus, @TotalPrice, @CustomerId, @EmployeeId)",
                new { PickUpTime = order.PickUpTime, OrderStatus = order.OrderStatus, TotalPrice = order.TotalPrice, CustomerId = order.Customer.PersonId, EmployeeId = order.Employee.PersonId }) ;
            int id = conn.Query<int>("SELECT @@IDENTITY").SingleOrDefault();
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

            if (rowsAffected >= 1) { return true; }
            else { return false; }
        }

        public bool UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
