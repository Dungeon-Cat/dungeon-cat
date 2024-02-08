using NUnit.Framework;
using Scripts.Definitions;

namespace Tests.EditMode
{
    public class ItemRegistryTests
    {
        [SetUp]
        public void Setup()
        {
            ItemRegistry.Initialize();
        }

        [Test]
        public void AnyItemsRegistered()
        {
            Assert.IsTrue(ItemRegistry.Items.Count > 0);
        }
    }
}