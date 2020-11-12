using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        List<Product> products;
        List<Category> categories;

        public ProductController()
        {
            products = new List<Product>();
            categories = new List<Category>();

            //stubs
            Image image = new Bitmap(123, 321);
            System.IO.MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            string imageString = Convert.ToBase64String(byteImage);
           
            categories.Add(new Category(1, "Cat 1"));
            categories.Add(new Category(2, "Cat 2"));
            categories.Add(new Category(3, "Cat 3"));

            for (int i = 0; i < 21; i++)
            {
                products.Add(new Product(i ,"test Name " + i.ToString(), (i + 1) * 7, imageString, categories[i % 3]));
            };

        }

        public ActionResult Index(string sortOrder, int category = 0)
        {
            List<Product> prods = products; 
            switch (sortOrder)
            {
                case "price_desc":
                    prods = prods.OrderByDescending(s => s.price).ToList();
                    break;
                case "price_asc":
                    prods = prods.OrderBy(s => s.price).ToList();
                    break;
                case "name_asc":
                    prods = prods.OrderByDescending(s => s.name).ToList();
                    break;
                case "name_desc":
                    prods = prods.OrderBy(s => s.name).ToList();
                    break;
                default:
                    break;
            }

            if (category > 0)
            {
                prods.RemoveAll(p => p.category.id != category);
            }

            return View(new ProductViewModel() { categories = this.categories, products = prods });
        }

        public ActionResult Search(string query)
        {
            List<Product> prods = products;
            prods = prods.FindAll(p => p.name.Contains(query));
            return View("Index", new ProductViewModel() { categories = this.categories, products = prods });
        }

        public ActionResult Details(int id)
        {
            return View(products[id]);
        }
    }
}
