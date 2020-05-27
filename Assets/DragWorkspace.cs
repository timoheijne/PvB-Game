using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragWorkspace : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private RectTransform Workspace;

    [SerializeField]
    private Vector2 bounds;

    private CameraMovement _cameraMovement;
    private bool isDragging;

    private Vector3 lastPosition;

    private void Start()
    {
        _cameraMovement = Camera.main.transform.parent.GetComponent<CameraMovement>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        lastPosition = Workspace.localPosition - Input.mousePosition;
    }

    private void Update()
    {
        if (!isDragging)
        {
            return;
        }

        _cameraMovement?.SetFreeze(isDragging);


        Vector3 newPos = lastPosition + Input.mousePosition;

        newPos.x = Mathf.Min(Mathf.Max(newPos.x, -bounds.x), bounds.x);
        newPos.y = Mathf.Min(Mathf.Max(newPos.y, -bounds.y), bounds.y);

        Workspace.localPosition = newPos;

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            ReleaseDrag();
        }
    }

    private void ReleaseDrag()
    {
        isDragging = false;
        _cameraMovement?.SetFreeze(false);
    }
}
