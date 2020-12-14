using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebUI.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ImageUrl { get; set; }
        public CategoryViewModel category { get; set; }
        public ProductViewModel()
        {

        }
        public ProductViewModel(Product product)
        {
            //TODO
            //
            this.ProductId = product.ProductId;
            this.ProductName = product.ProductName;
            //TODO check maybe it wouldn't break to change the viewmodel to decimal too
            this.ProductPrice = (double)product.ProductPrice;
            //TODO set image
            this.ImageUrl = product.ImageUrl;
            this.category = new CategoryViewModel(product.Category.CategoryId, product.Category.CategoryName);
        }
        public ProductViewModel(int id, string name, double price, string ImageUrl, CategoryViewModel category)
        {
            this.ProductId = id;
            this.ProductName = name;
            this.ProductPrice = price;
            this.ImageUrl = ImageUrl;
            this.category = category;
        }
    }
}