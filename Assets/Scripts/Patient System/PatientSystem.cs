using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatientSystem : MonoBehaviour
{
    public static PatientSystem Instance;
    
    public event Action<PatientFile> OnPatientDone;
    public event Action<PatientFile> OnPatientChange;
    
    public event Action<Objective, PatientFile> OnObjectiveComplete;
    public PatientFile ActivePatient => (_activePatientIndex < _patientFiles.Count) ? _patientFiles[_activePatientIndex] : null;
    
    public bool AutoContinue
    {
        get => _autoContinue;
        set => _autoContinue = value;
    }

    [SerializeField, Tooltip("If you want to automatically continue to new patient when current is finished")]
    private bool _autoContinue = true;
    
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

    public PatientFile GetNextPatient()
    {
        PatientFile _nextPatient = null;
        if (_activePatientIndex + 1 < _patientFiles.Count)
        {
            _nextPatient = _patientFiles[_activePatientIndex + 1];
        }

        return _nextPatient;
    }
    
    
    public bool SetActivePatient(PatientFile patient)
    {
        if (patient.Active)
        {
            int index = _patientFiles.FindIndex(p => p == patient);

            if (index > -1)
            {
                _activePatientIndex = index;
                patient.ResetObjectives();
                
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
        
        Tuple<bool, Objective> _objective = this.ActivePatient.MarkObjectiveDone(_objectiveName);

        if (_objective.Item1)
        {
            OnObjectiveComplete?.Invoke(_objective.Item2, this.ActivePatient);
        }
        
        if (this.ActivePatient.IsDone())
        {
            OnPatientDone?.Invoke(this.ActivePatient);

            if (_autoContinue && _activePatientIndex + 1 < _patientFiles.Count - 1)
            {
                SetActivePatient(_patientFiles[_activePatientIndex + 1]);
            }
        }

        return _objective.Item1;
    }
    
    
}