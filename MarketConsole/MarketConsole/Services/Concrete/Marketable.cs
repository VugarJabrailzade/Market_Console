using MarketConsole.Data.Enums;
using MarketConsole.Data.Models;
using MarketConsole.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsole.Services.Concrete
{
    internal class Marketable : IMarkettable
    {
        private List<Product> products;
        

        public List<Product> GetProducts()
        {
            return products;
        }
        public Marketable()
        {
            products = new();
        }
        public int AddProduct(string name, decimal price, ProductCategory category, int counts)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Name is null!");
            if (price < 0) throw new ArgumentOutOfRangeException("Price is negative!");
            if (counts < 0) throw new ArgumentOutOfRangeException("Count can't be less than 0!");

            var product = new Product(name, price, category, counts);

            products.Add(product);
            return product.ID;

        }

        public void DeleteProduct(int ID)
        {
            if (ID < 0) throw new ArgumentOutOfRangeException("ID can't be negative!");
            var existingproduct = products.FirstOrDefault(p => p.ID == ID);

            if (existingproduct == null) throw new ArgumentNullException("Not found!");

            products = products.Where(p => p.ID != ID).ToList();
        }

        public void UpdateProduct(int ID, string name, decimal price, ProductCategory category,int counts)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Name is null!");
            if (price < 0) throw new ArgumentOutOfRangeException("Price is negative!");
            if (counts < 0) throw new ArgumentNullException("Counts can't be negative!");

            var existingproduct = products.FirstOrDefault(p => p.ID == ID);
            if (existingproduct == null) throw new Exception("Student not found!");

            existingproduct.Name = name;
            existingproduct.Price = price;
            existingproduct.Category = category;
            existingproduct.Counts = counts;

        }

        public List<Product> ShowCategoryByProduct(ProductCategory category)
        {
            if(category != null ) throw new ArgumentNullException("Category can't be null!");

            var searchCategory = products.Where(x => x.Category == category).ToList();
            return searchCategory;
        }

        public List<Product> FindProductByName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Name can't be empty!");
            var searchName = products.Where(x =>x.Name == name).ToList();
            return searchName;

        }

        public List<Product> ShowProductByPriceRange(int minPrice, int maxPrice)
        {
            if ( minPrice < 0) throw new Exception("Minimum price can't less than 0");
            if (minPrice > maxPrice) throw new Exception("Minimum price can't be more than maximum price!");

            return products.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToList();
        }
    }
   
}
