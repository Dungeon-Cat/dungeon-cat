using UnityEngine;
namespace Scripts.Components
{
    public interface IComponentWithData
    {
        public GameObject GameObject { get; }

        public void SetData(Data.Data data);

        public void SyncFromData();

        public void SyncToData();
    }
}