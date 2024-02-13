using System.Linq;
using NUnit.Framework;
using Scripts.Data;
using Scripts.Definitions.Items;
namespace Tests.EditMode
{
    public class InventoryTests
    {
        [SetUp]
        public void Setup()
        {
            GameStateManager.Init();
        }

        [Test]
        public void SimpleInventoryTests()
        {
            var cat = GameStateManager.CurrentState.cat;

            var key = ItemData.Create<TutorialKey>();

            Assert.IsTrue(cat.TryPickupItem(key));
            Assert.AreEqual(1, cat.inventory.TotalCount);
            Assert.AreEqual(1, cat.inventory.UsedSlots);
            
            
            var keys = ItemData.Create<TutorialKey>(2);
            
            Assert.IsTrue(cat.TryPickupItem(keys));
            Assert.AreEqual(3, cat.inventory.TotalCount);
            Assert.AreEqual(1, cat.inventory.UsedSlots);

            var boots = ItemData.Create<Boots>();
            
            Assert.IsTrue((cat.TryPickupItem(boots)));
            Assert.AreEqual(4, cat.inventory.TotalCount);
            Assert.AreEqual(2, cat.inventory.UsedSlots);
            Assert.IsTrue(cat.tags.Contains(Boots.FlyingTag));
        }

        [Test]
        public void DropItemTest()
        {
            var cat = GameStateManager.CurrentState.cat;

            var key = ItemData.Create<TutorialKey>();
            Assert.IsTrue(cat.TryPickupItem(key));
            Assert.AreEqual(1, cat.inventory.TotalCount);

            var before = GameStateManager.CurrentState.CurrentScene.entities.Count;
            
            cat.DropAllItems();

            var after = GameStateManager.CurrentState.CurrentScene.entities.Count;
            
            Assert.AreEqual(before + 1, after);
        }
        
        [Test]
        public void DropThenPickUpTest()
        {
            var cat = GameStateManager.CurrentState.cat;

            var key = ItemData.Create<TutorialKey>();
            Assert.IsTrue(cat.TryPickupItem(key));
            Assert.AreEqual(1, cat.inventory.TotalCount);
            
            cat.DropAllItems();
            
            Assert.AreEqual(0, cat.inventory.TotalCount);

            var itemEntity = GameStateManager.CurrentState.CurrentScene.entities.Values.OfType<ItemEntityData>().First();

            Assert.IsTrue(cat.TryPickupItem(itemEntity));
            
            Assert.AreEqual(1, cat.inventory.TotalCount);
        }
    }
}