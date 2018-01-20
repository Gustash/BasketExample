using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketTask
{
    public struct ItemInCart
    {
        public Item Item { get; }
        public int Quantity { get; }
        public int ValueInPence => Item.PriceInPence * Quantity;

        public ItemInCart(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}
