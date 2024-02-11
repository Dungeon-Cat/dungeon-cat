using System.Collections.Generic;
using System.Linq;
using Scripts.Data;
using UnityEngine;

namespace Scripts.Components
{
    /// <summary>
    /// Script attached to the root object of each scene that's a Dungeon Level
    /// </summary>
    public class DungeonLevel : ComponentWithData<SceneData>
    {
        public Dictionary<string, GameObject> entities = new();

        private void Awake()
        {
            GameStateManager.CurrentState.scenes.TryAdd(gameObject.scene.name, new SceneData());
        }

        private void Start()
        {
            entities = gameObject.scene
                .GetRootGameObjects()
                .SelectMany(o => o.GetComponentsInChildren<IEntityComponent>())
                .ToDictionary(e => e.Id, e => e.GameObject);
        }

        /*public ComponentWithData<EntityData>[] defaultEntities;

        private void Start()
        {
            defaultEntities = gameObject.scene.GetRootGameObjects()
                .SelectMany(o => o.transform.GetComponentsInChildren<ComponentWithData<EntityData>>())
                .ToArray();
        }

        public override void SyncToData()
        {
            data.entities = gameObject.scene.GetRootGameObjects()
                .SelectMany(o => o.transform.GetComponentsInChildren<ComponentWithData<EntityData>>())
                .Select(e => e.data)
                .ToDictionary(e => e.id);
        }*/
    }
}