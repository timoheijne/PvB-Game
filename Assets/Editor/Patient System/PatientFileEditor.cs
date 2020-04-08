using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[UnityEditor.CustomEditor(typeof(PatientFile))]
public class PatientFileEditor : UnityEditor.Editor
{
    private List<Objective> _objectives = new List<Objective>();
    private readonly List<string> _popupOptions = new List<string>();
    private int _selectedObject = 0;
    
    public override void OnInspectorGUI()
    {
        PatientFile patientFile = (PatientFile) target;

        patientFile.PatientName = EditorGUILayout.TextField("Patient Name", patientFile.PatientName);
        patientFile.Age = EditorGUILayout.IntField("Age", patientFile.Age);
        patientFile.Difficulty = (Shared.Difficulty)EditorGUILayout.EnumPopup("Difficulty", patientFile.Difficulty);
        patientFile.Active = EditorGUILayout.Toggle("Active", patientFile.Active);
            
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Prefab");
        patientFile.Prefab =
            (GameObject) EditorGUILayout.ObjectField(patientFile.Prefab, typeof(GameObject), false);
        EditorGUILayout.Space(5);
            
        EditorGUILayout.LabelField("Objectives:");
        EditorGUILayout.Space(1);

        int inactiveObjectives = patientFile.Objectives.ToList().FindAll(o => !o.Active).Count;

        if (inactiveObjectives > 0)
        {
            EditorGUILayout.HelpBox("This patient file contains inactive objectives. These will be ignored in game!", MessageType.Warning);
        }
        
        foreach (Objective objective in patientFile.Objectives)
        {
            EditorGUILayout.BeginHorizontal();

            string label = objective.Active ? $"- {objective.FriendlyName}" : $"- {objective.FriendlyName} (INACTIVE)";
            EditorGUILayout.LabelField(label);
            if (GUILayout.Button("Remove"))
            {
                List<Objective> _targetObjectives = patientFile.Objectives.ToList();
                _targetObjectives.Remove(objective);
                patientFile.Objectives = _targetObjectives.ToArray();
            }
            EditorGUILayout.EndHorizontal();
        }
        
        loadObjectives();
        parseOptions(patientFile);
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
                addToObjectives(patientFile, _popupOptions[_selectedObject]);
                _selectedObject = 0;
            }
        }
        
        EditorGUILayout.EndHorizontal();
    }

    private void addToObjectives(PatientFile _target, string _objectiveName)
    {
        Objective objective = _objectives.Find(o => o.InternalName == _objectiveName);
        if (objective == null)
        {
            Debug.LogError("Tried to add invalid objective to patient file");
            return;
        }
        else
        {
            List<Objective> _targetObjectives = _target.Objectives.ToList();
            _targetObjectives.Add(objective);
            _target.Objectives = _targetObjectives.ToArray();
        }
    }

    private void loadObjectives()
    {
        _objectives = Resources.LoadAll<Objective>("Objectives").ToList();
    }

    private void parseOptions(PatientFile _target)
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

