using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    /// <summary>
    /// Script to scale game correctly in editor
    /// </summary>
    [RequireComponent(typeof(CanvasScaler))]
    [ExecuteAlways]
    public class EditorUI : MonoBehaviour
    {
        private CanvasScaler canvasScaler;
        
        private void Awake()
        {
            canvasScaler = GetComponent<CanvasScaler>();
        }

        private void Update()
        {
            canvasScaler.scaleFactor = Math.Max(1.5f, Screen.width / 1920f);
        }
    }
}