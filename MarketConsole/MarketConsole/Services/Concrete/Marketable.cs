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

    }
   
}
