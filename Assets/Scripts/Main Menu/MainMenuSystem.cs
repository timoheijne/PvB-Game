using System;
using UnityEngine;

public class MainMenuSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject _startMenu;
    
    [SerializeField]
    private GameObject _levelSelect;

    private void Start()
    {
        if (LevelSystem.Instance != null && LevelSystem.Instance.ReturnToLevelSelect == true)
        {
            OpenLevelSelect();
        }
    }

    private void OnEnable()
    {
        if (LevelSystem.Instance != null && LevelSystem.Instance.ReturnToLevelSelect == true)
        {
            OpenLevelSelect();
        }
    }

    public void OnClickPlay()
    {
        // CHECK IF CHARACTER HAS BEEN CREATED
        if (!PlayerPrefs.HasKey("hasCharacter") || PlayerPrefs.GetInt("hasCharacter") == 0)
        {
            LevelSystem.Instance.SwitchToCustomScene("CharacterCustomization");
        }
        else
        {
            OpenLevelSelect();
        } 
    }

    public void OpenLevelSelect()
    {
        _startMenu.SetActive(false);
        _levelSelect.SetActive(true);
    }
}