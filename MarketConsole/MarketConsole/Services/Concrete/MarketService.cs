using MarketConsole.Data.Enums;
using MarketConsole.Data.Models;
using MarketConsole.Services.Abstract;
using MarketConsole.Services.Concrete;
using MarketManagement.HelpMenu;
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
        public void AddNewSale(int id , int counts, DateTime dateTime)
        {
            var product = products.Find(x => x.ID == id);
            List<SaleItem> tempItems = new();

            if (counts < 0) throw new Exception("Count can't be less than 0!");

            if (product != null && product.Counts >= counts) //We call to the information contained in the product
            {
                
                var price = product.Price * counts; //when we write count this variable calculated price according to count
                product.Counts -= counts;
                var saleItem = new SaleItem(product, counts);
                tempItems.Add(saleItem);
                
                var sale = new Sale(price, counts, DateTime.Now);
                int option;
                do
                {
                    Console.WriteLine("Do u want to add one more sale item?");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No");

                    while (!int.TryParse(Console.ReadLine(), out option))
                    {
                        Console.WriteLine("Invalid option!");
                        Console.WriteLine("Enter option again:");
                    }
                    switch (option)
                    {
                        case 1:

                            Console.WriteLine("Please add product ID for  sales");
                            int salesID = int.Parse(Console.ReadLine());

                            Console.WriteLine("Enter the counts:");
                            int countSale = int.Parse(Console.ReadLine());

                            var newProduct = products.Find(x => x.ID == salesID);

                            var secondPrice = product.Price * countSale;
                            newProduct.Counts -= countSale;
                           var neSaleItem = new SaleItem(newProduct, countSale);
                            tempItems.Add(neSaleItem);
                            sale = new Sale(secondPrice, countSale, DateTime.Now);
                            break;
                        case 2:
                            return;
                        default:
                            Console.WriteLine("No such option!");
                            break;
                    }
                    foreach (var res in tempItems)
                    {
                        sale.AddSaleItem(res);
                    }
                    sales.Add(sale);

                } while (option != 2);

            }
        }


        
    }
   
}
