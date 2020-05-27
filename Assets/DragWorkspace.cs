using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragWorkspace : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private GameObject Workspace;

    private CameraMovement _cameraMovement;
    private Vector2 lastMousePosition;
    private bool isDragging;

    private void Start()
    {
        _cameraMovement = Camera.main.transform.parent.GetComponent<CameraMovement>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
        isDragging = true;
    }

    private void Update()
    {
        if (!isDragging)
        {
            return;
        }

        _cameraMovement?.SetFreeze(isDragging);

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            ReleaseDrag();
        }

        Vector2 currentMousePosition = Input.mousePosition;
        Vector2 diff = currentMousePosition - lastMousePosition;
        RectTransform rect = GetComponent<RectTransform>();

        Vector3 newPosition = rect.position + new Vector3(diff.x, diff.y, transform.position.z);
        Vector3 oldPos = rect.position;
        rect.position = newPosition;
        if (!IsRectTransformInsideSreen(rect))
        {
            rect.position = oldPos;
        }
        lastMousePosition = currentMousePosition;
    }

    private void ReleaseDrag()
    {
        isDragging = false;
        _cameraMovement?.SetFreeze(false);
    }

    private bool IsRectTransformInsideSreen(RectTransform rectTransform)
    {
        bool isInside = false;
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        int visibleCorners = 0;
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        foreach (Vector3 corner in corners)
        {
            if (rect.Contains(corner))
            {
                visibleCorners++;
            }
        }
        if (visibleCorners == 4)
        {
            isInside = true;
        }
        return isInside;
    }
}
