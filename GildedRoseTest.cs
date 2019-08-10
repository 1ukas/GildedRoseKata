using NUnit.Framework;
using System.Collections.Generic;

namespace csharp {
    [TestFixture]
    public class GildedRoseTest {
        [Test]
        public void UpdateItemNameTest() {
            IList<Item> Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            // Assert that they have the same name:
            Assert.AreEqual("+5 Dexterity Vest", Items[0].Name, "False Name string, expected '+5 Dexterity Vest'");
        }

        [Test]
        public void UpdateBasicItemValuesTest() {
            IList<Item> Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            // Assert that the SellIn and Quality values were adjusted correctly:
            Assert.AreEqual(9, Items[0].SellIn, "False SellIn value, expected 9");
            Assert.AreEqual(19, Items[0].Quality, "False Quality value, expected 19");
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
        public void UpdateAgedBrieValuesTest() {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 10 } };
            GildedRose app = new GildedRose(Items);

            // Assert that the SellIn and Quality values were adjusted correctly:
            app.UpdateQuality();
            Assert.AreEqual(-1, Items[0].SellIn, "False SellIn value, expected -1");
            Assert.AreEqual(12, Items[0].Quality, "False Quality value, expected 12");

            // Assert that the Quality value was increased two folds:
            app.UpdateQuality();
            Assert.AreEqual(-2, Items[0].SellIn, "False SellIn value, expected -2");
            Assert.AreEqual(14, Items[0].Quality, "False Quality value, expected 14");
        }

        [Test]
        public void QualityFactorNotLessThanZeroTest() {
            IList<Item> Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            // Assert that the Quality value does not go lower than 0:
            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality, "False Quality value, expected 0");
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
        public void ConjuredItemsDegradeTwiceAsFastTest() {
            IList<Item> Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 2 },
                                                 new Item { Name = "Conjured Mana Cake", SellIn = 0, Quality = 4 }};
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();
            // Conjured items should decrease in quality by 2 before the sell by date:
            Assert.AreEqual(0, Items[0].Quality, "False Quality value, expected 0");
            // Conjured items should decrease in quality by 4 after the sell by date:
            Assert.AreEqual(0, Items[1].Quality, "False Quality value, expected 0");
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
