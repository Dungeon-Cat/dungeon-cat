namespace Scripts.Components
{
    public interface IEntityComponent : IComponentWithData
    {
        public string Id { get; }
    }
}