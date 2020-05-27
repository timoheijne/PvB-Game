using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Security.Cryptography;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragUI : MonoBehaviour, IPointerDownHandler
{
    public RectTransform WorkspaceTransform;

    private CameraMovement _cameraMovement;
    private Vector2 lastMousePosition;
    private bool isDragging;

    private void Start()
    {
        _cameraMovement = Camera.main.transform.parent.GetComponent<CameraMovement>();
    }

    /// <summary>
    /// This method will be called on the start of the mouse drag
    /// </summary>
    /// <param name="eventData">mouse pointer event data</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
        GetComponent<Node>()?.RemoveNode();
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

        Node[] nodes = FindObjectsOfType<Node>();
        foreach (Node node in nodes)
        {
            if (node.gameObject.tag.Equals("TrashCan") && GetComponent<Head>() == null && node.IsColliding(GetComponent<RectTransform>().position))
            {
                Destroy(gameObject);
                return;
            }
            
            if(node.gameObject != gameObject && node.IsColliding(GetComponent<RectTransform>().position) && GetComponent<Head>() == null && !node.gameObject.tag.Equals("TrashCan"))
            {
                Node _thisNode = GetComponent<Node>();
                node.InsertNode(_thisNode, GetComponent<RectTransform>().rect.height + -_thisNode.UiOffset.y);
                return;
            }
        }
    }

    /// <summary>
    /// This methods will check is the rect transform is inside the screen or not
    /// </summary>
    /// <param name="rectTransform">Rect Trasform</param>
    /// <returns></returns>
    private bool IsRectTransformInsideSreen(RectTransform rectTransform)
    {
        bool isInside = false;
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        int visibleCorners = 0;
        Rect rect = (WorkspaceTransform != null ? new Rect((1 - WorkspaceTransform.rect.width / 1920) * Screen.width, 100, WorkspaceTransform.rect.width / 1920 * Screen.width, Screen.height-100) : new Rect(0, 0, Screen.width, Screen.height));
        
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