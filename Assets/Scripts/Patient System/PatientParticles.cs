using System;
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
            Destroy(_particle, 5);
        } 
        
        if (_objective.InternalName == "wash")
        {
            //Instantiate(_healParticle, transform);
        }
    }
    
    private void OnDestroy()
    {
        Debug.Log("Bye");
        PatientSystem.Instance.OnObjectiveComplete -= OnObjectiveDone;
    }
}       