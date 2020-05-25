using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class PatientUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _agendaPanel;
    
    [SerializeField]
    private TextMeshProUGUI _dataObject;

    [SerializeField] 
    private GameObject _objectivesHolder;
    
    [SerializeField] 
    private GameObject _objectivePrefab;
    
    [SerializeField] 
    private Sprite _emptyStar;
    
    [SerializeField]
    private Sprite _fullStar;


    private void Start()
    {
        if (PatientSystem.Instance == null)
        {
            throw new ArgumentNullException("PatientSystem");
        }
        
        PatientSystem.Instance.OnPatientChange += OnPatientChange;
        PatientSystem.Instance.OnPatientDone += OnPatientDone;
        PatientSystem.Instance.OnObjectiveComplete += OnObjectiveComplete;
    }

    private void OnObjectiveComplete(Objective objective, PatientFile patientFile)
    {
        UpdateUI(patientFile);
    }

    private void OnPatientChange(PatientFile obj)
    {
        UpdateUI(obj);
    }
    
    private void OnPatientDone(PatientFile obj)
    {
        //throw new NotImplementedException();
    }

    private void UpdateUI(PatientFile obj)
    {
        StringBuilder _data = new StringBuilder();

        _data.Append($"Naam: {obj.PatientName}\n");
        _data.Append($"Leeftijd: {obj.Age}\n");
        
        _dataObject.SetText(_data);

        DeleteCurrentObjectives();
        
        foreach (Objective _objective in obj.Objectives)
        {
            AddObjective(_objective);
        }
    }

    private void DeleteCurrentObjectives()
    {
        // Transform[] _children = _objectivesHolder.GetComponentsInChildren<Transform>();
        foreach (Transform _child in _objectivesHolder.transform)
        {
            Destroy(_child.gameObject);
        }
    }

    private void AddObjective(Objective _objective)
    {
        GameObject _objectiveGO = Instantiate(_objectivePrefab, _objectivesHolder.transform);

        Image _starImage = _objectiveGO.transform.GetComponentInChildren<Image>();
        _starImage.sprite = _emptyStar;
        if (_objective.IsDone)
        {
            _starImage.sprite = _fullStar;
        }

        TextMeshProUGUI _objectiveText = _objectiveGO.transform.GetComponentInChildren<TextMeshProUGUI>();
        _objectiveText.text = _objective.FriendlyName;
    }

    public void OpenAgenda()
    {
        UpdateUI(PatientSystem.Instance.ActivePatient);
        _agendaPanel.SetActive(true);
    }

    public void CloseAgenda()
    {
        _agendaPanel.SetActive(false);
    }
}