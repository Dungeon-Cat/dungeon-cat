using UnityEngine;

namespace Scripts.UI
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager Instance { get; private set; }

        public bool isDragging;

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}