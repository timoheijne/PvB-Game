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
    }
    
    private void OnPatientChange(PatientFile obj)
    {
        _name?.SetText($"Naam: {obj.PatientName}");
        _age?.SetText($"Leeftijd: {obj.Age}");

        string _objectiveText = "Doelen: \n  ";
        foreach (Objective _objective in obj.Objectives)
        {
            _objectiveText += $"- {_objective.FriendlyName}\n";
        }
        
        _objectives?.SetText(_objectiveText);
    }
    
    private void OnPatientDone(PatientFile obj)
    {
        throw new NotImplementedException();
    }
}