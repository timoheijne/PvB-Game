using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ObjectiveName => _objectiveName;

    [SerializeField]
    private string _objectiveName;
    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();

        if (_collider == null || _collider.enabled == false)
        {
            Debug.LogError("Interactable object does not have collider");
        }
    }
    
    
}