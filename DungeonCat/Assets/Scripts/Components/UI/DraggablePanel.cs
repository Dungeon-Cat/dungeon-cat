using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Components.UI
{
    public class DraggablePanel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private RectTransform rectTransform;
        private Vector2 originalLocalPointerPosition;
        private Vector3 originalPanelLocalPosition;
        private RectTransform parentRectTransform;

        [HideInInspector]
        public bool isDragging;

        public int edgeMargin;

        public bool resetAtEnd;
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            parentRectTransform = transform.parent as RectTransform;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (edgeMargin < 0 || IsPointerOnEdge(eventData))
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
                originalPanelLocalPosition = rectTransform.localPosition;
                isDragging = true;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (rectTransform == null || !isDragging) return;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out var localPointerPosition))
            {
                Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
                rectTransform.localPosition = originalPanelLocalPosition + offsetToOriginal;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
            if (resetAtEnd)
            {
                rectTransform.localPosition = originalPanelLocalPosition;
            }
        }
        
        private bool IsPointerOnEdge(PointerEventData eventData)
        {
            Vector2 localPointerPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition);

            // Check if the pointer is within the edge margins
            return Mathf.Abs(localPointerPosition.x) > rectTransform.rect.width / 2 - edgeMargin ||
                   Mathf.Abs(localPointerPosition.y) > rectTransform.rect.height / 2 - edgeMargin;
        }
    }
}