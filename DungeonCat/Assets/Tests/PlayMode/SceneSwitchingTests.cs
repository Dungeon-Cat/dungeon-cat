using System.Collections;
using System.Linq;
using NUnit.Framework;
using Scripts.Components;
using Scripts.Components.CommonEntities;
using Scripts.Data;
using Scripts.Definitions.Items;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class SceneSwitchingTests
    {
        private bool setup;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            yield return SceneManager.LoadSceneAsync("Root");
            yield return SceneManager.LoadSceneAsync("Room1", LoadSceneMode.Additive);
        }

        [UnityTest]
        public IEnumerator TestItemIsStillThere()
        {
            var itemStart = UnityState.CurrentScene.AllEntities.OfType<ItemEntity>().Count();
            
            GameStateManager.CurrentState.cat.DropAllItems();

            var itemsBefore = UnityState.CurrentScene.AllEntities.OfType<ItemEntity>().Count();
            
            Assert.IsTrue(itemsBefore > itemStart);
            
            GameStateManager.SwitchScene("Testing", new Vector2(0, 0));
            yield return new WaitUntil(() => !UnityState.Instance.IsSwitchingScenes);
            
            GameStateManager.SwitchScene("Room1", new Vector2(0, 0));
            yield return new WaitUntil(() => !UnityState.Instance.IsSwitchingScenes);
            
            var itemsAfter = UnityState.CurrentScene.AllEntities.OfType<ItemEntity>().Count();
            
            Assert.AreEqual(itemsBefore, itemsAfter);
        }
        
        [UnityTest]
        public IEnumerator TestDefaultItemsStayDestroyed()
        {
            var itemStart = UnityState.CurrentScene.AllEntities.OfType<ItemEntity>().Count();
            
            foreach (var itemEntityData in GameStateManager.CurrentState.CurrentScene.entities.Values.OfType<ItemEntityData>().ToList())
            {
                Assert.IsTrue(itemEntityData.isDefaultInScene);
                GameStateManager.CurrentState.cat.TryPickupItem(itemEntityData);
            }
            
            var itemsBefore = UnityState.CurrentScene.AllEntities.OfType<ItemEntity>().Count();

            Assert.IsTrue(itemsBefore < itemStart);
            
            GameStateManager.SwitchScene("Testing", new Vector2(0, 0));
            yield return new WaitUntil(() => !UnityState.Instance.IsSwitchingScenes);
            
            GameStateManager.SwitchScene("Room1", new Vector2(0, 0));
            yield return new WaitUntil(() => !UnityState.Instance.IsSwitchingScenes);
            
            var itemsAfter = UnityState.CurrentScene.AllEntities.OfType<ItemEntity>().Count();
            
            Assert.AreEqual(itemsBefore, itemsAfter);
        }
    }
}