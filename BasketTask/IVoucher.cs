using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketTask
{
    public interface IVoucher
    {
        // All values are calculated in pence
        string Value { get; }
        int ValueInPence { get; }

        bool IsValid(ShoppingCart cart);

        string AppliedMessage();
    }
}
