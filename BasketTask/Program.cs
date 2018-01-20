using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketTask
{
    class Program
    {
        static void AddItemAndLog(ShoppingCart cart, Item item, int quantity)
        {
            cart.AddItem(item, quantity);
            Console.WriteLine(quantity + " " + item.Name + " @ £" + item.Price);
        }

        static void ApplyVoucherAndLog(ShoppingCart cart, IVoucher voucher)
        {
            cart.ApplyVoucher(voucher);
            Console.WriteLine(voucher.AppliedMessage());
        }

        static void DisplayBasket(List<Item> items, List<IVoucher> vouchers)
        {
            ShoppingCart cart = new ShoppingCart();
            foreach (Item item in items)
                AddItemAndLog(cart, item, 1);
            Console.WriteLine("---------");

            foreach (IVoucher voucher in vouchers)
                ApplyVoucherAndLog(cart, voucher);
            Console.WriteLine("---------");

            Console.WriteLine("Total: £" + cart.FinalValue);

            if (cart.Messages.Count > 0)
            {
                Console.WriteLine("");
                foreach (string message in cart.Messages)
                    Console.WriteLine("Message: " + message);
            }

            Console.WriteLine("==============================");
        }

        static void Main(string[] args)
        {
            Item hat1 = new Item("Hat", 1050, Categories.Clothing);
            Item jumper1 = new Item("Jumper", 5465, Categories.Clothing);
            Item hat2 = new Item("Hat", 2500, Categories.Clothing);
            Item jumper2 = new Item("Jumper", 2600, Categories.Clothing);
            Item headLight = new Item("Head Light", 350, Categories.HeadGear);
            Item giftVoucher = new Item("£30 Gift Voucher", 3000, Categories.Voucher);

            GiftVoucher voucher1 = new GiftVoucher(500);
            OfferVoucher voucher2 = new OfferVoucher(500, 5000, Categories.HeadGear);
            OfferVoucher voucher3 = new OfferVoucher(500, 5000);

            Console.WriteLine("Basket 1");
            Console.WriteLine("==============================");
            DisplayBasket(new List<Item> { hat1, jumper1 }, new List<IVoucher> { voucher1 });

            Console.WriteLine("Basket 2");
            Console.WriteLine("==============================");
            DisplayBasket(new List<Item> { hat2, jumper2 }, new List<IVoucher> { voucher2 });

            Console.WriteLine("Basket 3");
            Console.WriteLine("==============================");
            DisplayBasket(new List<Item> { hat2, jumper2, headLight }, new List<IVoucher> { voucher2 });

            Console.WriteLine("Basket 4");
            Console.WriteLine("==============================");
            DisplayBasket(new List<Item> { hat2, jumper2 }, new List<IVoucher> { voucher1, voucher3 });

            Console.WriteLine("Basket 5");
            Console.WriteLine("==============================");
            DisplayBasket(new List<Item> { hat2, giftVoucher }, new List<IVoucher> { voucher3 });

            Console.ReadLine();
        }
    }
}
