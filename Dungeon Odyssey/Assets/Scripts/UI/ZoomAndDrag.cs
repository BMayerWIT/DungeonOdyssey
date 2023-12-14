using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ZoomAndDrag : MonoBehaviour, IPointerClickHandler, IScrollHandler, IDragHandler
{
    public float zoomSpeed = 0.5f;
    public float maxZoom = 5f;
    public float minZoom = 1f;

    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;

    public void OnScroll(PointerEventData eventData)
    {
        float scrollDelta = eventData.scrollDelta.y;
        float newScale = Mathf.Clamp(transform.localScale.x - scrollDelta * zoomSpeed, minZoom, maxZoom);
        transform.localScale = new Vector3(newScale, newScale, 1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            transform.localScale = Vector3.one; // Double-click to reset zoom
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            dragCurrentPosition = eventData.position;
            Vector3 difference = dragStartPosition - dragCurrentPosition;
            transform.position -= difference;
            dragStartPosition = dragCurrentPosition;
        }
    }
}
