using System;
using UnityEngine;

public class PatientSpawningHook : MonoBehaviour
{
    [SerializeField, Tooltip("Interactable object the patient is spawned on")]
    private InteractableObject _spawnObject;
    
    private void Start()
    {
        if (PatientSystem.Instance == null)
        {
            throw new ArgumentNullException("PatientSystem");
        }
        
        PatientSystem.Instance.OnPatientChange += OnPatientChange;
        PatientSystem.Instance.OnPatientDone += OnPatientDone;
    }
    
    private void OnPatientChange(PatientFile obj)
    {
        if (_spawnObject == null)
        {
            throw new ArgumentNullException("SpawnObject");
        }
        if (obj.Prefab == null)
        {
            throw new ArgumentNullException("Patient Prefab");
        }
        
        // TODO: Spawn particle so it doesn't just "plop" in

        GameObject _patient = Instantiate(obj.Prefab);
        Patient _data = _patient.GetComponent<Patient>();
        if (_data == null)
        {
            throw new ArgumentNullException("Patient Data");
        }
        
        _spawnObject.PlacePatient(_data);
    }
    
    private void OnPatientDone(PatientFile obj)
    {
        throw new NotImplementedException();
    }

}