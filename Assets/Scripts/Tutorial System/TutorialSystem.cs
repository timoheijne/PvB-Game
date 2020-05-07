using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{

    public static TutorialSystem Instance => _instance;
    private static TutorialSystem _instance;

    public event Action<TutorialObject> OnActive;
    public event Action OnDeactivate;

    public TutorialObject ActiveTutorial => _activeTutorial;
    private TutorialObject _activeTutorial;
    private List<TutorialObject> _tutorialObjects = new List<TutorialObject>();

    private void Start()
    {
        if (_instance == null)
        {
            LoadTutorials();
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    

    public void ActivateTutorial(string _tutorialID)
    {
        TutorialObject _object = _tutorialObjects.Find(o => o.TutorialID == _tutorialID);
        if (_object != null)
        {
            _activeTutorial = _object;
            OnActive?.Invoke(_activeTutorial);
        }
        else
        {
            throw new ArgumentNullException("Tutorial Not Found");
        }
    }

    public void DeactivateTutorial()
    {
        _activeTutorial = null;
        OnDeactivate?.Invoke();
    }

    private void LoadTutorials()
    {
        _tutorialObjects = Resources.LoadAll<TutorialObject>("Tutorials").ToList();

        foreach (TutorialObject _object in _tutorialObjects)
        {
            if (_object.isEnabled == false)
            {
                _tutorialObjects.Remove(_object);
            }
        }
    }
}