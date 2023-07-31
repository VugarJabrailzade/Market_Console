using ConsoleTables;
using MarketConsole.Data.Common;
using MarketConsole.Data.Enums;
using MarketConsole.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsole.Services.Concrete
{
    public class MenuService : BaseEntity
    {
        private static MarketService marketable = new MarketService();
        

        public static void ShowProducts()
        {

            try
            {
                var products = marketable.GetProducts();
                if (products.Count == 0)
                {
                    Console.WriteLine("There are no products!");
                    return;
                }

                var table = new ConsoleTable("ID", "Name", "Price", "Category", "Count");

                foreach (var product in products)
                {
                    table.AddRow(product.ID, product.Name, product.Price, product.Category, product.Counts);
                }
                table.Write();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error! {ex.Message}");
            }
        }
        public static void AddNewProduct()
        {
            try
            {
                Console.WriteLine("Please add products name:");
                string productName = Console.ReadLine();

                Console.WriteLine("Please add price:");
                decimal productPrice = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Please add category of product:");
                ProductCategory productCategory = (ProductCategory)Enum.Parse(typeof(ProductCategory), Console.ReadLine(), true);

                Console.WriteLine("Please add count of product:");
                int productCount = int.Parse(Console.ReadLine());

                var newID = marketable.AddProduct(productName, productPrice, productCategory, productCount);

                Console.WriteLine($"Product with ID {newID} was created!");


            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }

        }

        public static void UpdateProduct()
        {
            try
            {
                Console.WriteLine("Please add ID for change product:");
                int numberID = int.Parse(Console.ReadLine());

                Console.WriteLine("Please add  new product name:");
                string productName = Console.ReadLine();

                Console.WriteLine("Please add new price:");
                decimal productPrice = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Please add new category of product:");
                ProductCategory productCategory = (ProductCategory)Enum.Parse(typeof(ProductCategory), Console.ReadLine(), true);

                Console.WriteLine("Please add new count of product:");
                int productCount = int.Parse(Console.ReadLine());

                marketable.UpdateProduct(numberID, productName, productPrice, productCategory, productCount);

                Console.WriteLine($"Product uptade is succesfully!");


            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }
        }
        public static void RemoveProduct()
        {
            try
            {
                Console.WriteLine("Please enter product ID for deleting product!");
                int newID = int.Parse(Console.ReadLine());

                marketable.DeleteProduct(newID); // deleting  product by Id which Id giving us by user

                Console.WriteLine("Deleting product was succesfully!");

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error! {ex.Message}");
            }
        }
        public static void ShowCategoryByProduct()
        {
            try
            {
                Console.WriteLine("You can see all product category in the below:");
                Console.WriteLine("Categories: Foods, Kitchen, Bathroom, Furniture, Garden, Decorative, Electronic, Car.");

                Console.WriteLine("\nSelect category for showing product:");
                string categoryName = Console.ReadLine();

                var res = marketable.ShowCategoryByProduct((ProductCategory)Enum.Parse(typeof(ProductCategory), categoryName, true)).ToList(); //this method calls  enums which products save inside enum categories

                var table = new ConsoleTable("ID", "Name", "Price", "Category", "Count");

                foreach (var product in res)
                {
                    
                    table.AddRow(product.ID, product.Name, product.Price,product.Category, product.Counts);
                    break;

                }
                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }
        }
        public static void ShowProductByPriceRange()
        {
            try
            {
                Console.WriteLine("Enter minimum price for searching products:");
                int minPrice = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter maximum price for searching products:");
                int maxPrice = int.Parse(Console.ReadLine());

                var priceRange = marketable.ShowProductByPriceRange(minPrice, maxPrice); // giving minimum and maximum price range for search
                if (priceRange.Count == 0)
                {
                    Console.WriteLine("No products found!");
                }
                var table = new ConsoleTable("ID", "Name", "Price", "Category", "Count");

                foreach (var product in priceRange)
                {

                    table.AddRow(product.ID, product.Name, product.Price, product.Category, product.Counts);
                    

                }
                table.Write();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }

        }
        public static void FindProductByName()
        {
            try
            {
                Console.WriteLine("Please enter product name for finding:");
                string name = Console.ReadLine();

                var searchName = marketable.FindProductByName(name);
                if (searchName.Count == 0)
                {
                    Console.WriteLine("Product not found!");
                    return;
                }
                var table = new ConsoleTable("ID", "Name", "Price", "Category", "Count");

                foreach (var product in searchName)
                {

                    table.AddRow(product.ID, product.Name, product.Price, product.Category, product.Counts);


                }
                table.Write();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");

            }

        }

        public static void ShowSales()
        {
            try
            {
                var sales = marketable.GetSale();
                
                if (sales.Count == 0)
                {
                    Console.WriteLine("There are no sales!");
                    return;
                }

                var table = new ConsoleTable("ID", "Price", "Name", "Category", "Quantity","Date");

                foreach (var sale in sales)
                {
                    foreach (var item in sale.Items)
                    {
                        table.AddRow(sale.ID, sale.Price, item.SalesProduct.Name, item.SalesProduct.Category,sale.Quantity,sale.DateTime);
                        break;
                    }
                    
                }
                table.Write();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error! {ex.Message}");
            }
        }
        
        public static void AddNewSale()
        {
            try
            {
                Console.WriteLine("Please add product ID for  sales");
                int salesID = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the counts:");
                int counts = int.Parse(Console.ReadLine());

                marketable.AddNewSale(salesID, counts, DateTime.Now);

                Console.WriteLine($"Sale was implemented!");
                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }

        }
        public static void RemoveSale()
        {
            try
            {
                Console.WriteLine("Please enter sales ID for deleting product!");
                int salesID = int.Parse(Console.ReadLine());

                marketable.RemoveSale(salesID); 

                Console.WriteLine("Deleting sales was succesfully!");

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error! {ex.Message}");
            }
        }

        public static void ReturnPurchase() 
        {
            try
            {
                
                Console.WriteLine("Please enter ID for checking:");
                int saleID = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please product ID for checking:");
                int productID = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please quantity for checking:");
                int quantity = Convert.ToInt32(Console.ReadLine());

                marketable.ReturnPurchase(saleID, productID, quantity);

               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error!{ex.Message}");
            }
        }
        public static void ShowSaleByDate()
        {
            try
            {
                Console.WriteLine("Enter minDate for search (MM/dd/yyyy):");
                DateTime minDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter maxDate for search (MM/dd/yyyy):");
                DateTime maxDate = DateTime.Parse(Console.ReadLine());


                var foundSale = marketable.ShowSalesByDate(minDate, maxDate);

                if (foundSale.Count == 0)
                {
                    Console.WriteLine("Not found!");
                }
                var table = new ConsoleTable("ID", "Price", "Date", "Quantity", "Category");

                foreach (var sale in foundSale)
                {
                    foreach (var item in sale.Items)
                    {
                        table.AddRow(sale.ID, sale.Price, sale.DateTime, sale.Quantity, item.SalesProduct.Category);
                        break;
                    }

                }
                table.Write();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error! {ex.Message}");
            }
        }
        public static void ShowSaleByPriceRange()
        {
            try
            {
                
                Console.WriteLine("Enter minimum price for searching products:");
                int minPrice = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter maximum price for searching products:");
                int maxPrice = int.Parse(Console.ReadLine());

                var salePriceRange = marketable.ShowSaleByPriceRange(minPrice, maxPrice);
                if (salePriceRange.Count == 0)
                {
                    Console.WriteLine("No sales found!");
                }

                var table = new ConsoleTable("ID", "Price", "Date", "Quantity", "Category");

                foreach (var sale in salePriceRange)
                {
                    foreach (var item in sale.Items)
                    {
                        table.AddRow(sale.ID, sale.Price, sale.DateTime, sale.Quantity, item.SalesProduct.Category);
                        break;
                    }

                }
                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }
        }
        public static void ShowSalesInGivenOneDate()
        {
            try
            {
                 
                Console.WriteLine("Add that date to see sales on given date: MM/dd/yyyy");
                DateTime dateTime = DateTime.Parse(Console.ReadLine());

                var saletime = marketable.ShowSalesInGivenOneDate(dateTime);
               
                if (saletime.Count == 0)
                {
                    Console.WriteLine("No sales found!");
                }

                var table = new ConsoleTable("ID", "Price", "Date", "Quantity", "Category");

                foreach (var sale in saletime)
                {
                    foreach (var item in sale.Items)
                    {
                        table.AddRow(sale.ID, sale.Price, sale.DateTime, sale.Quantity, item.SalesProduct.Category);
                        
                    }

                }
                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }
        }
        public static void ShowSalesByID()
        {
            try
            {
                Console.WriteLine("Please add sale ID for showing all information about ID:");
                int saleID = Convert.ToInt32(Console.ReadLine());

                var secondSaleID = marketable.ShowSalesByID(saleID);
                if (secondSaleID.Count == 0)
                {
                    Console.WriteLine("No sales found!");
                }

                var table = new ConsoleTable("ID", "Product Name", "Price", "Date", "Quantity","ItemID", "Category");

                foreach (var sale in secondSaleID)
                {
                    foreach (var item in sale.Items)
                    {
                        table.AddRow(sale.ID, item.SalesProduct.Name, sale.Price, sale.DateTime, item.Quantity,item.ID, item.SalesProduct.Category);
                        break;
                    }

                }
                table.Write();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error!{ex.Message}");
            }

        }

    }


}
