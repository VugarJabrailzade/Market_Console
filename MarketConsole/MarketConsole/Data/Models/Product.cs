using MarketConsole.Data.Common;
using MarketConsole.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsole.Data.Models
{
    public class Product : BaseEntity
    {
        private static int count = 0;
        public Product(string name,decimal price,ProductCategory category,int count)
        {
            Name = name;
            Price = price;
            Category = category;
            Count = count;

            ID = count;
            count++;
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; } // for saving items by category
        public int Count { get; set; }
    }

}
