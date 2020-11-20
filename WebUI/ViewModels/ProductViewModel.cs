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
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string image { get; set; }
        public CategoryViewModel category { get; set; }
        public ProductViewModel()
        {

        }
        public ProductViewModel(Product product)
        {
            //TODO
            //
            this.id = product.ProductId;
            this.name = product.ProductName;
            //TODO check maybe it wouldn't break to change the viewmodel to decimal too
            this.price = (double)product.ProductPrice;
            //TODO set image
            this.image = "";
            this.category = new CategoryViewModel(product.Category.CategoryId, product.Category.CategoryName);
        }
        public ProductViewModel(int id, string name, double price, string image, CategoryViewModel category)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.image = image;
            this.category = category;
        }
    }
}