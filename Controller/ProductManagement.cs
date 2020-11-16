using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class ProductManagement
    {

        public IEnumerable<Product> GetAll()
        {
            IProductRepository pRepo = new ProductRepository();
            return  pRepo.GetAllProducts();
            
        }
        
 
    }
}
