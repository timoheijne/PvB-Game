using System;
using TMPro;
using UnityEngine;

public class PatientUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField] 
    private TextMeshProUGUI _age;
    [SerializeField] 
    private TextMeshProUGUI _objectives;
    
    private void Start()
    {
        if (PatientSystem.Instance == null)
        {
            throw new ArgumentNullException("PatientSystem");
        }
        
        PatientSystem.Instance.OnPatientChange += OnPatientChange;
        PatientSystem.Instance.OnPatientDone += OnPatientDone;
        PatientSystem.Instance.OnObjectiveComplete += OnObjectiveComplete;
    }

    private void OnObjectiveComplete(Objective objective, PatientFile patientFile)
    {
        UpdateUI(patientFile);
    }

    private void OnPatientChange(PatientFile obj)
    {
        UpdateUI(obj);
    }
    
    private void OnPatientDone(PatientFile obj)
    {
        throw new NotImplementedException();
    }

    private void UpdateUI(PatientFile obj)
    {
        _name?.SetText($"Naam: {obj.PatientName}");
        _age?.SetText($"Leeftijd: {obj.Age}");

        string _objectiveText = "Doelen: \n";
        foreach (Objective _objective in obj.Objectives)
        {
            if (_objective.IsDone)
            {
                _objectiveText += $" - <s>{_objective.FriendlyName}</s>\n";
            }
            else
            {
                _objectiveText += $" - {_objective.FriendlyName}\n";
            }
            
        }
        
        _objectives?.SetText(_objectiveText);
    }
}