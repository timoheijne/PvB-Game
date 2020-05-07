using System;
using UnityEngine;

public class TutorialUIController : MonoBehaviour
{
    [SerializeField]
    private Transform _objectHighlight;
    
    private void Start()
    {
        TutorialSystem.Instance.OnActive += OnActive;
        TutorialSystem.Instance.OnDeactivate += OnDeactivate;
        
        HighlightObject("DoctorRef");
    }

    private void OnDeactivate()
    {
        // TODO: Clean UI and hide object highlighters
        throw new NotImplementedException();
    }

    private void OnActive(TutorialObject obj)
    {
        throw new NotImplementedException();
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
                rectTransform.sizeDelta = referenceObject.GetComponent<RectTransform>().sizeDelta;
            }
        }
    }
}