using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryBlockInstantiator : MonoBehaviour, IPointerDownHandler
{
    private GameObject _prefab;
    private Transform _canvas;

    private void Start()
    {
        _canvas = GameObject.FindWithTag("MainCanvas").transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Spawn block
        // Attach to mouse immediately 
        GameObject _instance = Instantiate(_prefab, _canvas);
        _instance.transform.position = transform.position;
        DragUI _dragUi = _instance.GetComponent<DragUI>();
        _dragUi.OnPointerDown(eventData);
    }

    public void SetPrefab(GameObject _newPrefab)
    {
        _prefab = _newPrefab;
    }
}