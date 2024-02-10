using System.Linq;
using Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Components
{
    public class UnityState : MonoBehaviour
    {
        public Cat cat;
        public static UnityState Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            GameStateManager.Init(cat.data);

            // listen to events
            GameStateManager.onItemPickedUp += OnItemPickedUp;
            GameStateManager.onEntityCreated += OnEntityCreated;
        }

        public static DungeonLevel GetScene(string sceneName) =>
            SceneManager.GetSceneByName(sceneName).GetRootGameObjects().First().GetComponentInChildren<DungeonLevel>();

        private void OnEntityCreated(EntityData entity)
        {
            var scene = GetScene(entity.scene);
            switch (entity)
            {
                case ItemEntityData itemData:
                    var prefab = Resources.Load<GameObject>("Prefabs/ItemPrefab");
                    var newGameObject = Instantiate(prefab, scene.transform);
                    newGameObject.name = entity.id;
                    var newItem = newGameObject.GetComponent<ItemEntity>();
                    newItem.data = itemData;
                    newItem.SyncFromData();
                    return;
            }
        }

        private void OnItemPickedUp(CatData _, ItemData item)
        {
            Debug.Log($"Picked up {item.id}");
        }

        private void OnDestroy()
        {
            GameStateManager.onItemPickedUp -= OnItemPickedUp;
            GameStateManager.onEntityCreated -= OnEntityCreated;
        }
    }
}