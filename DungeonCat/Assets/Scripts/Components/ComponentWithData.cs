using UnityEngine;
namespace Scripts.Components
{
    public abstract class ComponentWithData<T> : MonoBehaviour
    {
        [SerializeField]
        public T data;
        
        public virtual void SyncFromData()
        {
            
        }
        
        public virtual void SyncToData()
        {
            
        }
    }
}