using UnityEngine;

namespace Scripts.Components.UI
{
    public class PortraitModeWarning : MonoBehaviour
    {
        public GameObject image;
        
        private void Update()
        {
            image.SetActive(Camera.main!.aspect < 1.3);
        }
    }
}