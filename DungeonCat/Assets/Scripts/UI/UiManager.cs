using UnityEngine;

namespace Scripts.UI
{
    public class UiManager : MonoBehaviour
    {
        public Material spriteDefault;
        public Material spriteHighlight;
        
        public static UiManager Instance { get; private set; }

        private void Start()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}