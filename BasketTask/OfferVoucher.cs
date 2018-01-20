using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketTask
{
    public class OfferVoucher : GiftVoucher
    {
        private int _threshold;
        private Categories CategoryRestriction { get; }

        // Default voucherId to YYY-YYY as all offer vouchers have this id in the scenario.
        public OfferVoucher(int value, int threshold, Categories categoryRestriction = Categories.None, string voucherId = "YYY-YYY") : base(value, voucherId)
        {
            _threshold = threshold;
            CategoryRestriction = categoryRestriction;
        }

        public override bool IsValid(ShoppingCart cart)
        {
            // Doesn't make sense to create an offer voucher applied only to gift vouchers
            if (CategoryRestriction == Categories.Voucher)
                return false;

            return IsValidCategory(cart.ItemsInCart) && HasReachedThreshold(cart.ValueWithoutDiscount);
        }

        public bool IsValidCategory(ItemInCart itemInCart)
        {
            return CategoryRestriction == Categories.None ||
                itemInCart.Item.Category == CategoryRestriction;
        }

        public bool IsValidCategory(List<ItemInCart> itemsInCart)
        {
            return CategoryRestriction == Categories.None || 
                itemsInCart.Any(itemInCart => itemInCart.Item.Category == CategoryRestriction);
        }

        public bool HasReachedThreshold(int cartValue)
        {
            return cartValue > _threshold;
        }

        public string GenerateNotValidMessage(ShoppingCart cart)
        {
            if (CategoryRestriction == Categories.Voucher)
                return "This voucher is most likely an error.";

            if (!IsValidCategory(cart.ItemsInCart))
                return "There are no products in your basket applicable to voucher Voucher " + VoucherID + ".";

            if (!HasReachedThreshold(cart.ValueWithoutDiscount))
                return "You have not reached the spend threshold for voucher " + VoucherID + ". Spend another £" + MissingUntilThreshold(cart.ValueWithoutDiscount).ToString("0.00") + " to receive £" + Value + " discount from your basket total.";

            return "";
        }

        private double MissingUntilThreshold(int cartValue)
        {
            // Add another pence because the cart has to be over the threshold
            return ((_threshold - cartValue) * 0.01) + 0.01;
        }

        public override string AppliedMessage()
        {
            return "1 x £" + Value + " off" +
                ((CategoryRestriction != Categories.None) ? " " + GetCategoryName() + " in" : "") + 
                " baskets over £" + (_threshold * 0.01).ToString("0.00") + 
                " Offer Voucher " + VoucherID + " applied";
        }

        private string GetCategoryName()
        {
            switch (CategoryRestriction)
            {
                case Categories.Clothing:
                    return "Clothing";

                case Categories.HeadGear:
                    return "Head Gear";

                // Offer vouchers will never be applied to a gift voucher
                default:
                    return "";
            }
        }
    }
}
