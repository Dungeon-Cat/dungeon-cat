using Scripts.Data;
using UnityEngine;

namespace Scripts.Components
{
    public abstract class EntityComponent<T> : ComponentWithData<T>, IEntityComponent where T : EntityData
    {
        public new Collider2D collider;

        
        public string Id => data.id;
        
        protected virtual void Start()
        {
            GameStateManager.Register(data);
        }

        public override void SyncFromData()
        {
            transform.position = data.position;
        }

        public override void SyncToData()
        {
            data.position = transform.position;
            data.scene = gameObject.scene.name;
        }
    }
}