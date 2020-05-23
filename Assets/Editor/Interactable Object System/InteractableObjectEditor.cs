using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

[CanEditMultipleObjects]
[CustomEditor(typeof(InteractableObject))]
public class InteractableObjectEditor : Editor
{
    private List<Objective> _objectives = new List<Objective>();
    private readonly List<string> _popupOptions = new List<string>();
    private int _selectedObject;

    private SerializedProperty _objectiveName;
    private SerializedProperty _patientPlace;
    private SerializedProperty _particleSystem;


    private void OnEnable()
    {
        _objectiveName = serializedObject.FindProperty("_objectiveName");
        _patientPlace = serializedObject.FindProperty("_patientPlace");
        _particleSystem = serializedObject.FindProperty("_particleSystem");
    }

    public override void OnInspectorGUI()
    {
        InteractableObject _interactableObject = (InteractableObject) target;
        LoadObjectives();
        ParseOptions(_interactableObject);
        EditorGUILayout.LabelField("Selected Objective");
        _selectedObject = EditorGUILayout.Popup(_selectedObject, _popupOptions.ToArray());
        
        EditorGUILayout.LabelField("Patient Place Transform");
        UnityEngine.Object _selectedPlace = EditorGUILayout.ObjectField(_interactableObject.PatientPlace, typeof(Transform), true);
        
        EditorGUILayout.LabelField("Particle System");
        UnityEngine.Object _selectedParticle = EditorGUILayout.ObjectField(_particleSystem.objectReferenceValue, typeof(ParticleSystem), true);

        if (_selectedObject == 0)
        {
            return;
        }
        
        _objectiveName.stringValue = _popupOptions[_selectedObject];
        _patientPlace.objectReferenceValue = _selectedPlace;
        _particleSystem.objectReferenceValue = _selectedParticle;
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