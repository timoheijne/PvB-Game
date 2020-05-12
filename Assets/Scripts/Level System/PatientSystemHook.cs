using System;
using UnityEngine;

public class PatientSystemHook : MonoBehaviour
{
    private void Start()
    {
        if(PatientSystem.Instance == null)
        {
            throw new ArgumentNullException("PatientSystem");
        }

        if(LevelSystem.Instance == null) 
        {
            throw new ArgumentNullException("LevelSystem");
        }
        
        Debug.Log("Patient System Loaded");
        
        PatientSystem.Instance.OnPatientDone += OnPatientDone;
        PatientSystem.Instance.SetLevelFiles(LevelSystem.Instance.ActiveLevel.PatientFiles);
    }

    private void OnPatientDone(PatientFile obj)
    {
        if (LevelSystem.Instance.ActiveLevel.NextLevel != string.Empty)
        {
            LevelObject _level = LevelSystem.Instance.GetLevel(LevelSystem.Instance.ActiveLevel.NextLevel);
            LevelSystem.Instance.ChangeLevel(_level);
        }
        
        // TODO: Open level success menu!
        LevelSystem.Instance.ChangeToMainMenu(true);
    }
}