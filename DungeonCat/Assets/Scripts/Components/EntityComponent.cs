using Scripts.Data;
namespace Scripts.Components
{
    public abstract class EntityComponent<T> : ComponentWithData<T> where T : EntityData
    {
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