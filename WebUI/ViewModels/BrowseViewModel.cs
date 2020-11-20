using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.ViewModels
{
    public class BrowseViewModel
    {
        public IEnumerable<CategoryViewModel> categories { get; set; }
        public IEnumerable<ProductViewModel> products { get; set; }
    }
}