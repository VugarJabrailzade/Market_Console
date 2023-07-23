﻿using MarketConsole.Data.Models;
using MarketConsole.Services.Concrete;
using MarketConsole.Services.Concrete.MenuServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketManagement.HelpMenu
{
    public class SubMenu
    {
        public static void ProductHandling()
        {
            int option;

            do
            {

                Console.WriteLine("1. Show products.");
                Console.WriteLine("2. Add new product.");
                Console.WriteLine("3. Update product.");
                Console.WriteLine("4. Remove product.");
                Console.WriteLine("5. Show category by product.");
                Console.WriteLine("6. Show product by price range.");
                Console.WriteLine("7. Find product by name.");
                Console.WriteLine("0. Back to main menu.");
                Console.WriteLine("------------------------");
                Console.WriteLine("Please, enter a valid option:");
                Console.WriteLine("------------------------");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Please, enter a valid option:");
                    Console.WriteLine("------------------------");
                }


                switch (option)
                {
                    case 1:
                        ProductsHandling.ShowProducts();
                        break;
                    case 2:
                        ProductsHandling.AddNewProduct();
                        break;
                    case 3:
                        ProductsHandling.UpdateProduct();
                        break;                      
                    case 4:
                        ProductsHandling.RemoveProduct();
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }

            } while (option != 0);

        }

        private static object GetProductHandling()
        {
            return ProductHandling;
        }

        public static void SalesHandling()
        {

            int option;

            do
            {

                Console.WriteLine("1. Show sales.");
                Console.WriteLine("2. Return purchase.");
                Console.WriteLine("3. Remove sales.");
                Console.WriteLine("4. Add new sales.");
                Console.WriteLine("5. Show sales by date.");
                Console.WriteLine("6. Show sales by amount range.");
                Console.WriteLine("7. Search by sale date.");
                Console.WriteLine("8. Show sales by ID.");
                Console.WriteLine("0. Back to main menu.");
                Console.WriteLine("------------------------");
                Console.WriteLine("Please, enter a valid option:");
                Console.WriteLine("------------------------");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Please, enter a valid option:");
                    Console.WriteLine("------------------------");
                }

                switch (option)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break; 
                    case 8:
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }


            }while(option != 0);
        }




    }
}
