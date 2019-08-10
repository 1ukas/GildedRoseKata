using NUnit.Framework;
using System.Collections.Generic;

namespace csharp {
    [TestFixture]
    public class GildedRoseTest {

        [Test]
        public void ValidateItemCreationTest() {
            /*
             * Check if the item is being created successfully.
             */

            Item item = new Item { Name = "Basic Item", SellIn = 10, Quality = 20 };

            Assert.AreEqual("Basic Item", item.Name);
            Assert.AreEqual(10, item.SellIn);
            Assert.AreEqual(20, item.Quality);
        }

        [Test]
        public void ValidateItemNameAfterUpdateTest() {
            /*
             * Check if the item name wasn't changed in an unwanted way after being updated.
             */

            IList<Item> Items = new List<Item> { new Item { Name = "Basic Item", SellIn = 10, Quality = 1 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.AreEqual("Basic Item", Items[0].Name, "The item name should not be changed");
        }

        [Test]
        public void QualityFactorDecreaseTest() {
            /*
             * Normal items decrease in quality by 1 when the SellIn value is >= 0,
             * - but they decrease in quality by 2 when the SellIn value is < 0,
             * - the quality should also never be below 0
             */

            IList<Item> Items = new List<Item> { new Item { Name = "Basic Item", SellIn = 10, Quality = 1 },
                                                 new Item { Name = "Basic Item", SellIn = 0, Quality = 2 },
                                                 new Item { Name = "Basic Item", SellIn = 5, Quality = -1 }};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.AreEqual(0, Items[0].Quality, "The Quality should decrease by 1 when SellIn >= 0");
            Assert.AreEqual(0, Items[1].Quality, "The Quality should decrease by 2 when SellIn < 0");
            Assert.AreEqual(0, Items[2].Quality, "The Quality should never be below 0");
        }

        [Test]
        public void QualityFactorIncreaseTest() {
            /*
             * Some items increase in quality depending on their SellIn value:
             * - Aged Brie increases by 1 when its SellIn value is >= 0,
             *   - or its quality increases by 2 when the SellIn value is < 0.
             * - Backstage passes increase in quality by 1 when their SellIn value is > 10,
             *   - or their quality increases by 2 when 10 >= SellIn < 5,
             *   - or the quality increases by 3 when 5 >= SellIn < 0,
             *   - when the SellIn value is 0 the quality is set to 0.
             */

            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 0 },
                                                 new Item { Name = "Aged Brie", SellIn = 0, Quality = 0 },
                                                 new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 20, Quality = 0},
                                                 new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 0},
                                                 new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 0},
                                                 new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.AreEqual(1, Items[0].Quality, "Aged Brie' quality should increase by 1 when SellIn >= 0");
            Assert.AreEqual(2, Items[1].Quality, "Aged Brie' quality should increase by 2 when SellIn < 0");
            Assert.AreEqual(1, Items[2].Quality, "Backstage passes' quality should increase by 1 when SellIn > 10");
            Assert.AreEqual(2, Items[3].Quality, "Backstage passes' quality should increase by 2 when 10 >= SellIn < 5");
            Assert.AreEqual(3, Items[4].Quality, "Backstage passes' quality should increase by 3 when 5 >= SellIn < 0");
            Assert.AreEqual(0, Items[5].Quality, "Backstage passes' quality should be 0 when SellIn < 0");
        }

        [Test]
        public void QualityFactorNotHigherThanFiftyTest() {
            /* 
             * Test so the Quality value does not increase over 50 for items which increase it.
             */

            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 50 },
                                                 new Item { Name = "Aged Brie", SellIn = 0, Quality = 49 },
                                                 new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 20, Quality = 50},
                                                 new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 49},
                                                 new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 48}};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.AreEqual(50, Items[0].Quality, "Aged Brie' quality should not increase over 50");
            Assert.AreEqual(50, Items[1].Quality, "Aged Brie' quality should not increase over 50");
            Assert.AreEqual(50, Items[2].Quality, "Backstage passes' quality should not increase over 50");
            Assert.AreEqual(50, Items[3].Quality, "Backstage passes' quality should not increase over 50");
            Assert.AreEqual(50, Items[4].Quality, "Backstage passes' quality should not increase over 50");
        }

        [Test]
        public void SellInFactorDecreaseTest() {
            /*
             * At the end of the day the SellIn value is decreased by 1 for every item
             */

            IList<Item> Items = new List<Item> { new Item { Name = "Basic Item", SellIn = 1, Quality = 20 },
                                                 new Item { Name = "Basic Item", SellIn = 0, Quality = 20 },
                                                 new Item { Name = "Basic Item", SellIn = int.MinValue, Quality = 20 }};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.AreEqual(0, Items[0].SellIn, "The SellIn value should decrease by 1 for every item");
            Assert.AreEqual(-1, Items[1].SellIn, "The SellIn value should decrease by 1 even when at 0");
            Assert.AreEqual(int.MinValue, Items[2].SellIn, "The SellIn value should not be lower than the minimum integer value");
        }

        [Test]
        public void SulfurasSpecialTests() {
            /*
             * Because Sulfuras is a special item it acts differently:
             * - Its quality is always 80,
             * - We have to keep its SellIn value the same to avoid selling it.
             */

            IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 1, Quality = 80 },
                                                 new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = int.MaxValue, Quality = 50 }};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.AreEqual(1, Items[0].SellIn, "Sulfuras' SellIn value should not change");
            Assert.AreEqual(int.MaxValue, Items[1].SellIn, "The SellIn value should not exceed the maximum integer value");
            Assert.AreEqual(80, Items[1].Quality, "The quality of Sulfuras should always be 80");
        }

        [Test]
        public void ConjuredItemsDegradeTwiceAsFastTest() {
            /*
             * Conjured items are special because they decrease in quality twice as fast as other items:
             * - Their quality decreases by 2 when SellIn is >= 0,
             *  - or their quality decreases by 4 when SellIn is < 0
             */

            IList<Item> Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 2 },
                                                 new Item { Name = "Conjured Mana Cake", SellIn = 0, Quality = 4 }};
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality, "Conjured item quality should decrease by 2 when SellIn >= 0");
            Assert.AreEqual(0, Items[1].Quality, "Conjured item quality should decrease by 4 when SellIn < 0");
        }

        //[Test]
        //public void ItemCantBeNullTest() {
        //    IList<Item> Items = new List<Item> { new Item {} };
        //    GildedRose app = new GildedRose(Items);

        //    // Assert that the UpdateQuality function throws a NullReferenceException when given a null item:
        //    Assert.Throws<System.NullReferenceException>(() => app.UpdateQuality());
        //}
    }
}
