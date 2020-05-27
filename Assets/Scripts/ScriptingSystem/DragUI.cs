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
    private RectTransform rectTransform;

    private float leftBoundary;
    private float rightBoundary;
    private float topBoundary;
    private float bottomBoundary;

    private void Start()
    {
        _cameraMovement = Camera.main.transform.parent.GetComponent<CameraMovement>();
        rectTransform = GetComponent<RectTransform>();
        
        //these calculations mark the outer edges of the work space
        leftBoundary = (1 - (WorkspaceTransform.rect.width - rectTransform.rect.width) / 1920) * Screen.width;
        rightBoundary = (1 - rectTransform.rect.width / 1920) * Screen.width;
        topBoundary = (1 - rectTransform.rect.height / 1080) * Screen.height;
        bottomBoundary = 100;
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

        Vector3 newPos = Input.mousePosition;

        newPos.x = Mathf.Min(Mathf.Max(newPos.x, leftBoundary), rightBoundary);
        newPos.y = Mathf.Min(Mathf.Max(newPos.y, bottomBoundary), topBoundary);

        rectTransform.position = newPos;

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            ReleaseDrag();
        }
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