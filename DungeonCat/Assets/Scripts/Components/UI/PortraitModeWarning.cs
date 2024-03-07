using UnityEngine;

namespace Scripts.Components.UI
{
    /// <summary>
    /// Script that handles Portrait Mode on some devices
    /// </summary>
    public class PortraitModeWarning : MonoBehaviour
    {
        public GameObject image;
        
        private void Update()
        {
            image.SetActive(Camera.main!.aspect < 1.3);
        }
    }
}