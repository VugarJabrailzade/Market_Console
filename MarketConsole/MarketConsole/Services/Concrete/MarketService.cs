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
        public List<SaleItem> GetItem()
        {
            return saleItems;
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
            if (price < 0) throw new Exception("Price is negative!");
            if (counts < 0) throw new Exception("Count can't be less than 0!");
            
            var product = new Product(name, price, category, counts);

            products.Add(product);
            return product.ID;

        }

        public void DeleteProduct(int ID)
        {
            if (ID < 0) throw new Exception("ID can't be negative!");
            var existingproduct = products.FirstOrDefault(p => p.ID == ID); // return first element in product ID

            if (existingproduct == null) throw new ArgumentNullException("Not found!");

            products = products.Where(p => p.ID != ID).ToList();
        }

        public void UpdateProduct(int ID, string name, decimal price, ProductCategory category,int counts)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Name is null!");
            if (price < 0) throw new Exception("Price is negative!");
            if (counts < 0) throw new Exception("Counts can't be negative!");

            var existingproduct = products.FirstOrDefault(p => p.ID == ID);
            if (existingproduct == null) throw new Exception("Student not found!");

            existingproduct.Name = name;
            existingproduct.Price = price;
            existingproduct.Category = category;
            existingproduct.Counts = counts;
            //to change the new parameters of the existing product
        }

        public List<Product> ShowCategoryByProduct(ProductCategory category) //showing product category when writing searchin for category by user
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
            //givin minimum price and maximum price by user and shows the product according to the price range
            return products.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToList();
        }

        public void ShowSale() { }
        
        public void AddNewSale(int id , int quantity, DateTime dateTime)
        {
            var product = products.Find(x => x.ID == id);
            List<SaleItem> tempItems = new();

            if (quantity < 0) throw new Exception("Count can't be less than 0!");

            if (product != null && product.Counts >= quantity) //We call to the information contained in the product
            {
                
                var price = product.Price * quantity; //when we write count this variable calculated price according to count
                product.Counts -= quantity;
                var saleItem = new SaleItem(product, quantity);
                tempItems.Add(saleItem);
                var sale = new Sale(price, quantity, DateTime.Now);
                foreach (var res in tempItems)
                {
                    sale.AddSaleItem(res);
                    
                }
                sales.Add(sale);

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
                            int secondCount = int.Parse(Console.ReadLine());

                            var newProduct = products.Find(x => x.ID == salesID); // Finding Id when we input from user for adding new sale. 

                            var secondPrice = product.Price * secondCount;
                            newProduct.Counts -= secondCount;

                            var newSaleItem = new SaleItem(newProduct, secondCount);
                            tempItems.Add(newSaleItem);
                            sale = new Sale(secondPrice, secondCount, DateTime.Now);
                            foreach (var res in tempItems)
                            {
                                sale.AddSaleItem(res);

                            }
                            sales.Add(sale);
                            
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
                    //sales.Add(sale);

                } while (option != 2);

            }
        }
        public void RemoveSale(int saleID)
        {
            if (saleID < 0) throw new ArgumentException("ID can't be less than 0!");

            var saleFind = sales.FindIndex(x => x.ID == saleID);
            if (saleFind == -1) throw new Exception("Sale not found!");

            var removeSale = sales[saleFind];

            foreach (var res in removeSale.Items) // remove sale by given user id and we return deleting product count to back
            {
                var product = products.Find(x => x.ID == res.SalesProduct.ID);
                product.Counts += res.Quantity;
            }
            sales.RemoveAt(saleFind);

        }
        public void ReturnPurchase(int ID, int productID,int quantity) 
        {
            Sale sale = sales.Find(x=> x.ID == ID);
            if (sale == null) throw new ArgumentNullException("Sale can't be null!");

            var saleItem = sale.Items.Find(x => x.SalesProduct.ID == productID);
            if (saleItem == null) throw new ArgumentNullException("Product not found in sale!");

            if (quantity > saleItem.Quantity) throw new Exception("Quantity function invalid!");
            
            var product = products.Find(x => x.ID== productID);
            if (product == null) throw new ArgumentNullException("Product not found in sale!");

            product.Counts += quantity; // we return sale product to product list
            saleItem.Quantity -= quantity;

            sale.Price -= product.Price * quantity; // sale.Price give us last price after return sale
            sale.Quantity -= quantity;

            Console.WriteLine("Return is succesfully!");

        }
        public List<Sale> ShowSalesByDate(DateTime minDate, DateTime maxDate)
        {
            if (minDate > maxDate) throw new ArgumentException("Min date can't be more than Max date!");

            return sales.Where(x => x.DateTime >= minDate && x.DateTime <= maxDate).ToList();
        }
        public List<Sale> ShowSaleByPriceRange(int minPrice, int maxPrice)
        {
            if (minPrice < 0) throw new Exception("Minimum price can't less than 0");
            if (minPrice > maxPrice) throw new Exception("Minimum price can't be more than maximum price!");
            
            return sales.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToList();
        }
        public List<Sale> ShowSalesInGivenOneDate(DateTime dateTime) 
        {
            if (dateTime == null) throw new ArgumentNullException("Date is not found!");

            return sales.Where(x => x.DateTime.Date ==dateTime.Date).ToList(); // .Date giving us only date(MM/dd/yyyy) from Datetime(hourse not searching)
        }
        public List<Sale> ShowSalesByID(int ID)
        {
            if (ID < 0) throw new Exception("ID can't be less than 0!");
            var existingID = sales.Find(x => x.ID == ID); // finding sale ID with Find method

            if (existingID == null) throw new ArgumentNullException("Not Found!");

            return  sales.Where(p => p.ID == ID).ToList(); // show all information about sales by take sale ID
        }


    }
   
}
