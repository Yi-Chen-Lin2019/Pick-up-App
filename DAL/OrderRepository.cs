﻿using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
            using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                Order result = conn.Query<Order>("SELECT [OrderId], [OrderStatus], [PickUpTime], [OrderedTime], [TotalPrice], [RowId], CAST(RowId as bigint) AS RowIdBig  FROM [Order] WHERE OrderId =@OrderId", new { OrderId = id }).SingleOrDefault();
                if (result == null) { conn.Close(); return null; }

                //List<String> snList = conn.Query<String>("SELECT [SerialNumber] FROM [SNProduct] WHERE OrderId =@OrderId", new { OrderId = id }).ToList();
                List<int> orderLineList = conn.Query<int>("SELECT [OrderLineId] FROM [OrderLine] WHERE OrderId =@OrderId", new { OrderId = id }).ToList();
                conn.Close();

                result.Customer = persR.GetPersonById(conn.Query<string>("SELECT [CustomerId] FROM [Order] WHERE OrderId =@OrderId", new { OrderId = id }).SingleOrDefault());
                try
                {
                    result.Employee = persR.GetPersonById(conn.Query<string>("SELECT [EmployeeId] FROM [Order] WHERE OrderId =@OrderId", new { OrderId = id }).SingleOrDefault());  
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                /*
                foreach (String sn in snList)
                {
                    result.AddSNProduct(prodR.GetSNProductBySerialNumber(sn));
                }
                */
                foreach (int orderLineId in orderLineList)
                {
                    int productId = conn.Query<int>("SELECT [ProductId] FROM [OrderLine] WHERE OrderLineId =@OrderLineId", new { OrderLineId = orderLineId }).SingleOrDefault();
                    OrderLine ol = conn.Query<OrderLine>("SELECT [OrderLineId], [Quantity], [OrderId] FROM [OrderLine] WHERE OrderLineId =@OrderLineId", new { OrderLineId = orderLineId }).SingleOrDefault();
                    ol.Product = conn.Query<Product>("SELECT [Product].[ProductId], [Product].[ProductName], [Product].[Barcode], [Product].[ProductPrice], [Product].[StockQuantity], [Product].[RowId], CAST([Product].[RowId] as bigint) AS RowIdBig FROM [Product] WHERE [ProductId] = @ProductId",
                    new { ProductId = productId }).SingleOrDefault();
                    result.AddOrderLine(ol);
                }
                return result;
            }

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
            int rowsAffected;
            using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                SqlTransaction transaction;
                transaction = (SqlTransaction)conn.BeginTransaction();
                    try
                    {
                        #region insertOrder
                        rowsAffected = conn.Execute("INSERT INTO [Order] VALUES(@PickUpTime, @OrderedTime, @OrderStatus, @TotalPrice, @CustomerId, null, null)",
                    new { PickUpTime = order.PickUpTime, OrderedTime = order.OrderedTime, OrderStatus = order.OrderStatus, TotalPrice = order.TotalPrice, CustomerId = order.Customer.Id },
                    transaction);
                        int newOrderId = conn.ExecuteScalar<int>("SELECT @@IDENTITY", null, transaction);
                        order.OrderId = newOrderId;
                        order.RowIdBig = conn.Query<int>("SELECT CAST(RowId as bigint) AS RowIdBig from [Order] where OrderId = @OrderId", new { OrderId = order.OrderId }, transaction).SingleOrDefault();
                        #endregion

                        #region insertOrderLines
                        //if order inserted then we insert orderLines
                        if (rowsAffected >= 1)
                        {
                                if (order.OrderLineList.Count != 0)
                                {
                                    ProductRepository productRepository = new ProductRepository();
                                    foreach (var item in order.OrderLineList)
                                    {
                                        item.OrderId = order.OrderId;
                                        //get the info about the product
                                        Product productRetrived = conn.Query<Product>("SELECT [ProductId], [ProductName], [Barcode], [ProductPrice], [StockQuantity] ,[RowId], CAST(RowId as bigint) AS RowIdBig FROM [Product] WHERE ProductId =@ProductId", new { ProductId = item.Product.ProductId }, transaction).SingleOrDefault();
                                        //compare the quantities of item
                                        if (productRetrived.StockQuantity >= item.Quantity)
                                        {
                                                //insert the orderline
                                                conn.Execute("INSERT INTO [OrderLine] VALUES(@Quantity, @OrderId, @ProductId)",
                                                new { Quantity = item.Quantity, OrderId = item.OrderId, ProductId = item.Product.ProductId }, transaction);
                                                //update product stock quantity
                                                productRetrived.StockQuantity -= item.Quantity;
                                                rowsAffected = conn.Execute("UPDATE [Product] SET StockQuantity = @StockQuantity WHERE ProductId = @ProductId AND (cast(@OldRowIdBig as binary(8)) = RowId)",
                                                new { StockQuantity = productRetrived.StockQuantity, ProductId = productRetrived.ProductId, OldRowIdBig = productRetrived.RowIdBig }, transaction);
                                                order.TotalPrice += productRetrived.ProductPrice * item.Quantity;
                                        }
                                        else
                                        {
                                            throw new OutOfStockException();
                                        }
                                    }
                                }   
                        }
                        #endregion

                        #region updateOrderTotalPrice
                        int affected = conn.Execute("UPDATE [Order] SET TotalPrice=@TotalPrice WHERE OrderId = @OrderId",
                               new { TotalPrice = order.TotalPrice, OrderId = order.OrderId }, transaction);
                        if (affected == 1) 
                        {
                           transaction.Commit();
                           return order; 
                        } 
                        else 
                        {
                           transaction.Rollback();
                           return null; 
                        };
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return null;
                    }
                }
        }
        private decimal InsertOrderLineList(OrderLine orderLine)
        {
            decimal subTotalOfOrderLines = 0;
            ProductRepository pr = new ProductRepository();
            Product productRetrived = pr.GetProductById(orderLine.Product.ProductId);
            var rowBig = productRetrived.RowIdBig;
            if (productRetrived.StockQuantity >= orderLine.Quantity)
            {
                using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            conn.Execute("INSERT INTO [OrderLine] VALUES(@Quantity, @OrderId, @ProductId)",
                       new { Quantity = orderLine.Quantity, OrderId = orderLine.OrderId, ProductId = orderLine.Product.ProductId },
                       transaction);

                            productRetrived.StockQuantity -= orderLine.Quantity;
                            while (!pr.UpdateProduct(productRetrived))
                            {                              
                                pr.UpdateProduct(productRetrived);
                                
                            }
                            transaction.Commit();
                            subTotalOfOrderLines += productRetrived.ProductPrice * orderLine.Quantity;

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            transaction.Rollback();
                        }
                    }
                }
            }
            else
            {
                throw new OutOfStockException("Out of stock, please wait for in stock or try with different quantity");

            }
            return subTotalOfOrderLines;
        }

        /*
        private decimal InsertSNProductList(Order order)
        {
            ProductRepository pr = new ProductRepository();
            decimal subTotalOfSnProducts = 0;

            foreach (SNProduct product in order.SnProductList)
            {
                product.OrderId = order.OrderId;
                product.Product.StockQuantity -= 1;
                pr.UpdateSNProduct(product);
                subTotalOfSnProducts += product.Product.ProductPrice;
            }
            return subTotalOfSnProducts;
        }
        */




        public bool UpdateOrder(Order order)
        {
            ProductRepository prodR = new ProductRepository();
            
            int rowsAffected = 0;
            using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        rowsAffected = conn.Execute("UPDATE [Order] SET PickUpTime=@PickUpTime, OrderStatus=@OrderStatus, TotalPrice=@TotalPrice, EmployeeId=@EmployeeId WHERE OrderId = @OrderId AND (cast(@OldRowIdBig as binary(8)) = RowId)",
                    new { PickUpTime = order.PickUpTime, OrderStatus = order.OrderStatus, TotalPrice = order.TotalPrice, EmployeeId = order.Employee.Id, OrderId = order.OrderId, OldRowIdBig = order.RowIdBig }, transaction);





                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                    }
                }

            }
            //Order oldOrder = GetOrderById(order.OrderId);
            ////Check and update new OrderLines
            //if (oldOrder.OrderLineList.Count != order.OrderLineList.Count)
            //{
            //    List<OrderLine> newOrderLines = new List<OrderLine>(order.OrderLineList);
            //    foreach (OrderLine ol in order.OrderLineList)
            //    {
            //        if (ol.OrderLineId > 0)
            //        {
            //            newOrderLines.Remove(ol);
            //            //Update orderLines that were already inserted (Quantity might have changed)
            //            conn.Open();
            //            conn.Execute("UPDATE [OrderLine] SET Quantity = @Quantity WHERE OrderLineId = @OrderLineId",
            //                new { Quantity = ol.Quantity, OrderLineId = ol.OrderLineId });
            //            conn.Close();
            //        }
            //        //It will leave inside newOrderLines only new OrderLines, later to insert
            //    }
            //    //Insert all newOrderLines
            //    foreach (OrderLine orderLine in newOrderLines)
            //    {
            //        orderLine.OrderId = order.OrderId;
            //        using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            //        {
            //            conn.Open();
            //            conn.Execute("INSERT INTO [OrderLine] VALUES(@Quantity, @OrderId, @ProductId)",
            //                new { Quantity = orderLine.Quantity, OrderId = orderLine.OrderId, ProductId = orderLine.Product.ProductId });
            //        }
            //    }
            //}

            /*
            //Check and update new SnProducts
            if (oldOrder.SnProductList.Count != order.SnProductList.Count)
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
                foreach (SNProduct snProduct in newSnProducts)
                {
                    snProduct.OrderId = order.OrderId;
                    prodR.UpdateSNProduct(snProduct);
                }
            }
            */

            if (rowsAffected >= 1) { return true; }
            else { return false; }
        }
    }
}
