using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.ViewModels
{
    public class BrowseViewModel
    {
        public IEnumerable<CategoryViewModel> categories { get; set; }
        public IPagedList<ProductViewModel> products { get; set; }
    }
}