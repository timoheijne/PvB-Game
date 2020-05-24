using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[UnityEditor.CustomEditor(typeof(PatientFile))]
public class PatientFileEditor : UnityEditor.Editor
{
    private List<Objective> _objectives = new List<Objective>();
    private readonly List<string> _popupOptions = new List<string>();
    private int _selectedObject;
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        PatientFile _patientFile = (PatientFile) target;

        _patientFile.PatientName = EditorGUILayout.TextField("Patient Name", _patientFile.PatientName);
        _patientFile.Age = EditorGUILayout.IntField("Age", _patientFile.Age);
        _patientFile.Difficulty = (Shared.Difficulty)EditorGUILayout.EnumPopup("Difficulty", _patientFile.Difficulty);
        _patientFile.Active = EditorGUILayout.Toggle("Active", _patientFile.Active);
            
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Prefab");
        _patientFile.Prefab =
            (GameObject) EditorGUILayout.ObjectField(_patientFile.Prefab, typeof(GameObject), false);
        EditorGUILayout.Space(5);
            
        EditorGUILayout.LabelField("Objectives:");
        EditorGUILayout.Space(1);

        int _inactiveObjectives = 0;
        if (_patientFile.Objectives != null)
        {
            _inactiveObjectives = _patientFile.Objectives.ToList().FindAll(o => !o.Active).Count;
        }
        

        if (_inactiveObjectives > 0)
        {
            EditorGUILayout.HelpBox("This patient file contains inactive objectives. These will be ignored in game!", MessageType.Warning);
        }
        
        foreach (Objective objective in _patientFile.Objectives)
        {
            EditorGUILayout.BeginHorizontal();

            string label = objective.Active ? $"- {objective.FriendlyName}" : $"- {objective.FriendlyName} (INACTIVE)";
            EditorGUILayout.LabelField(label);
            if (GUILayout.Button("Remove"))
            {
                List<Objective> _targetObjectives = _patientFile.Objectives.ToList();
                _targetObjectives.Remove(objective);
                _patientFile.Objectives = _targetObjectives.ToArray();
            }
            EditorGUILayout.EndHorizontal();
        }
        
        LoadObjectives();
        ParseOptions(_patientFile);
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Add Objective");
        EditorGUILayout.BeginHorizontal();
        
        _selectedObject = EditorGUILayout.Popup(_selectedObject, _popupOptions.ToArray());
        if (GUILayout.Button("Add"))
        {
            if (_selectedObject == 0)
            {
                Debug.LogError("Nothing Selected!");
            }
            else
            {
                AddToObjectives(_patientFile, _popupOptions[_selectedObject]);
                _selectedObject = 0;
            }
        }
        
        EditorGUILayout.EndHorizontal();
        
        serializedObject.ApplyModifiedProperties();
        if (GUILayout.Button("Save Patient"))
        {
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh ();
        }
    }

    private void AddToObjectives(PatientFile _target, string _objectiveName)
    {
        Objective objective = _objectives.Find(o => o.InternalName == _objectiveName);
        if (objective == null)
        {
            Debug.LogError("Tried to add invalid objective to patient file");
        }
        else
        {
            List<Objective> _targetObjectives = _target.Objectives.ToList();
            _targetObjectives.Add(objective);
            _target.Objectives = _targetObjectives.ToArray();
        }
    }

    private void LoadObjectives()
    {
        _objectives = Resources.LoadAll<Objective>("Objectives").ToList();
    }

    private void ParseOptions(PatientFile _target)
    {
        _popupOptions.Clear();
        _popupOptions.Add("-- SELECT ITEM --");
        foreach (Objective objective in _objectives)
        {
            if (!_target.Objectives.Contains(objective) && objective.Active)
            {
                _popupOptions.Add(objective.InternalName);
            }
        }
    }
}

