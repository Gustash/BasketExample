using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketTask
{
    public class Item
    {
        public string Name { get; }
        public Categories Category { get; }
        public int PriceInPence { get; }
        public string Price => (PriceInPence * 0.01).ToString("0.00");

        public Item(string name, int price, Categories category)
        {
            Name = name;
            PriceInPence = price;
            Category = category;
        }
    }
}
