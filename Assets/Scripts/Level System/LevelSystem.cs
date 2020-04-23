using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    public string MainMenuScene => _mainMenuScene;

        [SerializeField, Tooltip("The name of the main menu scene")]
    private string _mainMenuScene;
    private bool _returnToLevelSelect;
    private List<LevelObject> _levels;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void LoadAllLevels()
    {
        _levels = Resources.LoadAll<LevelObject>("Levels").ToList();
    }

    public void ChangeToMainMenu(bool _showLevelSelect = false)
    {
        if (MainMenuScene == string.Empty)
        {
            throw new ArgumentNullException("MainMenuScene");
        }
        
        _returnToLevelSelect = _showLevelSelect;
        SceneManager.LoadScene(MainMenuScene);
    }

    public void ChangeLevel(string _levelName)
    {
        LevelObject _level = _levels.Find(l => l.LevelName == _levelName);
        if (_level == null)
        {
            return;
        }
       
        SceneManager.LoadScene(_level.SceneName);
    }
    
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == MainMenuScene && _returnToLevelSelect)
        {
            _returnToLevelSelect = false;
            // TODO: Show level select.
        }
        
        throw new NotImplementedException();
    }
}