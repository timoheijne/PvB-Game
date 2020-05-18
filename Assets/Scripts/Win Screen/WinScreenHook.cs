using System;
using UnityEngine;

public class WinScreenHook : MonoBehaviour
{
    private void Start()
    {
        PatientSystem.Instance.OnPatientDone += OnPatientDone;
    }

    private void OnPatientDone(PatientFile obj)
    {
        if (PatientSystem.Instance.GetNextPatient() == null)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void ReturnToHome()
    {
        LevelSystem.Instance.ChangeToMainMenu();
    }

    public void NextLevel()
    {
        if (LevelSystem.Instance.ActiveLevel.NextLevel != string.Empty)
        {
            bool _changed = LevelSystem.Instance.ChangeLevel(LevelSystem.Instance.ActiveLevel.NextLevel);
            if (!_changed)
            {
                ReturnToHome();
            }
        }
        else
        {
            ReturnToHome();
        }
    }
}