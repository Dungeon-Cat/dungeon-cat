using UnityEngine;
namespace Scripts.Components
{
    /// <summary>
    /// Interface that avoids issues with generics of ComponentWithData
    /// </summary>
    public interface IComponentWithData
    {
        public GameObject GameObject { get; }

        public void SetData(Data.Data data);

        public void SyncFromData();

        public void SyncToData();
    }
}