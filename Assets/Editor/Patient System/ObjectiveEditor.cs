using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Objective))]
public class ObjectiveEditor : Editor
{
    private List<Objective> _objectives = new List<Objective>();
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        LoadObjectives();
        Objective _objective = (Objective) target;
        
        Objective internalName = _objectives.Find(o => o.InternalName == _objective.InternalName && o != _objective);
        if (internalName != null)
        {
            EditorGUILayout.HelpBox("Duplicate internal name!", MessageType.Error);
            _objective.Active = false;
        } 

        _objective.FriendlyName = EditorGUILayout.TextField("Friendly Name", _objective.FriendlyName);
        _objective.InternalName = EditorGUILayout.TextField("Internal Name", _objective.InternalName);
        _objective.Active = EditorGUILayout.Toggle("Active", _objective.Active);
        
        serializedObject.ApplyModifiedProperties();
        if (GUILayout.Button("Save Objective"))
        {
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh ();
        }
    }
    
    private void LoadObjectives()
    {
        _objectives = Resources.LoadAll<Objective>("Objectives").ToList();
    }
}