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
        loadObjectives();
        Objective objective = (Objective) target;
        
        Objective internalName = _objectives.Find(o => o.InternalName == objective.InternalName && o != objective);
        if (internalName != null)
        {
            EditorGUILayout.HelpBox("Duplicate internal name!", MessageType.Error);
            objective.Active = false;
        } 

        objective.FriendlyName = EditorGUILayout.TextField("Friendly Name", objective.FriendlyName);
        objective.InternalName = EditorGUILayout.TextField("Internal Name", objective.InternalName);
        objective.Active = EditorGUILayout.Toggle("Active", objective.Active);
    }
    
    private void loadObjectives()
    {
        _objectives = Resources.LoadAll<Objective>("Objectives").ToList();
    }
}