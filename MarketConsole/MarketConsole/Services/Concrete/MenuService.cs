﻿using ConsoleTables;
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
        private static object table;

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

                foreach (var item in res)
                {
                    Console.WriteLine($"ID: {item.ID} | Name: {item.Name} | Price: {item.Price} | Category: {item.Category} | Count: {item.Counts}");
                }
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

                var priceRange = marketable.ShowProductByPriceRange(minPrice, maxPrice);
                if (priceRange.Count == 0)
                {
                    Console.WriteLine("No products found!");
                }

                foreach (var price in priceRange)
                {
                    Console.WriteLine($"ID: {price.ID} | Name: {price.Name} | Price: {price.Price} | Category: {price.Category} | Count: {price.Counts}");
                }


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

                foreach (var item in searchName)
                {
                    Console.WriteLine($"ID: {item.ID} | Name: {item.Name} | Price: {item.Price} | Category: {item.Category} | Count: {item.Counts}");
                }

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

                var table = new ConsoleTable("ID", "Price", "Category", "Count");

                foreach (var sale in sales)
                {
                    table.AddRow(sale.ID, sale.Price, sale.DateTime, sale.Items);
                }
                table.Write();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error! {ex.Message}");
            }
        }
        public static void AddNewSales()
        {
            try
            {
                Console.WriteLine("Please add product ID for  sales");
                int salesID = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the counts:");
                int counts = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter date (dd/MM/yyyy):");
                DateTime dateTime = DateTime.Parse(Console.ReadLine());

                marketable.AddNewSale(salesID, counts, dateTime);

                




            }
            catch (Exception ex)
            {

            }

        }
    }


}