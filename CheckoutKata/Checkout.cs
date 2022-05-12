using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        private List<Item> items;
        private List<string> scannedItems;

        public Checkout(List<Item> items)
        {
            this.items = items;
            scannedItems = new List<string>();
        }

        public int GetTotalPrice()
        {
            var total = scannedItems.Sum(x => Price(x));
            return total - GetTotalSpecialPriceDiscount();
        }

        public void Scan(string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                throw (new Exception("Scan:: itemSKU is null"));
            }

            if (items.Where(x => x.SKU.Equals(item)).Count() == 0)
            {
                throw (new Exception("Scan:: itemSKU not found"));
            }

            scannedItems.Add(item);
        }

        private int GetTotalSpecialPriceDiscount()
        {
            var totalDiscount = 0;
            var groupedItems = scannedItems
                .GroupBy(u => u).Select(grp => new
                {
                    item = grp.Key,
                    total = grp.Count(),
                    discount = GetSpecialPriceDiscount(grp.Key, grp.Count())

                }).ToList();

            groupedItems.ForEach(x => totalDiscount += x.discount);

            return totalDiscount;
        }

        private int GetSpecialPriceDiscount(string item, int itemCount)
        {
            var discountedItem = items.First(x => x.SKU.Equals(item));

            if (discountedItem.specialPrice is null)
                return 0;

            var quantity = discountedItem.specialPrice.ItemCount;

            var discountedPrice = discountedItem.specialPrice.ItemsTotal;

            var difference = (discountedItem.Price * quantity) - discountedPrice;

            return (itemCount / quantity) * difference;
        }

        private int Price(string sku)
        {
            var item = items.First(x => x.SKU.Equals(sku));
            if (item == null)
            {
                throw (new Exception("Price:: item not found"));
            }
            return item.Price;
        }
    }
}