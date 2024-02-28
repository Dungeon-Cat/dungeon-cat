using UnityEngine;

namespace Scripts.UI
{
    public class UiManager : MonoBehaviour
    {
        public Material spriteDefault;
        public Material spriteHighlight;
        public GameObject portraitModeWarning;
        
        public static UiManager Instance { get; private set; }
        
        public bool isDragging;

        private void Start()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Update()
        {
            portraitModeWarning.SetActive(Camera.main!.aspect < 1);
        }
    }
}