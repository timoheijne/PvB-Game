using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ObjectiveName => _objectiveName;
    public Transform PatientPlace => _patientPlace;
    
    [SerializeField]
    private string _objectiveName;
    [SerializeField, Tooltip("A child object, the system can place the patient if the scripter wants to place a patient on this object.")]
    private Transform _patientPlace;
    
    private void Start()
    {
        Collider _collider = GetComponent<Collider>();
        if (_collider == null || _collider.enabled == false)
        {
            Debug.LogError("Interactable object does not have collider");
        }
    }

    public bool HasPatient()
    {
        return false;
    }

    public void PlacePatient()
    {
        
    }

    public GameObject TakePatient()
    {
        return null;
    }
    
    
}