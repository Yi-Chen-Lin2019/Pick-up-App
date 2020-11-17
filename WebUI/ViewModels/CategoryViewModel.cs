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
            //TODO
            //this.id = category.id;
            //this.name = category.name;
        }

        public CategoryViewModel(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}