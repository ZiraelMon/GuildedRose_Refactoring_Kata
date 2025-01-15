using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose {
    private const int MaxQuality = 50;
    private const int MinQuality = 0;
    private const string AgedBrie = "Aged Brie";
    private const string Backstage = "Backstage passes to a TAFKAL80ETC concert";
    private const string Sulfuras = "Sulfuras, Hand of Ragnaros";

    private readonly IList<Item> _items;

    public GildedRose(IList<Item> items) {
        _items = items;
    }

    public void UpdateQuality() {
        foreach (var item in _items) {
            UpdateSingleItem(item);
        }
    }

    private void UpdateSingleItem(Item item) {
        if (item.Name == Sulfuras) {
            return; // Sulfuras never changes
        }

        UpdateSellIn(item);
        UpdateItemQuality(item);
    }

    private void UpdateSellIn(Item item) {
        item.SellIn--;
    }

    private void UpdateItemQuality(Item item) {
        switch (item.Name) {
            case AgedBrie:
                UpdateAgedBrie(item);
                break;
            case Backstage:
                UpdateBackstagePasses(item);
                break;
            default:
                UpdateStandardItem(item);
                break;
        }

        EnsureQualityInRange(item);
    }

    private void UpdateAgedBrie(Item item) {
        item.Quality++;

        if (item.SellIn < 0) {
            item.Quality++;
        }
    }

    private void UpdateBackstagePasses(Item item) {
        if (item.SellIn < 0) {
            item.Quality = 0;
            return;
        }

        item.Quality++;

        if (item.SellIn < 10) {
            item.Quality++;
        }

        if (item.SellIn < 5) {
            item.Quality++;
        }
    }

    private void UpdateStandardItem(Item item) {
        item.Quality--;

        if (item.SellIn < 0) {
            item.Quality--;
        }
    }

    private void EnsureQualityInRange(Item item) {
        if (item.Name == Sulfuras) {
            return;
        }

        if (item.Quality > MaxQuality) {
            item.Quality = MaxQuality;
        }

        if (item.Quality < MinQuality) {
            item.Quality = MinQuality;
        }
    }
}