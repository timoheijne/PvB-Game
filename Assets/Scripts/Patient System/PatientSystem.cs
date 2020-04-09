using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatientSystem : MonoBehaviour
{
    public static  PatientSystem Instance;
    
    public event Action<PatientFile> OnPatientDone;
    public event Action<PatientFile> OnPatientChange;
    public PatientFile ActivePatient => _activePatient;
    
    private PatientFile _activePatient;
    private List<PatientFile> _patientFiles = new List<PatientFile>();

    private void Awake()
    {
        if (PatientSystem.Instance == null)
        {
            PatientSystem.Instance = this;
        }
        else
        {
            Debug.LogError("PatientSystem already loaded.. Destroying");
            Destroy(this);
        }
    }

    private void Start()
    {
        _patientFiles = Resources.LoadAll<PatientFile>("Patient Files").ToList();
    }
    
    public bool SetActivePatient(PatientFile patient)
    {
        if (patient.Active)
        {
            _activePatient = patient;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MarkObjectiveDone(string objectiveName)
    {
        if (this.ActivePatient == null) return;
        
        this.ActivePatient.MarkObjectiveDone(objectiveName);
        
        if (this.ActivePatient.IsDone())
        {
            OnPatientDone?.Invoke(this.ActivePatient);
        }
    }
    
    
}