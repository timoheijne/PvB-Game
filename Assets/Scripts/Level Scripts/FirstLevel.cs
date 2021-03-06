using System;
using UnityEngine;
using UnityEngine.Serialization;


// This script is for controlling the tutorials in the first level
// Preferably this would've been made dynamically with scriptable objects and such
// However, due to time limitations, This will be done the ugly but fast way.

public class FirstLevel : MonoBehaviour
{
    [FormerlySerializedAs("_tutorialUi")] [SerializeField]
    private TutorialUIController _tutorialUI;

    [SerializeField]
    private GameManager _gameManager;

    [SerializeField] 
    private ActorObject _actor;

    private TutorialObject.TutorialSection _activeSection;

    private Vector3 _mainCameraOriginalPosition;
    
    private void Start()
    {
        _mainCameraOriginalPosition = Camera.main.transform.position;
        TutorialSystem.Instance.OnDeactivate += OnDeactivate;
        _tutorialUI.OnSectionChange += OnSectionChange;
        
        _gameManager.OnFinished += OnFinished;
        
        PatientSystem.Instance.OnPatientDone += OnPatientDone;
        
        TutorialSystem.Instance.ActivateTutorial("basic-ui");
    }

    private void Update()
    {
        if (TutorialSystem.Instance.ActiveTutorial == null)
        {
            return;
        }
        
        
        if (TutorialSystem.Instance.ActiveTutorial.TutorialID == "basic-ui" 
            && _activeSection.Name == "de-camera"
            && Vector3.Distance(Camera.main.transform.position, _mainCameraOriginalPosition) > 1)
        {
            // Correct. Move to next section.
            _tutorialUI.NextSection();
        }
    }

    private void OnPatientDone(PatientFile obj)
    {
        if (TutorialSystem.Instance.ActiveTutorial.TutorialID == "first-program" 
            && _activeSection.Name == "heal-patient")
        {
            // Correct. Move to next section.
            _tutorialUI.NextSection();
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
            _tutorialUI.NextSection();
        }

        if (TutorialSystem.Instance.ActiveTutorial.TutorialID == "first-program" 
            && _activeSection.Name == "pickup-and-turn"
            && _actor.transform.position.z > 13 
            && _actor.transform.position.z < 14
            && (_actor.transform.rotation.eulerAngles.y == 180 || _actor.transform.rotation.eulerAngles.y == -180)
            && _actor.CarryObject.IsCarrying())
        {
            // Correct. Move to next section.
            _tutorialUI.NextSection();
        }
        
        if (TutorialSystem.Instance.ActiveTutorial.TutorialID == "first-program" 
            && _activeSection.Name == "walk-to-table"
            && _actor.transform.position.z > 8 
            && _actor.transform.position.z < 11
            && (_actor.transform.rotation.eulerAngles.y == 90)
            && _actor.CarryObject.IsCarrying())
        {
            // Correct. Move to next section.
            _tutorialUI.NextSection();
        }
    }

    private void OnSectionChange(TutorialObject.TutorialSection _section, TutorialObject _object)
    {
        _activeSection = _section;
        
        if (TutorialSystem.Instance.ActiveTutorial.TutorialID == "basic-ui" 
            && _activeSection.Name == "de-camera")
        {
            _mainCameraOriginalPosition = Camera.main.transform.position;
        }
    }

    private void OnDeactivate(TutorialObject _tutorialObject)
    {
        if (_tutorialObject.TutorialID == "basic-ui")
        {
            TutorialSystem.Instance.ActivateTutorial("first-program");
        }
        
        if (_tutorialObject.TutorialID == "first-program")
        {
            if (PatientSystem.Instance.GetNextPatient() == null)
            {
                // Well this is not supposed to happen. But then we just move back to main menu
                LevelSystem.Instance.ChangeToMainMenu();
                return;
            }
            
            PatientSystem.Instance.SetActivePatient(PatientSystem.Instance.GetNextPatient());
            PatientSystem.Instance.AutoContinue = true;
        }
    }
}