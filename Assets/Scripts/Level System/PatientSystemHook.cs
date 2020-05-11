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
        
        Debug.Log("Patient System Loaded");
        
        PatientSystem.Instance.OnPatientDone += OnPatientDone;
        PatientSystem.Instance.SetLevelFiles(_levelSystem.ActiveLevel.PatientFiles);
    }

    private void OnPatientDone(PatientFile obj)
    {
        if (_levelSystem.ActiveLevel.NextLevel != string.Empty)
        {
            LevelObject _level = _levelSystem.GetLevel(_levelSystem.ActiveLevel.NextLevel);
            _levelSystem.ChangeLevel(_level);
        }
        
        // TODO: Open level success menu!
        _levelSystem.ChangeToMainMenu(true);
    }
}