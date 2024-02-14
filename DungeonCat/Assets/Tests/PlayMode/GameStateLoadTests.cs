using System.Collections;
using NUnit.Framework;
using Scripts.Components;
using Scripts.Data;
using Scripts.Definitions.Items;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class GameStateLoadTests
    {
        private bool setup;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            if (!setup)
            {
                yield return SceneManager.LoadSceneAsync("Root");
                setup = true;
            }
        }

        [Test]
        public void LoadsGameState()
        {
            Assert.IsNotNull(GameStateManager.CurrentState);
            Assert.IsNotNull(GameStateManager.CurrentState.cat);
            Assert.IsNotNull(UnityState.Instance.cat);

            Assert.AreEqual(UnityState.Instance.cat.data, GameStateManager.CurrentState.cat);
        }

        [Test]
        public void DefaultInventory()
        {
            var item = GameStateManager.CurrentState.cat.inventory.items[0];
            Assert.IsNotNull(item);
            Assert.AreEqual(nameof(RedHerring), item.id);
        }
    }
}