using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void CheckUpdatedItemName()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            // Assert that they have the same name:
            Assert.AreEqual("+5 Dexterity Vest", Items[0].Name);
        }

        [Test]
        public void CheckUpdatedItemValues() {
            IList<Item> Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            // Assert that the SellIn and Quality values were adjusted correctly:
            Assert.AreEqual(9, Items[0].SellIn);
            Assert.AreEqual(19, Items[0].Quality);
        }
    }
}
