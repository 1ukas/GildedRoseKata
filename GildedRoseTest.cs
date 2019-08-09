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
        public void QualityCantBeLowerThanZeroTest() {
            IList<Item> Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            // Assert that the Quality value does not go lower than 0:
            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality, "False Quality value, expected 0");
        }

        [Test]
        public void QualityCantBeHigherThanFiftyTest() {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 50 } };
            GildedRose app = new GildedRose(Items);

            // Assert that the Quality value does not go higher than 50:
            app.UpdateQuality();
            Assert.AreEqual(50, Items[0].Quality, "False Quality value, expected 50");
        }

        [Test]
        public void ConjuredItemsDegradeTwiceAsFastTest() {
            IList<Item> Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 20 },
                                                 new Item { Name = "Conjured Mana Cake", SellIn = 0, Quality = 20 }};
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(18, Items[0].Quality, "False Quality value, expected 18");
            Assert.AreEqual(16, Items[1].Quality, "False Quality value, expected 16");
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
