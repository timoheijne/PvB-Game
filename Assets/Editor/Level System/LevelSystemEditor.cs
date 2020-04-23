using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(LevelSystem))]
public class LevelSystemEditor : UnityEditor.Editor
{
    private LevelObject[] _levels = new LevelObject[0];
    private readonly List<string> _levelOptions = new List<string>();
    private int _selectedLevel;
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        LevelSystem _levelSystem = (LevelSystem)target;

        if (!EditorApplication.isPlaying)
        {
            return;
        }

        string _activeLevel = "No active level";
        if (_levelSystem.ActiveLevel != null)
        {
            _activeLevel = _levelSystem.ActiveLevel.LevelName;
        }
        
        EditorGUILayout.LabelField($"Active Level: {_activeLevel}");
        
        LoadLevels();
        EditorGUILayout.LabelField("Next Level:");
        
        EditorGUILayout.BeginHorizontal();
        _selectedLevel = EditorGUILayout.Popup(_selectedLevel, _levelOptions.ToArray());
        if (GUILayout.Button("Switch"))
        {
            if (_levels[_selectedLevel - 1].SceneName == string.Empty)
            {
                Debug.LogError("No scene name has been set in this level, Can not switch");
                return;
            }
            
            _levelSystem.ChangeLevel(_levels[_selectedLevel - 1]);
        }
        EditorGUILayout.EndHorizontal();
    }
    
    private void LoadLevels()
    {
        _levelOptions.Clear();
        _levelOptions.Add("-- Select --");
        _levels = Resources.LoadAll<LevelObject>("Levels");
        
        foreach (LevelObject l in _levels)
        {
            _levelOptions.Add(l.LevelName);
        }
        
    }
}