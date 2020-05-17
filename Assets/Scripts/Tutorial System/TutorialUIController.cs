using System;
using Shared;
using TMPro;
using UnityEngine;

public class TutorialUIController : MonoBehaviour
{
    [SerializeField]
    private Transform _objectHighlight;

    [SerializeField]
    private GameObject _tutorialWindow;
    
    [SerializeField]
    private GameObject _doctorObject;
    
    [SerializeField]
    private GameObject _doctorLeftWaypoint;
    
    [SerializeField]
    private GameObject _doctorRightWaypoint;
    
    [SerializeField]
    private GameObject _nextButton;
    
    [SerializeField]
    private GameObject _prevButton;
    
    [SerializeField]
    private TextMeshProUGUI _textObject;

    private int _activeSection;
    
    private void Start()
    {
        TutorialSystem.Instance.OnActive += OnActive;
        TutorialSystem.Instance.OnDeactivate += OnDeactivate;
    }

    private void OnDeactivate()
    {
        // TODO: Clean UI and hide object highlighters
        _tutorialWindow.SetActive(false);
        _doctorObject.SetActive(false);
        _objectHighlight.gameObject.SetActive(false);
    }

    private void OnActive(TutorialObject obj)
    {
        _activeSection = 0;
        _tutorialWindow.SetActive(true);
        _doctorObject.SetActive(true);
        
        UpdateSection();
    }

    private void UpdateSection()
    {  
        TutorialObject _activeTutorial = TutorialSystem.Instance.ActiveTutorial;
        if (_activeTutorial.TutorialSections.Length > _activeSection)
        {
            TutorialObject.TutorialSection _section = _activeTutorial.TutorialSections[_activeSection];
            
            // Reposition Tutorial window
            UpdateWindowPosition(_section.PanelPosition);
            
            // Within bounds
            _prevButton.SetActive(_activeSection != 0);
            _nextButton.SetActive(_activeSection < _activeTutorial.TutorialSections.Length - 1);

            _textObject.text = _section.Body;
            
            Vector3 doctorScale = _doctorObject.transform.localScale;
            
            if (_section.DoctorOnLeft)
            {
                doctorScale.x = -1;
                _doctorObject.transform.position = _doctorLeftWaypoint.transform.position;
            }
            else
            {
                _doctorObject.transform.position = _doctorRightWaypoint.transform.position;
                doctorScale.x = 1;
            }
            
            _doctorObject.transform.localScale = doctorScale;
            
            // TODO: SET HIGHLIGHT
            if (_section.ObjectReferenceID != String.Empty)
            {
                HighlightObject(_section.ObjectReferenceID);
            }
            else
            {
                _objectHighlight.gameObject.SetActive(false);
            }
        } 
        else if (_activeTutorial.TutorialSections.Length == 0)
        {
            Debug.LogError("Tutorial has no sections... Closing");
            TutorialSystem.Instance.DeactivateTutorial();
        }
        else
        {
            throw new IndexOutOfRangeException();
        }
    }

    private void UpdateWindowPosition(Position _position)
    {
        RectTransform _windowTransfrom = _tutorialWindow.GetComponent<RectTransform>();
        
        // This is assuming that the tutorial window's anchor is center screen!
        switch (_position)
        {
            case Position.Center:
                _windowTransfrom.localPosition = new Vector3(0,0, 0);
                break;
            case Position.Left:
                Debug.Log("Yas");
                _windowTransfrom.localPosition = new Vector3(-Screen.width/2 + _windowTransfrom.rect.width/2 + 20, 0, 0);
                break;
            case Position.Top:
                _windowTransfrom.localPosition = new Vector3(0, Screen.height/2 - _windowTransfrom.rect.height/2 - 20, 0);
                break;
            case Position.Right:
                _windowTransfrom.localPosition = new Vector3(Screen.width/2 - _windowTransfrom.rect.width/2 - 20, 0, 0);
                break;
            case Position.Bottom:
                _windowTransfrom.localPosition = new Vector3(0, -Screen.height/2 + _windowTransfrom.rect.height/2 + 20, 0);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_position), _position, null);
        }
    }

    public void NextSection()
    {
        TutorialObject _activeTutorial = TutorialSystem.Instance.ActiveTutorial;
        _activeSection += 1;
        if (_activeTutorial.TutorialSections.Length <= _activeSection)
        {
            TutorialSystem.Instance.DeactivateTutorial();
        }
        else
        {
            UpdateSection();
        }
    }

    public void PreviousSection()
    {
        _activeSection -= 1;
        if (_activeSection < 0)
        {
            _activeSection = 0;
        }
        
        UpdateSection();
    }

    private void HighlightObject(string ObjectReference)
    {
        TutorialReferenceObject[] referenceObjects = UnityEngine.Object.FindObjectsOfType<TutorialReferenceObject>();
        foreach (TutorialReferenceObject referenceObject in referenceObjects)
        {
            if (referenceObject.ReferenceID == ObjectReference)
            {
                // Move highlight to this
                _objectHighlight.gameObject.SetActive(true);
                _objectHighlight.position = referenceObject.transform.position;
                RectTransform rectTransform = _objectHighlight.GetComponent<RectTransform>();
                Vector2 _sizeDelta = new Vector2(referenceObject.GetComponent<RectTransform>().rect.width, referenceObject.GetComponent<RectTransform>().rect.height);
                _sizeDelta += new Vector2(50, 40);
                
                rectTransform.sizeDelta = _sizeDelta;
            }
        }
    }
}