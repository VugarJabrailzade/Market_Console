using MarketConsole.Data.Enums;
using MarketConsole.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsole.Services.Abstract
{
    public interface IMarkettable
    {
        public List<Product> GetProducts();
        public int AddProduct (string name,decimal price, ProductCategory category,int count);
        public void DeleteProduct (int ID);

    }
}
 