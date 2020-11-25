using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.ViewModels
{
    public class CategoryViewModel
    {
        public int id { get; set; }
        public string name { get; set; }

        public CategoryViewModel(Category category)
        {
            
            this.id = category.CategoryId;
            this.name = category.CategoryName;
        }

        public CategoryViewModel(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}