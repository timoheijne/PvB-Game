using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem Instance;
    public string MainMenuScene => _mainMenuScene;

    public event Action<LevelObject> OnLevelLoad; 

        [SerializeField, Tooltip("The name of the main menu scene")]
    private string _mainMenuScene;
    private bool _returnToLevelSelect;
    private List<LevelObject> _levels;

    public LevelObject ActiveLevel => _activeLevel;
    private LevelObject _activeLevel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadAllLevels();
            SortLevelsByPosition();

            _activeLevel = _levels.Find(l => l.SceneName == SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(gameObject);
        }
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

        ChangeLevel(_level);
    }

    public void ChangeLevel(LevelObject _level)
    {
        if (_level.IsEnabled == false)
        {
            return;
        }

        SceneManager.LoadScene(_level.SceneName);
    }

    private void LoadAllLevels()
    {
        _levels = Resources.LoadAll<LevelObject>("Levels").ToList().FindAll(l => l.IsEnabled == true);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == MainMenuScene && _returnToLevelSelect)
        {
            _returnToLevelSelect = false;
            // TODO: Show level select.
        }
        
        Debug.Log("Level Loaded");
        _activeLevel = _levels.Find(l => l.SceneName == SceneManager.GetActiveScene().name);
        if (_activeLevel != null)
        {
            OnLevelLoad?.Invoke(_activeLevel);
        }
    }

    private List<LevelObject> GetAllLevels(bool _mustBeEnabled = true)
    {
        return _levels.FindAll(l => l.IsEnabled = _mustBeEnabled);
    }

    public void SortLevelsByPosition()
    {
        _levels.Sort((p1, p2) => p1.Position.CompareTo(p2.Position));
    }
}