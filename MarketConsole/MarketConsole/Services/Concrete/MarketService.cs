using MarketConsole.Data.Enums;
using MarketConsole.Data.Models;
using MarketConsole.Services.Abstract;
using MarketConsole.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketConsole.Services.Concrete
{
    internal class MarketService : IMarkettable
    {
        private List<Product> products;
        private List<Sale> sales;
        private List<SaleItem> saleItems;

        public List<Product> GetProducts()
        {
            return products;
        }
        public List<Sale> GetSale()
        {
            return sales;
        }
        public MarketService()
        {
            products = new();
            sales = new();
            saleItems = new();
        }
        public void ShowProducts() { }
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
            if(category == null ) throw new ArgumentNullException("Category can't be null!");
            
            var searchCategory = products.Where(x => x.Category == category).Select(p => new Product(p.Name,p.Price,p.Category,p.Counts,p.ID)).ToList();
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

        public void ShowSale() { }
        public int  AddNewSale(int id , int count, DateTime dateTime)
        {
            var product = products.Find(x => x.ID == id);
            
            
            //if (dateTime < DateTime.Now) throw new Exception("Error!!!!");
            if (count < 0) throw new Exception("Count can't be less than 0!");

            if (product != null && product.Counts >= count)
            {
                var price = product.Price * count;
                product.Counts -= count;

                var saleItem = new SaleItem(product, count);
                var sale = new Sale(price,count,dateTime);

                saleItems.Add(saleItem);
                sales.Add(sale);

                return product.Counts;
            }

            return 0;

        }
    }
   
}
