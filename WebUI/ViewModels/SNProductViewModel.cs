using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.ViewModels
{
    public class SNProductViewModel : ProductViewModel
    {
        public string SerialNumber { get; set; }

        public SNProductViewModel()
        {

        }
        public SNProductViewModel(int id, string name, double price, string image, CategoryViewModel category, string serialNumber) : base(id, name, price, image, category)
        {
            SerialNumber = serialNumber;
        }
    }
}