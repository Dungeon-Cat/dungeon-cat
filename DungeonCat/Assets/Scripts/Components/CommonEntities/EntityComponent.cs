using Scripts.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Components.CommonEntities
{
    public abstract class EntityComponent<T> : ComponentWithData<T>, IEntityComponent where T : EntityData
    {
        [FormerlySerializedAs("collider")]
        public Collider2D collider2d;
        
        public string Id => data.id;
        
        protected virtual void Start()
        {
            GameStateManager.Register(data);
        }

        protected override void OnValidateInEditor()
        {
            base.OnValidateInEditor();
            data.isDefaultInScene = true;
        }

        public override void SyncFromData()
        {
            transform.position = data.position;
            if (data.destroyed)
            {
                UnityState.OnEntityDestroyed(data);
            }
        }

        public override void SyncToData()
        {
            data.position = transform.position;
            data.scene = gameObject.scene.name;
        }
    }
}