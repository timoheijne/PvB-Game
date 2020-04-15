using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(PatientSystem))]
public class PatientSystemEditor : Editor
{
    
    private readonly List<string> _popupOptions = new List<string>();
    private int _selectedObject;
    
    public override void OnInspectorGUI()
    {
        PatientSystem _patientSystem = (PatientSystem) target;
        
        EditorGUILayout.LabelField($"Loaded patients: {_patientSystem.GetLoadedPatients().Length}");

        string patientName = "No Active Patient";
        if (_patientSystem.ActivePatient != null)
        {
            patientName = _patientSystem.ActivePatient.PatientName;
        }
        EditorGUILayout.LabelField($"Active Patient: {patientName}");
        
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Set active patient");

        _popupOptions.Clear();
        _popupOptions.Add("-- SELECT ITEM --");
        foreach (PatientFile patientFile in _patientSystem.GetLoadedPatients())
        {
            _popupOptions.Add(patientFile.PatientName);
        }
        
        EditorGUILayout.BeginHorizontal();
        _selectedObject = EditorGUILayout.Popup(_selectedObject, _popupOptions.ToArray());
        if (GUILayout.Button("Set"))
        {
            if (_selectedObject == 0)
            {
                Debug.LogError("Nothing Selected!");
            }
            else
            {
                _patientSystem.SetActivePatient(_patientSystem.GetLoadedPatients()[_selectedObject - 1]); 
                _selectedObject = 0;
            }
        }
        EditorGUILayout.EndHorizontal();
    }
    
}