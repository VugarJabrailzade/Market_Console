using ConsoleTables;
using MarketConsole.Data.Common;
using MarketConsole.Data.Enums;
using MarketConsole.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsole.Services.Concrete.MenuServices
{
    public class ProductsHandling : BaseEntity
    {
        private static Marketable marketable = new Marketable();
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
                Console.WriteLine();
            }
            
        }

        public static void UpdateProduct()
        {
            try
            {

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

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error! {ex.Message}");
            }
        }
        public static void ShowCategoryByProduct()
        {

        }
        public static void ShoProductByPriceRange()
        {

        }
        public static void FindProductByName()
        {

        }
    }
  

}
