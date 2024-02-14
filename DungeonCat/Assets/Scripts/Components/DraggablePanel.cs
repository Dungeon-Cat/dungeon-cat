using UnityEngine;
using UnityEngine.EventSystems;
namespace Scripts.Components
{
    public class DraggablePanel : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        private RectTransform rectTransform;
        private Vector2 originalLocalPointerPosition;
        private Vector3 originalPanelLocalPosition;
        private RectTransform parentRectTransform;
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            parentRectTransform = transform.parent as RectTransform;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
            originalPanelLocalPosition = rectTransform.localPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (rectTransform == null) return;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out var localPointerPosition))
            {
                Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
                rectTransform.localPosition = originalPanelLocalPosition + offsetToOriginal;
            }
        }
    }
}