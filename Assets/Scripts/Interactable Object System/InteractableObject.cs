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
    
    [SerializeField, Tooltip("If you want the object to send out particles when being used drag the particle system in here.")]
    private ParticleSystem _particleSystem;

    private Patient _patient;
    
    private void Start()
    {
        Collider _collider = GetComponent<Collider>();
        if (_collider == null || _collider.enabled == false)
        {
            Debug.LogError("Interactable object does not have collider");
        }
    }

    public bool Interact()
    {
        if (!HasPatient())
        {
            return false;
        }

        // start particle if present.
        if (_particleSystem != null)
        {
            _particleSystem.Play();
        }

        return true;
    }

    public bool HasPatient()
    {
        return _patient != null;
    }

    public void PlacePatient(Patient patient)
    {
        if (patient == null)
        {
            throw new ArgumentNullException("Patient");
        }
        
        _patient = patient;

        _patient.transform.position = _patientPlace.position;
        _patient.transform.rotation = _patientPlace.rotation;
    }

    public Transform TakePatient()
    {
        if (_patient == null)
        {
            return null;
        }
        
        // Make sure to stop particle.
        if (_particleSystem != null)
        {
            _particleSystem.Stop();
        }
        
        Patient patient = _patient;
        _patient = null;
        
        return patient.transform;
    }
    
    
}