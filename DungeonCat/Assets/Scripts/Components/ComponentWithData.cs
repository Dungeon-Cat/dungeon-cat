using System.Collections;
using UnityEngine;
namespace Scripts.Components
{
    public abstract class ComponentWithData<T> : MonoBehaviour, IComponentWithData where T : Data.Data
    {
        [SerializeField]
        public T data;
        
        public GameObject GameObject => gameObject;

        public void SetData(Data.Data d) => data = d as T;

        public virtual void SyncFromData()
        {

        }

        public virtual void SyncToData()
        {
        }

        protected virtual void OnValidateInEditor()
        {
            SyncToData();
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            //Prevents attempts to run while initializing
            if (!gameObject.activeInHierarchy) return;

            StartCoroutine(DelayedValidate());
        }

        private IEnumerator DelayedValidate()
        {
            //Waits for first available frame
            yield return null;

            OnValidateInEditor();
        }
#endif
    }
}