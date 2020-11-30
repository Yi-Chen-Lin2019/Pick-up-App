using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductRepository : IProductRepository
    {
        IDbConnection conn;
        public ProductRepository()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        //Category methods
        public List<Category> GetAllCategories()
        {
            conn.Open();

            List<Category> result = conn.Query<Category>("SELECT [CategoryId], [CategoryName] FROM [Category]").ToList();
            conn.Close();

            return result;
        }

        public Category GetCategoryByName(string name)
        {
            conn.Open();
            Category result = conn.Query<Category>("SELECT [CategoryId], [CategoryName] FROM [Category] WHERE CategoryName = @CategoryName", 
                new { CategoryName = name }).FirstOrDefault();
            conn.Close();

            return result;
        }

        public bool UpdateCategory(Category category)
        {
            conn.Open();

            int rowsAffected = conn.Execute("UPDATE [Category] SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId", new { CategoryName = category.CategoryName, CategoryId = category.CategoryId });

            conn.Close();

            if (rowsAffected >= 1) { return true; }
            else { return false; }
        }
        public Category InsertCategory(Category category)
        {
            conn.Open();

            int rowsAffected = conn.Execute(@"INSERT INTO [Category] VALUES(@CategoryName)", new { CategoryName = category.CategoryName });
            category.CategoryId = conn.Query<int>("SELECT @@IDENTITY").FirstOrDefault();
            conn.Close();

            if(rowsAffected >= 1) { return category; }
            else { return null; }

        }
        public bool DeleteCategory(Category category)
        {
            conn.Open();

            int rowsAffected = conn.Execute("DELETE FROM [Category] WHERE [CategoryId] = @CategoryId", new { CategoryId = category.CategoryId });
            conn.Close();

            if (rowsAffected >= 1) { return true; }
            else { return false; }
        }

        //SNProduct methods
        public List<SNProduct> GetAllSNProducts()
        {
            conn.Open();

            List<SNProduct> result = new List<SNProduct>();

            List<String> serialNumberList = conn.Query<String>("SELECT [SerialNumber] FROM [SNProduct]").ToList();
            conn.Close();

            foreach (String serialNumber in serialNumberList)
            {
                result.Add(GetSNProductBySerialNumber(serialNumber));
            }

            return result;
        }

        public SNProduct GetSNProductBySerialNumber(String serialNumber)
        {
            conn.Open();

            SNProduct snProduct = conn.Query<SNProduct>("SELECT [SNProductId], [SerialNumber] FROM [SNProduct] WHERE SerialNumber = @SerialNumber", 
                new { SerialNumber = serialNumber }).FirstOrDefault();
            if(snProduct == null) { conn.Close(); return null;}
            int productId = conn.Query<int>("SELECT [Product].[ProductId] FROM [Product] INNER JOIN [SNProduct] ON [SNProduct].[ProductId] = [Product].[ProductId] WHERE [SNProduct].[SerialNumber] = @SerialNumber", new { SerialNumber = serialNumber }).FirstOrDefault();
            conn.Close();
            snProduct.Product = GetProductById(productId);

            return snProduct;
        }

        public SNProduct InsertSNProduct(SNProduct snProduct)
        {
            conn.Open();
            int rowsAffected = 0;
            if (snProduct.Product != null)
            {
                rowsAffected = conn.Execute(@"INSERT INTO [SNProduct] VALUES(@SerialNumber, null, @ProductId)",
                    new { SerialNumber = snProduct.SerialNumber, ProductId = snProduct.Product.ProductId });
            }

            conn.Close();

            if (rowsAffected >= 1) { return GetSNProductBySerialNumber(snProduct.SerialNumber); }
            else { return null; }
        }

        public bool UpdateSNProduct(SNProduct snProduct)
        {
            conn.Open();

            int rowsAffected = conn.Execute("UPDATE [SNProduct] SET OrderId = @OrderId WHERE ProductId = @ProductId",
                new { OrderId = snProduct.OrderId, ProductId = snProduct.Product.ProductId});

            conn.Close();
            if (rowsAffected >= 1) { return true; }
            else { return false; }
        }

        //NoSNProduct methods

        public List<NoSNProduct> GetAllNoSNProduct()
        {
            conn.Open();

            List<NoSNProduct> productList = conn.Query<NoSNProduct>("SELECT [NoSNProduct].[NoSNProductId], [Product].[ProductId], [Product].[ProductName], [Product].[Barcode], [Product].[ProductPrice], [Product].[StockQuantity], [Product].[RowId], CAST([Product].[RowId] as bigint) AS RowIdBig FROM [Product] INNER JOIN [NoSNProduct] ON [Product].[ProductId] = [NoSNProduct].[ProductId]").ToList();

            foreach (NoSNProduct product in productList)
            {
                product.Category = conn.Query<Category>("SELECT [Category].[CategoryId], [Category].[CategoryName] FROM [Category] INNER JOIN [Product] ON [Category].[CategoryId] = [Product].[CategoryId] WHERE [Product].[ProductId] = @ProductId", new { ProductId = product.ProductId}).FirstOrDefault();
            }

            conn.Close();
            return productList;
        }

        public List<NoSNProduct> GetNoSNProductByProductId(int productId)
        {
            conn.Open();

            List<NoSNProduct> productList = conn.Query<NoSNProduct>("SELECT [NoSNProduct].[NoSNProductId], [Product].[ProductId], [Product].[ProductName], [Product].[Barcode], [Product].[ProductPrice], [Product].[StockQuantity],  [Product].[RowId], CAST([Product].[RowId] as bigint) AS RowIdBig FROM [Product] INNER JOIN [NoSNProduct] ON [Product].[ProductId] = [NoSNProduct].[ProductId] WHERE [NoSNProduct].[ProductId] = @ProductId",
                new { ProductId = productId }).ToList();


            conn.Close();
            return productList;
        }

        public NoSNProduct InsertNoSNProduct(NoSNProduct noSNProduct)
        {
            conn.Open();

            int rowsAffected = conn.Execute(@"INSERT INTO [NoSNProduct] VALUES(@ProductId)",
                new { ProductId = noSNProduct.ProductId});
            noSNProduct.NoSNProductId = conn.Query<int>("SELECT @@IDENTITY").FirstOrDefault();
            conn.Close();

            if (rowsAffected >= 1) { return noSNProduct; }
            else { return null; }
        }

        public bool UpdateNoSNProduct(NoSNProduct noSNProduct)
        {
            ////No variables to update in NoSNProduct, might be useful for future feature
            //throw new NotImplementedException();
            //conn.Open();

            //int rowsAffected = conn.Execute("UPDATE [NoSNProduct] SET ... WHERE ProductId = @ProductId",
            //    new { ProductId = noSNProduct.ProductId });

            //conn.Close();
            //if (rowsAffected >= 1) { return true; }
            //else { return false; }
            //placeholder return - delete it later
            return false;
        }

        //Product methods

        public List<Product> GetAllProducts()
        {
            //Gets ID of all products, then gets each product by its id
            conn.Open();
            List<Product> result = new List<Product>();
            List<int> ids = conn.Query<int>("SELECT [ProductId] FROM [Product]").ToList();
            conn.Close();
            foreach (int i in ids)
            {
                result.Add(GetProductById(i));
            }

            return result;
        }

        public Product GetProductById(int productId)
        {
            conn.Open();
            Product result = conn.Query<Product>("SELECT [ProductId], [ProductName], [Barcode], [ProductPrice], [StockQuantity] ,[RowId], CAST(RowId as bigint) AS RowIdBig FROM [Product] WHERE ProductId =@ProductId", new { ProductId = productId }).SingleOrDefault();
            if (result == null) { conn.Close(); return null; }
            result.Category = conn.Query<Category>("SELECT [Category].[CategoryId], [Category].[CategoryName] FROM [Category] INNER JOIN [Product] ON [Category].[CategoryId] = [Product].[CategoryId] WHERE [Product].[ProductId]=@ProductId", new { ProductId = productId }).SingleOrDefault();
            conn.Close();

            return result;
        }

        public List<Product> GetProductByName(String productName)
        {
            conn.Open();

            List<Product> result = new List<Product>();

            List<int> idList = conn.Query<int>("SELECT [ProductId] FROM [Product] WHERE ProductName =@ProductName", new { ProductName = productName }).ToList();
            conn.Close();
            if (idList.Count == 0) { result = null; }
            else
            {
                foreach (int i in idList)
                {
                    result.Add(GetProductById(i));
                }
            }

            return result;
        }

        public List<Product> GetAllProductsFromCategory(string categoryName)
        {
            conn.Open();

            List<Product> result = conn.Query<Product>
                ("SELECT [Product].[ProductId], [Product].[ProductName], [Product].[Barcode], [Product].[ProductPrice], [Product].[StockQuantity], [Product].[RowId], CAST([Product].[RowId] as bigint) AS RowIdBig FROM [Product] INNER JOIN [Category] ON [Product].[CategoryId] = [Category].[CategoryId] WHERE CategoryName = @CategoryName", new { CategoryName = categoryName }).ToList();

            conn.Close();
            return result;
        }

        public Product InsertProduct(Product product)
        {
            conn.Open();
            int rowsAffected = 0;
            try {
                rowsAffected = conn.Execute(@"INSERT INTO [Product] VALUES(@ProductName, @Barcode, @ProductPrice, @StockQuantity, @CategoryId, null)",
              new { ProductName = product.ProductName, Barcode = product.Barcode, ProductPrice = product.ProductPrice, StockQuantity = product.StockQuantity, CategoryId = product.Category.CategoryId });
                product.ProductId = conn.Query<int>("SELECT @@IDENTITY").FirstOrDefault();
            }
            catch(NullReferenceException) { Debug.WriteLine("Product didn't have category variable."); }

            conn.Close();

            if (rowsAffected >= 1) { return product; }
            else { return null; }
        }

        public bool UpdateProduct(Product product, Int64 rowIdBig)
        {
            int rowsAffected = 0;
            using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        rowsAffected = conn.Execute("UPDATE [Product] SET ProductName = @ProductName, Barcode = @Barcode, ProductPrice = @ProductPrice, StockQuantity = @StockQuantity, CategoryId = @CategoryId WHERE ProductId = @ProductId AND (cast(@OldRowIdBig as binary(8)) = RowId)",
                            new { ProductName = product.ProductName, Barcode = product.Barcode, ProductPrice = product.ProductPrice, StockQuantity = product.StockQuantity, CategoryId = product.Category.CategoryId, ProductId = product.ProductId, OldRowIdBig = rowIdBig }, transaction);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message + " in updateProduct");
                        transaction.Rollback();

                    }
                }
            }
            if (rowsAffected >= 1) { return true; }
            else { return false; }
        }

    }
}
