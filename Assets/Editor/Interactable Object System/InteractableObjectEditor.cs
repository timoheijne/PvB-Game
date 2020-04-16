using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(InteractableObject))]
public class InteractableObjectEditor : Editor
{
    private List<Objective> _objectives = new List<Objective>();
    private readonly List<string> _popupOptions = new List<string>();
    private int _selectedObject;

    private SerializedProperty _objectiveName;

    private void OnEnable()
    {
        _objectiveName = serializedObject.FindProperty("_objectiveName");
    }

    public override void OnInspectorGUI()
    {
        InteractableObject _interactableObject = (InteractableObject) target;
        LoadObjectives();
        ParseOptions(_interactableObject);
        EditorGUILayout.LabelField("Selected Objective");
        _selectedObject = EditorGUILayout.Popup(_selectedObject, _popupOptions.ToArray());

        if (_selectedObject == 0) return;
        _objectiveName.stringValue = _popupOptions[_selectedObject];
        serializedObject.ApplyModifiedProperties();
    }
    
    private void LoadObjectives()
    {
        _objectives = Resources.LoadAll<Objective>("Objectives").ToList();
    }

    private void ParseOptions(InteractableObject _target)
    {
        _popupOptions.Clear();
        _popupOptions.Add("-- SELECT ITEM --");
        for (int i = 0; i < _objectives.Count; i++)
        {
            _popupOptions.Add(_objectives[i].InternalName);
            if (_objectives[i].InternalName == _target.ObjectiveName)
            {
                _selectedObject = i + 1;
            }
        }
    }
}