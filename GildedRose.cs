using System.Collections.Generic;

namespace csharp
{
    /*
        - All items have a SellIn value which denotes the number of days we have to sell the item
	    - All items have a Quality value which denotes how valuable the item is
	    - At the end of each day our system lowers both values for every item

    Pretty simple, right? Well this is where it gets interesting:

	    - Once the sell by date has passed, Quality degrades twice as fast
	    - The Quality of an item is never negative
	    - "Aged Brie" actually increases in Quality the older it gets
	    - The Quality of an item is never more than 50
	    - "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
	    - "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
	    Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
	    Quality drops to 0 after the concert

    We have recently signed a supplier of conjured items. This requires an update to our system:

	    - "Conjured" items degrade in Quality twice as fast as normal items

     */

    public class GildedRose
    {
        // Some constant strings for ease of writing:
        public const string AGED_BRIE_TAG = "Aged Brie";
        public const string BACKSTAGE_PASS_TAG = "Backstage passes to a TAFKAL80ETC concert";
        public const string SULFURAS_TAG = "Sulfuras, Hand of Ragnaros";

        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            // Loop through all the items in the system:
            foreach (Item item in Items) {
                // Check if the items sell by date has passed:
                if (item.SellIn < 0) {
                    // If passed, decrease the quality twice as fast:
                    item.Quality = item.Quality - 2 < 0 ? 0 : item.Quality - 2; // Check so the quality does not become negative
                }
                else {
                    // If the sell by date has not passed, decrease quality by one:
                    item.Quality = item.Quality - 1 < 0 ? 0 : item.Quality - 1; // Check so the quality does not become negative
                    item.SellIn -= 1; // Decrease the sell by date;
                }
            }

            //// Loop through all the items in the system:
            //for (var i = 0; i < Items.Count; i++)
            //{
            //    // If the item is not either 'Aged Brie' and a 'Backstage pass':
            //    if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
            //    {
            //        if (Items[i].Quality > 0)
            //        {
            //            if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            //            {
            //                Items[i].Quality = Items[i].Quality - 1;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (Items[i].Quality < 50)
            //        {
            //            Items[i].Quality = Items[i].Quality + 1;

            //            if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
            //            {
            //                if (Items[i].SellIn < 11)
            //                {
            //                    if (Items[i].Quality < 50)
            //                    {
            //                        Items[i].Quality = Items[i].Quality + 1;
            //                    }
            //                }

            //                if (Items[i].SellIn < 6)
            //                {
            //                    if (Items[i].Quality < 50)
            //                    {
            //                        Items[i].Quality = Items[i].Quality + 1;
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            //    {
            //        Items[i].SellIn = Items[i].SellIn - 1;
            //    }

            //    if (Items[i].SellIn < 0)
            //    {
            //        if (Items[i].Name != "Aged Brie")
            //        {
            //            if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
            //            {
            //                if (Items[i].Quality > 0)
            //                {
            //                    if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            //                    {
            //                        Items[i].Quality = Items[i].Quality - 1;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                Items[i].Quality = Items[i].Quality - Items[i].Quality;
            //            }
            //        }
            //        else
            //        {
            //            if (Items[i].Quality < 50)
            //            {
            //                Items[i].Quality = Items[i].Quality + 1;
            //            }
            //        }
            //    }
            //}
        }
    }
}
