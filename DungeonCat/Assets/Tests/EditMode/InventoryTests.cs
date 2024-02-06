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

            Assert.IsTrue(cat.inventory.TryAdd(key));
            Assert.AreEqual(1, cat.inventory.TotalCount);
            Assert.AreEqual(1, cat.inventory.UsedSlots);
            
            
            var keys = ItemData.Create<TutorialKey>(2);
            
            Assert.IsTrue(cat.inventory.TryAdd(keys));
            Assert.AreEqual(3, cat.inventory.TotalCount);
            Assert.AreEqual(1, cat.inventory.UsedSlots);
        }
    }
}