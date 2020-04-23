using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelObject))]
public class LevelObjectEditor : Editor
{
    private LevelObject[] _levels = new LevelObject[0];
    private PatientFile[] _patientFiles = new PatientFile[0];
    
    private readonly List<string> _levelOptions = new List<string>();
    private int _selectedLevel;
    
    private readonly List<string> _patientOptions = new List<string>();
    private int _selectedPatient;
    
    public override void OnInspectorGUI()
    {
        LevelObject _levelObject = (LevelObject) target;

        _levelObject.LevelName = EditorGUILayout.TextField("Level Name", _levelObject.LevelName);
        _levelObject.Difficulty = (Shared.Difficulty)EditorGUILayout.EnumPopup("Difficulty", _levelObject.Difficulty);
        _levelObject.SceneName = EditorGUILayout.TextField("Scene Name", _levelObject.SceneName);
        _levelObject.IsEnabled = EditorGUILayout.Toggle("Enabled", _levelObject.IsEnabled);
            
        // ---------- Select Next Level ----------
        EditorGUILayout.Space(10);
        
        LoadLevels(_levelObject);
        EditorGUILayout.LabelField("Next Level:");
        _selectedLevel = EditorGUILayout.Popup(_selectedLevel, _levelOptions.ToArray());
        if (_selectedLevel == 0)
        {
            _levelObject.NextLevel = string.Empty;
        }
        else
        {
            _levelObject.NextLevel = _levels[_selectedLevel - 1].LevelName;
        }
        
        // ---------- List Patient Files ----------
        
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Patients:");
        foreach (var _patientFile in _levelObject.PatientFiles)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"- {_patientFile.PatientName}");
            if (GUILayout.Button("Remove"))
            {
                _levelObject.PatientFiles = DeletePatient(_levelObject, _patientFile);
            }
            EditorGUILayout.EndHorizontal();
        }
        
        // ---------- Add Patient File ----------
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Add Patient");
        LoadPatients(_levelObject);
        
        EditorGUILayout.BeginHorizontal();
        _selectedPatient = EditorGUILayout.Popup(_selectedPatient, _patientOptions.ToArray());
        if (GUILayout.Button("Add"))
        {
            _levelObject.PatientFiles = AddPatientToLevel(_levelObject, _patientFiles[_selectedPatient - 1]);
            _selectedPatient = 0;
        }
        EditorGUILayout.EndHorizontal();
    }

    private PatientFile[] DeletePatient(LevelObject _levelObject, PatientFile patientFile)
    {
        List<PatientFile> patientFiles = _levelObject.PatientFiles.ToList();
        patientFiles.Remove(patientFile);

        return patientFiles.ToArray();
    }

    private void LoadLevels(LevelObject _level)
    {
        _levelOptions.Clear();
        _levelOptions.Add("None");
        List<LevelObject> _rawLevels = Resources.LoadAll<LevelObject>("Levels").ToList();
        _rawLevels.Remove(_level);
        foreach (LevelObject l in _rawLevels)
        {
            _levelOptions.Add(l.LevelName);
        }

        _levels = _rawLevels.ToArray();
    }

    private void LoadPatients(LevelObject _level)
    {
        _patientOptions.Clear();
        _patientOptions.Add("-- Select Patient --");
        List<PatientFile> _rawPatientFiles = Resources.LoadAll<PatientFile>("Patient Files").ToList();

        foreach (PatientFile _patientFile in _level.PatientFiles)
        {
            _rawPatientFiles.Remove(_patientFile);
        }

        foreach (PatientFile _patientFile in _rawPatientFiles)
        {
            _patientOptions.Add(_patientFile.PatientName);
        }
        
        _patientFiles = _rawPatientFiles.ToArray();
    }

    private PatientFile[] AddPatientToLevel(LevelObject _levelObject, PatientFile patientFile)
    {
        List<PatientFile> patientFiles = _levelObject.PatientFiles.ToList();
        patientFiles.Add(patientFile);

        return patientFiles.ToArray();
    }
    
}