using System;
using UnityEngine;


// This script is for controlling the tutorials in the first level
// Preferably this would've been made dynamically with scriptable objects and such
// However, due to time limitations, This will be done the ugly but fast way.

public class FirstLevel : MonoBehaviour
{
    [SerializeField]
    private TutorialUIController _tutorialUi;

    [SerializeField]
    private GameManager _gameManager;
    
    private void Start()
    {
        TutorialSystem.Instance.OnDeactivate += OnDeactivate;
        _tutorialUi.OnSectionChange += OnSectionChange;
        
        TutorialSystem.Instance.ActivateTutorial("basic-ui");
    }

    private void OnSectionChange(TutorialObject.TutorialSection _section, TutorialObject _object)
    {
        //throw new NotImplementedException();
    }

    private void OnDeactivate()
    {
        throw new NotImplementedException();
    }
}