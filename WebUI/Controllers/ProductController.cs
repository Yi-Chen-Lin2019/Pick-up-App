﻿using Microsoft.Ajax.Utilities;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebUI.ServiceLayer;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        List<ProductViewModel> products;
        List<CategoryViewModel> categories;

        public ProductController()
        {
            products = new List<ProductViewModel>();
            categories = new List<CategoryViewModel>();

            //stubs
            //Image image = new Bitmap(123, 321);
            //System.IO.MemoryStream ms = new MemoryStream();
            //image.Save(ms, ImageFormat.Jpeg);
            //byte[] byteImage = ms.ToArray();
            //string imageString = Convert.ToBase64String(byteImage);
           
            //categories.Add(new CategoryViewModel(1, "Cat 1"));
            //categories.Add(new CategoryViewModel(2, "Cat 2"));
            //categories.Add(new CategoryViewModel(3, "Cat 3"));

            //for (int i = 0; i < 21; i++)
            //{
            //    if (i % 2 == 1)
            //    {
            //        products.Add(new ProductViewModel(i, "test Name " + i.ToString(), i + 13.50, imageString, categories[i % 3]));
            //    }
            //    else
            //    {
            //        SNProductViewModel sNProduct = new SNProductViewModel(i, "test Name " + i.ToString(), i + 33.5, imageString, categories[i % 3], "serialNo" + i.ToString());
            //        products.Add(sNProduct);

            //    }
            //};

        }

        private async Task fetchItemsAsync()
        {
            LocalService service = new LocalService();
            List<Product> foundProducts = await service.GetAllProducts();

            foreach (var item in foundProducts)
            {
                products.Add(new ProductViewModel(item));
                //TODO: separate Product from SNProduct


                //TODO do the categories properly
                if (!categories.Any(cat => cat.id == item.Category.CategoryId))
                {
                   categories.Add(new CategoryViewModel(item.Category));
                }
            }

            //foreach (var item in foundCategories)
            //{
            //    categories.Add(new CategoryViewModel(item));
            //}
        }

        public async Task<ActionResult> Index(string sortOrder, int category = 0)
        {
            await fetchItemsAsync();

            List<ProductViewModel> prods = products; 
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

            return View(new ViewModels.BrowseViewModel() { categories = this.categories, products = prods });
        }

        

        public async Task<ActionResult> Search(string query)
        {
            await fetchItemsAsync();
            List<ProductViewModel> prods = products;
            prods = prods.FindAll(p => p.name.Contains(query));
            return View("Index", new ViewModels.BrowseViewModel() { categories = this.categories, products = prods });
        }

        public ActionResult Details(int id)
        {
            return View(products[id]);
        }
    }
}