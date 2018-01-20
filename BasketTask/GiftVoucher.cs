using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketTask
{
    public class GiftVoucher : IVoucher
    {
        protected int _value;

        public string Value => (_value * 0.01).ToString("0.00");
        public int ValueInPence => _value;
        public string VoucherID { get; }

        // Default voucherId to XXX-XXX as all gift vouchers have this id in the scenario.
        public GiftVoucher(int value, string voucherId = "XXX-XXX")
        {
            _value = value;
            VoucherID = voucherId;
        }

        // Gift vouchers are always valid
        public virtual bool IsValid(ShoppingCart cart)
        {
            return true;
        }

        public virtual string AppliedMessage()
        {
            return "1 x £" + Value + " Gift Voucher " + VoucherID + " applied";
        }
    }
}
