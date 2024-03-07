namespace Scripts.Components.CommonEntities
{
    /// <summary>
    /// Interface used to query all entity components, since that operation would not work with generics
    /// </summary>
    public interface IEntityComponent : IComponentWithData
    {
        public string Id { get; }
    }
}