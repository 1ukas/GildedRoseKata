# GildedRoseKata

One thing I noticed in the [requirements](https://github.com/emilybache/GildedRose-Refactoring-Kata/blob/master/GildedRoseRequirements.txt) for this kata is that it states that "Conjured" items decrease twice as fast in their quality compared to normal items.
In the [golden test file](https://github.com/emilybache/GildedRose-Refactoring-Kata/blob/master/csharp/ThirtyDays.txt), which gets provided with the kata, the quality for Conjured items only start decreasing twice as fast __after__ their sell by date. I believe this is an error in their test file.
My implementation decreases the quality of Conjured items by 2 before the sell by date, and by 4 after the sell by date.
