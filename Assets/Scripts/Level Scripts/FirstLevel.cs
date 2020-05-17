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
    private ActorObject _actor;

    private TutorialObject.TutorialSection _activeSection;
    
    private void Start()
    {
        TutorialSystem.Instance.OnDeactivate += OnDeactivate;
        _tutorialUi.OnSectionChange += OnSectionChange;
        
        _gameManager.OnFinished += OnFinished;
        
        PatientSystem.Instance.OnPatientDone += OnPatientDone;
        
        TutorialSystem.Instance.ActivateTutorial("basic-ui");
    }

    private void OnPatientDone(PatientFile obj)
    {
        if (TutorialSystem.Instance.ActiveTutorial.TutorialID == "first-program" 
            && _activeSection.Name == "heal-patient")
        {
            // Correct. Move to next section.
            _tutorialUi.NextSection();
        }
    }

    private void OnFinished()
    {
        if (TutorialSystem.Instance.ActiveTutorial.TutorialID == "first-program" 
            && _activeSection.Name == "move-forward"
            && _actor.transform.position.z > 13 
            && _actor.transform.position.z < 14)
        {
            // Correct. Move to next section.
            _tutorialUi.NextSection();
        }

        if (TutorialSystem.Instance.ActiveTutorial.TutorialID == "first-program" 
            && _activeSection.Name == "pickup-and-turn"
            && _actor.transform.position.z > 13 
            && _actor.transform.position.z < 14
            && (_actor.transform.rotation.eulerAngles.y == 180 || _actor.transform.rotation.eulerAngles.y == -180)
            && _actor.CarryObject.IsCarrying())
        {
            // Correct. Move to next section.
            _tutorialUi.NextSection();
        }
        
        if (TutorialSystem.Instance.ActiveTutorial.TutorialID == "first-program" 
            && _activeSection.Name == "walk-to-table"
            && _actor.transform.position.z > 8 
            && _actor.transform.position.z < 11
            && (_actor.transform.rotation.eulerAngles.y == 90)
            && _actor.CarryObject.IsCarrying())
        {
            // Correct. Move to next section.
            _tutorialUi.NextSection();
        }
    }

    private void OnSectionChange(TutorialObject.TutorialSection _section, TutorialObject _object)
    {
        _activeSection = _section;
    }

    private void OnDeactivate(TutorialObject _tutorialObject)
    {
        if (_tutorialObject.TutorialID == "basic-ui")
        {
            TutorialSystem.Instance.ActivateTutorial("first-program");
        }
    }
}