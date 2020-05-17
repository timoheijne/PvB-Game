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

    [SerializeField]
    private Transform _doctor;
    
    private void Start()
    {
        TutorialSystem.Instance.OnDeactivate += OnDeactivate;
        _tutorialUi.OnSectionChange += OnSectionChange;
        
        _gameManager.OnFinished += OnFinished;
        
        TutorialSystem.Instance.ActivateTutorial("basic-ui");
    }

    private void OnFinished()
    {
        Debug.Log(TutorialSystem.Instance.ActiveTutorial);
        if (TutorialSystem.Instance.ActiveTutorial.TutorialID == "first-program" 
            && _doctor.position.z > 13 &&
            _doctor.position.z < 14)
        {
            // Correct. Move to next section.
            Debug.Log("Done");
            _tutorialUi.NextSection();
        }
    }

    private void OnSectionChange(TutorialObject.TutorialSection _section, TutorialObject _object)
    {
        //throw new NotImplementedException();
    }

    private void OnDeactivate(TutorialObject _tutorialObject)
    {
        if (_tutorialObject.TutorialID == "basic-ui")
        {
            TutorialSystem.Instance.ActivateTutorial("first-program");
        }
    }
}