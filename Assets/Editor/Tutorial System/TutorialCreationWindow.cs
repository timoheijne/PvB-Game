using System.Collections.Generic;
using System.Linq;
using Shared;
using UnityEditor;
using UnityEngine;

public class TutorialCreationWindow : EditorWindow
{
    private string _tutorialName;
    private string _tutorialSectionName;
    
    private Vector2 scrollPosition = Vector2.zero;

    private TutorialObject _selectedObject;
    
    [MenuItem("Window/Tutorial Editor")]
    private static void ShowWindow()
    {
        TutorialCreationWindow window = GetWindow<TutorialCreationWindow>();
        window.titleContent = new GUIContent("Tutorial Editor");
        window.minSize = new Vector2(500, 750);
        window.maxSize = new Vector2(500, 750);
        window.Show();
    }

    // Step 2: Show tutorials and add edit button.
    // Step 3: Change panel to tutorial editor.

    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);
        
        if (_selectedObject != null)
        {
            ShowEditorPanel();
        }
        else
        {
            ShowTutorialList();
        }
        
        GUILayout.EndScrollView();
    }

    private void ShowTutorialList()
    {
        // Show Creation Button
        _tutorialName = EditorGUILayout.TextField("Tutorial Name", _tutorialName);
        if (GUILayout.Button("Create new tutorial"))
        {
            // TODO: Switch to editor panel

            TutorialObject _tutorialObject = ScriptableObject.CreateInstance<TutorialObject>();
            _tutorialObject.TutorialName = _tutorialName;
            AssetDatabase.CreateAsset(_tutorialObject, $"Assets/Resources/Tutorials/{_tutorialName}.asset");
            AssetDatabase.SaveAssets ();
        }
        
        GuiLine();
        EditorGUILayout.Space(15);
        
        // List Tutorials
        EditorGUILayout.LabelField("List of tutorials:");
        GuiLine();
        
        TutorialObject[] _tutorials = LoadTutorials();

        foreach (TutorialObject _tutorial in _tutorials)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(_tutorial.TutorialName);
            if (GUILayout.Button("Edit"))
            {
                _selectedObject = _tutorial;
            }
            
            EditorGUILayout.EndHorizontal();
            GuiLine();
        }
    }

    private void ShowEditorPanel()
    {
        EditorGUILayout.LabelField("Tutorial Data");
        GuiLine();
        EditorGUILayout.Space(10);
        
        _selectedObject.TutorialName = EditorGUILayout.TextField("Tutorial Name", _selectedObject.TutorialName);
        _selectedObject.TutorialID = EditorGUILayout.TextField("Tutorial ID", _selectedObject.TutorialID);
        _selectedObject.isEnabled = EditorGUILayout.Toggle("Is Enabled", _selectedObject.isEnabled);
        
        
        
        EditorGUILayout.Space(10);
        GuiLine();
        EditorGUILayout.LabelField("Tutorial Sections");
        GuiLine();
        EditorGUILayout.Space(10);

        if (_selectedObject.TutorialSections != null)
        {
            for (int i = 0; i < _selectedObject.TutorialSections.Length; i++)
            {
                ShowSectionEditor(ref _selectedObject.TutorialSections[i]);
            }
        }

        EditorGUILayout.Space(10);
        GuiLine();
        EditorGUILayout.LabelField("Create Section");
        GuiLine();
        
        _tutorialSectionName = EditorGUILayout.TextField("Section Name", _tutorialSectionName);
        if (GUILayout.Button("Create new section"))
        {
            // TODO: Switch to editor panel
            _selectedObject.TutorialSections = AddSection(_tutorialSectionName);
        }
        GuiLine();
        
        EditorGUILayout.Space(5);

        if (GUILayout.Button("Save Tutorial"))
        {
            EditorUtility.SetDirty(_selectedObject);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        if (GUILayout.Button("Back to list"))
        {
            _selectedObject = null;
        }
    }

    private void ShowSectionEditor(ref TutorialObject.TutorialSection _section)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(_section.Name);
        if (GUILayout.Button("Delete"))
        {
            _selectedObject.TutorialSections = RemoveSection(_section);
        }
        EditorGUILayout.EndHorizontal();
            
        _section.Name = EditorGUILayout.TextField("Section Name", _section.Name);
        _section.ObjectReferenceID = EditorGUILayout.TextField("Object Reference ID", _section.ObjectReferenceID);
        _section.PanelPosition = (Position)EditorGUILayout.EnumPopup("Panel Position", _section.PanelPosition);
        _section.DoctorOnLeft = EditorGUILayout.Toggle("Doctor on left", _section.DoctorOnLeft);
        EditorGUILayout.LabelField("Tutorial Body:");
        _section.Body = EditorGUILayout.TextArea(_section.Body);

        GuiLine();
    }

    private TutorialObject.TutorialSection[] AddSection(string _name)
    {
        List<TutorialObject.TutorialSection> _sections = _selectedObject.TutorialSections.ToList();
        TutorialObject.TutorialSection _newSection = new TutorialObject.TutorialSection();
        _newSection.Name = _name;
        _sections.Add(_newSection);
        
        return _sections.ToArray();
    }

    private TutorialObject.TutorialSection[] RemoveSection(TutorialObject.TutorialSection section)
    {
        List<TutorialObject.TutorialSection> _sections = _selectedObject.TutorialSections.ToList();
        _sections.Remove(section);
        return _sections.ToArray();
    }

    private TutorialObject[] LoadTutorials()
    {
        return Resources.LoadAll<TutorialObject>("Tutorials");
    }
    
    private void GuiLine(int i_height = 1)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, i_height );
        rect.height = i_height;
        EditorGUI.DrawRect(rect, new Color ( 0.5f,0.5f,0.5f, 1 ) );

    }
}