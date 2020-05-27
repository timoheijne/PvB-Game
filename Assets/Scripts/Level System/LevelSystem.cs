using System;
using System.Collections;
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

    public bool ReturnToLevelSelect => _returnToLevelSelect;
    
    private bool _returnToLevelSelect;
    private List<LevelObject> _levels;

    public LevelObject ActiveLevel => _activeLevel;
    private LevelObject _activeLevel;

    [SerializeField] private GameObject _loadingScreen;

    // Decrease this value for "Load time optimizations"
    [SerializeField] private float _minLoadTime = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadAllLevels();

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
        StartCoroutine(LoadScene(MainMenuScene));
    }

    public bool ChangeLevel(string _levelName)
    {
        LevelObject _level = GetLevel(_levelName);
        if (_level == null)
        {
            return false;
        }

        return ChangeLevel(_level);
    }

    public bool ChangeLevel(LevelObject _level)
    {
        if (_level.IsEnabled == false)
        {
            return false;
        }

        StartCoroutine(LoadScene(_level.SceneName));
        return true;
    }
    
    public void SwitchToCustomScene(string _levelName)
    {
        StartCoroutine(LoadScene(_levelName));
    }

    private IEnumerator LoadScene(string _sceneName)
    {
        float _startLoad = Time.time;

        // If there is no loading screen, There is no need to async scene switching!
        if (_loadingScreen == null)
        {
            SceneManager.LoadScene(_sceneName);
            yield break;
        }
        
        _loadingScreen.gameObject.SetActive(true);
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        float _endLoad = Time.time;
        if (_endLoad - _startLoad < _minLoadTime)
        {
            yield return new WaitForSecondsRealtime(_minLoadTime - (_endLoad - _startLoad));
        }
        

        _loadingScreen.gameObject.SetActive(false);
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

    public List<LevelObject> GetAllLevels(bool _mustBeEnabled = true)
    {
        return _levels.FindAll(l => l.IsEnabled = _mustBeEnabled);
    }
}