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
        public void ShowProducts() { }
        public int AddProduct (string name,decimal price, ProductCategory category,int count);
        public void DeleteProduct (int ID);
        public void UpdateProduct(int ID, string name, decimal price, ProductCategory category, int counts);
        public List<Product> ShowCategoryByProduct(ProductCategory category);
        public List<Product> FindProductByName(string name);
        public List<Product> ShowProductByPriceRange(int minPrice, int maxPrice);

        public List<Sale> GetSale();
        public void ShowSales() { }

    }
}
 