using System;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    private GameObject _childPanel;

    private void Start()
    {
        _childPanel = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        _childPanel.SetActive(!_childPanel.activeInHierarchy);
        
        if (_childPanel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void QuitLevel()
    {
        LevelSystem.Instance.ChangeToMainMenu();
    }

    public void RestartLevel()
    {
        LevelSystem.Instance.ChangeLevel(LevelSystem.Instance.ActiveLevel);
    }
}