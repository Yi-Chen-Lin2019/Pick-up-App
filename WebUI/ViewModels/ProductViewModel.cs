using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Models;

namespace WebUI.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<Product> products { get; set; }
    }
}