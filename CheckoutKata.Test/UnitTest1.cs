using NUnit.Framework;
using System.Collections.Generic;

namespace CheckoutKata.Test
{
    public class Tests
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
        public void Test1()
        {
            Assert.Pass();
        }
    }
}