using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(ObjectInteractor))]
public class ObjectInteractorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ObjectInteractor objectInteractor = (ObjectInteractor)target;
        
        if (GUILayout.Button("Manually Run Interactor"))
        {
            objectInteractor.InteractWithObject();
        }
    }
}