using System.Collections.Generic;

namespace csharp {
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

    public class GildedRose {
        // Some constant strings for ease of writing:
        public const string AGED_BRIE_TAG = "Aged Brie";
        public const string BACKSTAGE_PASS_TAG = "Backstage passes to a TAFKAL80ETC concert";
        public const string SULFURAS_TAG = "Sulfuras, Hand of Ragnaros";
        public const string CONJURED_TAG = "Conjured";

        IList<Item> Items;
        public GildedRose(IList<Item> Items) {
            this.Items = Items;
        }

        public void UpdateQuality() {
            // Loop through all the items in the system:
            for (int i = 0; i < Items.Count; i++) {
                Items[i].SellIn--; // Decrease sell by date
                ParseItem(Items[i]);
            }
        }

        public void ParseItem(Item item) {
            // Sulfuras should be never altered or sold:
            if (item.Name == SULFURAS_TAG) {
                item.SellIn++;
                return;
            }

            // Check if the items sell by date has passed:
            if (item.SellIn < 0) {
                if (item.Name == AGED_BRIE_TAG) {
                    // Aged Brie increases in quality the older it gets:
                    item.Quality = item.Quality + 2 > 50 ? 50 : item.Quality + 2; // Quality can never be above 50
                    return;
                }

                // Backstage passes have 0 quality after the sell by date:
                if (item.Name == BACKSTAGE_PASS_TAG) {
                    item.Quality = 0;
                    return;
                }

                // Conjured items degrade twice as fast:
                if (item.Name.Contains(CONJURED_TAG)) {
                    item.Quality = item.Quality - 4 < 0 ? 0 : item.Quality - 4; // Check so the quality does not become negative
                    return;
                }

                // For basic items decrease the quality twice as fast:
                item.Quality = item.Quality - 2 < 0 ? 0 : item.Quality - 2; // Check so the quality does not become negative
            }
            else {
                if (item.Name == BACKSTAGE_PASS_TAG) {
                    if (item.SellIn <= 10 && item.SellIn > 5) {
                        item.Quality = item.Quality + 2 > 50 ? 50 : item.Quality + 2; // Quality can never be above 50
                    }
                    else if (item.SellIn <= 5 && item.SellIn > 0) {
                        item.Quality = item.Quality + 3 > 50 ? 50 : item.Quality + 3; // Quality can never be above 50
                    }
                    else {
                        item.Quality = item.Quality + 1 > 50 ? 50 : item.Quality + 1; // Quality can never be above 50
                    }
                    return;
                }

                if (item.Name == AGED_BRIE_TAG) {
                    // Aged Brie increases in quality the older it gets:
                    item.Quality = item.Quality + 1 > 50 ? 50 : item.Quality + 1; // Quality can never be above 50
                    return;
                }

                // Conjured items degrade twice as fast:
                if (item.Name.Contains(CONJURED_TAG)) {
                    item.Quality = item.Quality - 2 < 0 ? 0 : item.Quality - 2; // Check so the quality does not become negative
                    return;
                }

                // For basic items, if the sell by date has not been reached - decrease quality by one:
                item.Quality = item.Quality - 1 < 0 ? 0 : item.Quality - 1; // Check so the quality does not become negative
            }
        }
    }
}
