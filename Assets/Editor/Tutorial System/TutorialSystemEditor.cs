using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(TutorialSystem))]
public class TutorialSystemEditor : Editor
{
    private string _tutorialID;
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        EditorGUILayout.Space(10);
        
        TutorialSystem _tutorialSystem = (TutorialSystem)target;

        _tutorialID = EditorGUILayout.TextField("Tutorial ID", _tutorialID);
        if (GUILayout.Button("Activate Tutorial"))
        {
            _tutorialSystem.ActivateTutorial(_tutorialID);
        }

        if (_tutorialSystem.ActiveTutorial != null && GUILayout.Button("Deactivate Tutorial"))
        {
            _tutorialSystem.DeactivateTutorial();
        }
    }
}