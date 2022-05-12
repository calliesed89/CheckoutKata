using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CheckoutKata.Test
{
    public class CheckoutUnitTests
    {
        private ICheckout checkout;
        [SetUp]
        public void Setup()
        {
            var items = new List<Item>
            {
                new Item{SKU = "A", Price = 50, specialPrice = new SpecialPrice(){ ItemCount = 3, ItemsTotal = 130} },
                new Item{SKU = "B", Price = 30,  specialPrice = new SpecialPrice(){ ItemCount = 2, ItemsTotal = 45} },
                new Item{SKU = "C", Price = 20},
                new Item{SKU = "D", Price = 15}
            };

            checkout = new Checkout(items);
        }

        [Test]
        public void NoItemsScanned_WhenGetTotalPrice_ReturnsZero()
        {
            Assert.AreEqual(0, checkout.GetTotalPrice());
        }

        [Test]
        public void WhenInvalidItemScanned_ThrowsException()
        {
            Assert.Throws<Exception>(() => checkout.Scan("E"));
        }

        [Test]
        public void WhenEmptyValueItemScanned_ThrowsException()
        {
            Assert.Throws<Exception>(() => checkout.Scan(""));
        }

        [Test]
        public void WhenSingleItemScanned_GetTotal_ReturnsItemValue()
        {
            checkout.Scan("A");
            Assert.AreEqual(50, checkout.GetTotalPrice());
        }

        [Test]
        public void WhenItemsWithSpecialPriceScanned_GetTotal_AppliesNewPrice()
        {
            ScanArrayHelper(new string[] { "A", "A", "A" });
            Assert.AreEqual(130, checkout.GetTotalPrice());
        }

        [Test]
        public void WhenMultipeItemsWithDiscountsScanned_GetTotal_AppliesDiscount()
        {
            ScanArrayHelper(new string[] { "A", "A", "A", "B", "B" });
            Assert.AreEqual(175, checkout.GetTotalPrice());
        }

        [Test]
        public void WhenMultipeItemsScanned_GetTotal_AppliesDiscountToOnlyValidItems()
        {
            ScanArrayHelper(new string[] { "A", "A", "A", "A", "B", "C" });
            Assert.AreEqual(230, checkout.GetTotalPrice());
        }

        private void ScanArrayHelper(string[] items)
        {
            foreach (var item in items)
            {
                checkout.Scan(item);
            }
        }
    }
}