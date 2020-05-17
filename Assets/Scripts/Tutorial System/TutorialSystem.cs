using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{

    public static TutorialSystem Instance => _instance;
    private static TutorialSystem _instance;

    public event Action<TutorialObject> OnActive;
    public event Action<TutorialObject> OnDeactivate;

    public TutorialObject ActiveTutorial => _activeTutorial;
    private TutorialObject _activeTutorial;
    private List<TutorialObject> _tutorialObjects = new List<TutorialObject>();

    private void Awake()
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
        OnDeactivate?.Invoke(_activeTutorial);
        _activeTutorial = null;
    }

    private void LoadTutorials()
    {
        _tutorialObjects = Resources.LoadAll<TutorialObject>("Tutorials").ToList().FindAll(l => l.isEnabled == true);
    }
}