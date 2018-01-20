using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketTask
{
    public class ShoppingCart
    {
        public List<ItemInCart> ItemsInCart { get; }
        private OfferVoucher _appliedOfferVoucher;
        private List<GiftVoucher> _appliedGiftVouchers;
        public List<string> Messages { get; }

        private List<ItemInCart> NonVoucherItems => ItemsInCart
            .FindAll(itemInCart => itemInCart.Item.Category != Categories.Voucher);

        private List<ItemInCart> VoucherItems => ItemsInCart
            .FindAll(itemInCart => itemInCart.Item.Category == Categories.Voucher);

        // Exclude all vouchers from total
        public int ValueWithoutDiscount => NonVoucherItems
                    .Sum(itemInCart => itemInCart.ValueInPence);

        private int GiftDiscount => _appliedGiftVouchers.Sum(voucher => voucher.ValueInPence);
        private int OfferDiscount
        {
            get
            {
                if (_appliedOfferVoucher == null)
                    return 0;

                int applicableItemsValue = NonVoucherItems
                        .FindAll(itemInCart => _appliedOfferVoucher.IsValidCategory(itemInCart))
                        .Sum(itemInCart => itemInCart.ValueInPence);
                if (applicableItemsValue <= _appliedOfferVoucher.ValueInPence)
                    return applicableItemsValue;
                else
                    return _appliedOfferVoucher.ValueInPence;
            }
        }
        private int TotalDiscount => GiftDiscount + OfferDiscount;
        private int FinalValueInPence
        {
            get
            {
                int voucherItemsValue = VoucherItems.Sum(itemInCart => itemInCart.ValueInPence);
                return Math.Max(0, ValueWithoutDiscount - TotalDiscount) + voucherItemsValue;
            }
        }

        public string FinalValue => (FinalValueInPence * 0.01).ToString("0.00");

        public ShoppingCart()
        {
            ItemsInCart = new List<ItemInCart>();
            _appliedGiftVouchers = new List<GiftVoucher>();
            _appliedOfferVoucher = null;
            Messages = new List<string>();
        }

        public void AddItem(Item item, int quantity)
        {
            ItemsInCart.Add(new ItemInCart(item, quantity));
        }

        private void AddMessage(string message)
        {
            Messages.Add(message);
        }

        public void ApplyVoucher(IVoucher voucher)
        {
            if (voucher.IsValid(this))
            {
                if (voucher.GetType() == typeof(GiftVoucher))
                    _appliedGiftVouchers.Add((GiftVoucher)voucher);
                else if (_appliedOfferVoucher == null)
                    _appliedOfferVoucher = (OfferVoucher)voucher;
                else
                    AddMessage("One offer voucher was already applied.");
            }
            else
            {
                // No need to check if the voucher is an offer voucher as gift vouchers are always valid.
                OfferVoucher offerVoucher = (OfferVoucher)voucher;
                string message = offerVoucher.GenerateNotValidMessage(this);
                AddMessage(message);
            }
        }
    }
}
