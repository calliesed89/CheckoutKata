using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        private List<Item> items;

        public Checkout(List<Item> items)
        {
            this.items = items;
        }

        public int GetTotalPrice()
        {
            return 0;
        }

        public void Scan(string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                throw (new Exception("Scan:: itemSKU is null"));
            }

            if (items.Where(x => x.SKU.Equals(items)).Count() == 0)
            {
                throw (new Exception("Scan:: itemSKU not found"));
            }
        }
    }
}