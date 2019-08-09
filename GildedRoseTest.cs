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
            Assert.AreEqual("+5 Dexterity Vest", Items[0].Name);
        }

        [Test]
        public void UpdateBasicItemValuesTest() {
            IList<Item> Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            // Assert that the SellIn and Quality values were adjusted correctly:
            Assert.AreEqual(9, Items[0].SellIn);
            Assert.AreEqual(19, Items[0].Quality);
        }

        [Test]
        public void UpdateAgedBrieValuesTest() {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 10 } };
            GildedRose app = new GildedRose(Items);

            // Assert that the SellIn and Quality values were adjusted correctly:
            app.UpdateQuality();
            Assert.AreEqual(-1, Items[0].SellIn);
            Assert.AreEqual(11, Items[0].Quality);

            // Assert that the Quality value was increased two folds:
            app.UpdateQuality();
            Assert.AreEqual(-2, Items[0].SellIn);
            Assert.AreEqual(13, Items[0].Quality);
        }
    }
}
