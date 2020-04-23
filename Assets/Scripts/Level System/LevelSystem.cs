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

    public LevelObject ActiveLevel => _activeLevel;
    private LevelObject _activeLevel;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        LoadAllLevels();

        _activeLevel = _levels.Find(l => l.SceneName == SceneManager.GetActiveScene().name);
    }

    public LevelObject GetLevel(string _levelName)
    {
        return _levels.Find(l => l.LevelName == _levelName);
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
        LevelObject _level = GetLevel(_levelName);
        if (_level == null)
        {
            return;
        }

        _activeLevel = _level;
        SceneManager.LoadScene(_level.SceneName);
    }
    
    public void ChangeLevel(LevelObject _level)
    {
        if (_level.IsEnabled == false)
        {
            return;
        }
        
        _activeLevel = _level;
        SceneManager.LoadScene(_level.SceneName);
    }
    
    private void LoadAllLevels()
    {
        _levels = Resources.LoadAll<LevelObject>("Levels").ToList();
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