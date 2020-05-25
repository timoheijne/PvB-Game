using System;
using System.Security.Cryptography;
using UnityEngine;

public class PatientParticles : MonoBehaviour
{
    private Patient _patient;

    [SerializeField]
    private GameObject _winParticle;
    
    [SerializeField]
    private GameObject _healParticle;
    
    [SerializeField]
    private GameObject _stinkParticle;

    private GameObject _trackStinkParticle;
    
    private void Start()
    { 
        _patient = GetComponent<Patient>();
        if (_patient == null)
        {
            Destroy(this);
            return;
        }                
        
        // Check if we need to enable stink.
        if (_patient.PatientFile.HasObjective("washing_table"))
        {
            _trackStinkParticle = Instantiate(_stinkParticle, transform);
            _trackStinkParticle.transform.position = transform.position;
        }
        
        PatientSystem.Instance.OnObjectiveComplete += OnObjectiveDone;

    }

    public void OnObjectiveDone(Objective _objective, PatientFile _patient)
    {

        if (_patient.IsDone())
        {
            // Play win particle
            GameObject _particle = Instantiate(_winParticle);
            _particle.transform.position = transform.position;
            
            Destroy(_particle, 5);
            
        } else if (_objective.InternalName == "operating_table")
        {
            GameObject _particle = Instantiate(_healParticle, transform);
            _particle.transform.position = transform.position;
            
            Destroy(_particle, 5);
        } 
        
        if (_objective.InternalName == "washing_table")
        {
            Destroy(_trackStinkParticle);
        }
    }
    
    private void OnDestroy()
    {
        PatientSystem.Instance.OnObjectiveComplete -= OnObjectiveDone;
    }
}       