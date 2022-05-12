using System.Collections.Generic;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        private List<Item> items;

        public Checkout(List<Item> items)
        {
            this.items = items;
        }
    }
}