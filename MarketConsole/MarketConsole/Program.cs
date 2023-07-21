using MarketConsole.HelpMenu;

namespace MarketConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int option;

            do
            {
                Console.WriteLine("1. Product Handling:");
                Console.WriteLine("2. Sales Handling");
                Console.WriteLine("3. Exit.");
                Console.WriteLine("----------------");
                Console.WriteLine("Enter an option please:");
                Console.WriteLine("----------------");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid option!");
                    Console.WriteLine("Enter an option please:");
                    Console.WriteLine("----------------");
                }

                switch (option)
                {
                    case 1:
                        SubMenu.ProductHandling();
                        break;
                    case 2:
                        SubMenu.SalesHandling();
                        break;
                    case 0:
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }

            } while (option!=0);

            
        }
    }
}