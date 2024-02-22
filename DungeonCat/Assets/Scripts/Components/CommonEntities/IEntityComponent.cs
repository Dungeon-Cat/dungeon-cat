namespace Scripts.Components.CommonEntities
{
    public interface IEntityComponent : IComponentWithData
    {
        public string Id { get; }
    }
}