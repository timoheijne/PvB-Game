using System;
using UnityEngine;

public class ResetPatientHook : ResetBehaviour
{
    private Patient _patient;

    private void Start()
    {
        _patient = GetComponent<Patient>();

        if (_patient == null)
        {
            Debug.LogError("Patient object not found on GameObject Destroying");
            Destroy(this);
        }
    }

    public override void InvokeReset()
    {
        InteractableObject[] _objects = GameManager.FindObjectsOfType<InteractableObject>();

        foreach (InteractableObject _object in _objects)
        {
            _object.TakePatient();
        }
        
        PatientSystem.Instance?.SetActivePatient(_patient.PatientFile);
        Destroy(gameObject);
    }
}