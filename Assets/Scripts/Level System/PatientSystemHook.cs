using System;
using UnityEngine;

public class PatientSystemHook : MonoBehaviour
{
    private LevelSystem _levelSystem;
    private void Start()
    {
        if(PatientSystem.Instance == null)
        {
            throw new ArgumentNullException("PatientSystem");
        }

        _levelSystem = GetComponent<LevelSystem>();
        if(_levelSystem == null) 
        {
            throw new ArgumentNullException("LevelSystem");
        }
        
        
        PatientSystem.Instance.OnPatientDone += OnPatientDone;
    }

    private void OnPatientDone(PatientFile obj)
    {
        throw new NotImplementedException();
    }
}