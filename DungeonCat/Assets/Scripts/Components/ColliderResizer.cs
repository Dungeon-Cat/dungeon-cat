using UnityEngine;
namespace Scripts.Components
{
    /// <summary>
    ///  Script that resizes Collider2D 
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(BoxCollider2D))]
    [ExecuteAlways]
    public class ColliderResizer : MonoBehaviour
    {
        private RectTransform rectTransform;
        private BoxCollider2D boxCollider;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            // Get the size of the RectTransform
            var rect = rectTransform.rect;
            var size = new Vector2(rect.width, rect.height);

            // Calculate the offset based on the pivot
            var pivot = rectTransform.pivot;
            var pivotOffset = new Vector2((0.5f - pivot.x) * size.x, (0.5f - pivot.y) * size.y);

            // Apply the size and offset to the BoxCollider2D
            boxCollider.size = size;
            boxCollider.offset = pivotOffset;
        }
    }
}