using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryBlockInstantiator : MonoBehaviour, IPointerDownHandler
{
    private GameObject _prefab;
    private Transform _panel;

    public RectTransform Workspace;

    private void Start()
    {
        _panel = GameObject.FindWithTag("VisualScriptingPanel").transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Spawn block
        // Attach to mouse immediately 
        GameObject _instance = Instantiate(_prefab, _panel);
        _instance.transform.position = transform.position;
        DragUI _dragUi = _instance.GetComponent<DragUI>();
        _dragUi.OnPointerDown(eventData);
        _dragUi.WorkspaceTransform = Workspace;
    }

    public void SetPrefab(GameObject _newPrefab)
    {
        _prefab = _newPrefab;
    }
}