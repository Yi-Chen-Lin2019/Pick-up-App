using Microsoft.Ajax.Utilities;
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
using PagedList;

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
        }

        private async Task fetchItemsAsync()
        {
            LocalService service = new LocalService();
            List<Product> foundProducts = await service.GetAllProducts();
            foundProducts = foundProducts.Where(p => p.StockQuantity > 0).ToList();
            foreach (var item in foundProducts)
            {
                products.Add(new ProductViewModel(item));

                if (!categories.Any(cat => cat.id == item.Category.CategoryId))
                {
                   categories.Add(new CategoryViewModel(item.Category));
                }
            }
        }

        public async Task<ActionResult> Index(string sortOrder, int category = 0, int page = 1)
        {
            await fetchItemsAsync();

            List<ProductViewModel> prods = products; 
            switch (sortOrder)
            {
                case "price_desc":
                    prods = prods.OrderByDescending(s => s.ProductPrice).ToList();
                    break;
                case "price_asc":
                    prods = prods.OrderBy(s => s.ProductPrice).ToList();
                    break;
                case "name_asc":
                    prods = prods.OrderByDescending(s => s.ProductName).ToList();
                    break;
                case "name_desc":
                    prods = prods.OrderBy(s => s.ProductName).ToList();
                    break;
                default:
                    break;
            }

            if (category > 0)
            {
                prods.RemoveAll(p => p.category.id != category);
            }

            //Paging
            int pageSize = 20;
            prods = prods.Skip(pageSize * (page-1)).Take(pageSize).ToList();
            IPagedList <ProductViewModel> prodVM = new StaticPagedList<ProductViewModel>(prods, page, pageSize, products.Count());
            return View(new ViewModels.BrowseViewModel() { categories = this.categories, products = prodVM });
        }

        

        public async Task<ActionResult> Search(string query)
        {
            await fetchItemsAsync();
            List<ProductViewModel> prods = products;
            prods = prods.FindAll(p => p.ProductName.ToLower().Contains(query.ToLower()));
            int pageSize = 20;
            IPagedList<ProductViewModel> prodVM = new StaticPagedList<ProductViewModel>(prods,  1, pageSize, prods.Count());
            return View("Index", new ViewModels.BrowseViewModel() { categories = this.categories, products = prodVM });
        }

        public ActionResult Details(int id)
        {
            return View(products[id]);
        }
    }
}
