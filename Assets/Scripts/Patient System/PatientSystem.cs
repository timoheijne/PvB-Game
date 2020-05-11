using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatientSystem : MonoBehaviour
{
    public static PatientSystem Instance;
    
    public event Action<PatientFile> OnPatientDone;
    public event Action<PatientFile> OnPatientChange;
    public PatientFile ActivePatient => (_activePatientIndex < _patientFiles.Count) ? _patientFiles[_activePatientIndex] : null;
    
    private int _activePatientIndex;
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

    public PatientFile[] GetLoadedPatients()
    {
        return _patientFiles.ToArray();
    }

    public void SetLevelFiles(PatientFile[] patientFiles)
    {
        _patientFiles = patientFiles.ToList();

        if (_patientFiles.Count > 0)
        {
            SetActivePatient(_patientFiles[0]);
        }
    }
    
    
    public bool SetActivePatient(PatientFile patient)
    {
        if (patient.Active)
        {
            int index = _patientFiles.FindIndex(p => p == patient);

            if (index > -1)
            {
                _activePatientIndex = index;
                OnPatientChange?.Invoke(patient);
            }
            else
            {
                _activePatientIndex = 0;
                OnPatientChange?.Invoke(ActivePatient);
            }
            
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool MarkObjectiveDone(string _objectiveName)
    {
        if (this.ActivePatient == null)
        {
            return false;
        }
        
        bool _success = this.ActivePatient.MarkObjectiveDone(_objectiveName);
        
        if (this.ActivePatient.IsDone())
        {
            OnPatientDone?.Invoke(this.ActivePatient);
        }

        return _success;
    }
    
    
}