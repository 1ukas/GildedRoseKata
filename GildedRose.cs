using System.Collections.Generic;

namespace csharp {
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
                // Decrease the sell by value and check that it's not lower than minimum int:
                Items[i].SellIn = Items[i].SellIn == int.MinValue ? int.MinValue : Items[i].SellIn - 1;
                ParseItem(Items[i]);
            }
        }

        private void ParseItem(Item item) {
            // Sulfuras is special and so it cannot be sold:
            if (item.Name == SULFURAS_TAG) {
                // Increase the sell by value to avoid selling Sulfuras, and check if the value is not higher than max int:
                item.SellIn = item.SellIn == int.MaxValue ? int.MaxValue : item.SellIn + 1;
                item.Quality = 80; // Sulfuras' quality should always be 80
                return;
            }

            // Check if the items sell by date has passed:
            if (item.SellIn < 0) {
                // If the item is an Aged Brie:
                if (item.Name == AGED_BRIE_TAG) {
                    // Aged Brie increases in quality by 2 if its past the sell by date:
                    item.Quality = item.Quality + 2 > 50 ? 50 : item.Quality + 2; // Quality can never be above 50
                    return;
                }

                // Backstage passes have 0 quality after the sell by date:
                if (item.Name == BACKSTAGE_PASS_TAG) {
                    item.Quality = 0;
                    return;
                }

                // If the item is Conjured:
                if (item.Name.Contains(CONJURED_TAG)) {
                    // Decrease the quality by 4 because Conjured items decrease in quality twice as fast:
                    item.Quality = item.Quality - 4 < 0 ? 0 : item.Quality - 4; // Check so the quality does not become negative
                    return;
                }

                // For basic items decrease the quality by 2:
                item.Quality = item.Quality - 2 < 0 ? 0 : item.Quality - 2; // Check so the quality does not become negative
            } // If the sell by date has not passed:
            else {
                // If the item is a Backstage pass:
                if (item.Name == BACKSTAGE_PASS_TAG) {
                    // Backstage passes are special because they increase in quality over time:
                    // If the sell by date is between 10 and 5:
                    if (item.SellIn <= 10 && item.SellIn > 5) {
                        // Increase the quality by 2:
                        item.Quality = item.Quality + 2 > 50 ? 50 : item.Quality + 2; // Quality can never be above 50
                    } // If the sell by date is between 5 and 0:
                    else if (item.SellIn <= 5 && item.SellIn > 0) {
                        // Increase the quality by 3:
                        item.Quality = item.Quality + 3 > 50 ? 50 : item.Quality + 3; // Quality can never be above 50
                    } // Else increase the quality by 1:
                    else {
                        item.Quality = item.Quality + 1 > 50 ? 50 : item.Quality + 1; // Quality can never be above 50
                    }
                    return;
                }

                // If the item is Aged Brie:
                if (item.Name == AGED_BRIE_TAG) {
                    // Aged Brie increases in quality the older it gets, so increase it by 1:
                    item.Quality = item.Quality + 1 > 50 ? 50 : item.Quality + 1; // Quality can never be above 50
                    return;
                }

                // If the item is Conjured:
                if (item.Name.Contains(CONJURED_TAG)) {
                    // Decrease the quality by 2 because Conjured items decrease in quality twice as fast:
                    item.Quality = item.Quality - 2 < 0 ? 0 : item.Quality - 2; // Check so the quality does not become negative
                    return;
                }

                // For basic items, if the sell by date has not been reached - decrease quality by 1:
                item.Quality = item.Quality - 1 < 0 ? 0 : item.Quality - 1; // Check so the quality does not become negative
            }
        }
    }
}
