using System.Collections.Generic;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;

public class GildedRoseTest {
    [Test]
    public void UpdateQuality_StandardItem_QualityDegradesByOne() {
        // Arrange
        var items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 } };
        var app = new GildedRose(items);

        // Act
        app.UpdateQuality();

        // Assert
        Assert.That(items[0].Quality, Is.EqualTo(19));
        Assert.That(items[0].SellIn, Is.EqualTo(9));
    }

    [Test]
    public void UpdateQuality_ExpiredItem_QualityDegradesTwiceAsFast() {
        // Arrange
        var items = new List<Item> { new Item { Name = "Regular Item", SellIn = 0, Quality = 20 } };
        var app = new GildedRose(items);

        // Act
        app.UpdateQuality();

        // Assert
        Assert.That(items[0].Quality, Is.EqualTo(18));
        Assert.That(items[0].SellIn, Is.EqualTo(-1));
    }

    [Test]
    public void UpdateQuality_QualityIsNeverNegative() {
        // Arrange
        var items = new List<Item> { new Item { Name = "Regular Item", SellIn = 5, Quality = 0 } };
        var app = new GildedRose(items);

        // Act
        app.UpdateQuality();

        // Assert
        Assert.That(items[0].Quality, Is.EqualTo(0));
    }

    [Test]
    public void UpdateQuality_AgedBrie_IncreasesInQuality() {
        // Arrange
        var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 5, Quality = 10 } };
        var app = new GildedRose(items);

        // Act
        app.UpdateQuality();

        // Assert
        Assert.That(items[0].Quality, Is.EqualTo(11));
    }

    [Test]
    public void UpdateQuality_QualityNeverExceedsFifty() {
        // Arrange
        var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 5, Quality = 50 } };
        var app = new GildedRose(items);

        // Act
        app.UpdateQuality();

        // Assert
        Assert.That(items[0].Quality, Is.EqualTo(50));
    }

    [Test]
    public void UpdateQuality_Sulfuras_NeverChanges() {
        // Arrange
        var items = new List<Item>
        {
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 }
        };
        var app = new GildedRose(items);

        // Act
        app.UpdateQuality();

        // Assert
        Assert.That(items[0].Quality, Is.EqualTo(80));
        Assert.That(items[0].SellIn, Is.EqualTo(0));
    }

    [TestCase(11, 20, 21)] // Normal increase
    [TestCase(10, 20, 22)] // Increases by 2 when 10 days or less
    [TestCase(5, 20, 23)]  // Increases by 3 when 5 days or less
    [TestCase(0, 20, 0)]   // Quality drops to 0 after concert
    public void UpdateQuality_BackstagePasses_QualityChangesBasedOnSellIn(int sellIn, int quality, int expected) {
        // Arrange
        var items = new List<Item>
        {
            new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = sellIn,
                Quality = quality
            }
        };
        var app = new GildedRose(items);

        // Act
        app.UpdateQuality();

        // Assert
        Assert.That(items[0].Quality, Is.EqualTo(expected));
    }

    [Test]
    public void UpdateQuality_AgedBrie_DoublesQualityIncreaseAfterSellIn() {
        // Arrange
        var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 10 } };
        var app = new GildedRose(items);

        // Act
        app.UpdateQuality();

        // Assert
        Assert.That(items[0].Quality, Is.EqualTo(12));
    }

    [Test]
    public void UpdateQuality_MultipleItems_HandlesAllItemsCorrectly() {
        // Arrange
        var items = new List<Item>
        {
            new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
            new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 }
        };
        var app = new GildedRose(items);

        // Act
        app.UpdateQuality();

        // Assert
        Assert.That(items[0].Quality, Is.EqualTo(19)); // Normal item
        Assert.That(items[1].Quality, Is.EqualTo(1));  // Aged Brie
        Assert.That(items[2].Quality, Is.EqualTo(80)); // Sulfuras
        Assert.That(items[3].Quality, Is.EqualTo(21)); // Backstage passes
    }
}