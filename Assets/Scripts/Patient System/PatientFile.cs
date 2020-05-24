using System;
using System.Linq;
using Shared;
using UnityEngine;
using Object = System.Object;

[UnityEngine.CreateAssetMenu(fileName = "PATIENTFILE", menuName = "Objects/Patient File", order = 0)]
public class PatientFile : UnityEngine.ScriptableObject
{
    public string PatientName;
    public int Age;
    public GameObject Prefab;
    public Difficulty Difficulty  = Difficulty.Beginner;
    public bool Active = true;
    
    public Objective[] Objectives;

    public GameObject GameObject
    {
        get => _gameObject;
        set => _gameObject = value;
    }
    private GameObject _gameObject;

    public void ResetObjectives() 
    {
        foreach (Objective objective in Objectives)
        {
            objective.IsDone = false;
        }
    }

    public bool IsDone()
    {
        foreach (Objective objective in Objectives)
        {
            if (objective.IsDone == false && objective.Active)
            {
                return false;
            }
        }

        return true;
    }

    public Tuple<bool, Objective> MarkObjectiveDone(string ObjectiveName)
    {
        Objective objective = Objectives.FirstOrDefault(o => o.InternalName == ObjectiveName);
        if (objective != null)
        {
            objective.IsDone = true;
            return new Tuple<bool, Objective>(true, objective);
        }

        return new Tuple<bool, Objective>(false, objective);
    }
    
}