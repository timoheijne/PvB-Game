using System.Linq;
using Shared;
using UnityEngine;

[UnityEngine.CreateAssetMenu(fileName = "PATIENTFILE", menuName = "Objects/Patient File", order = 0)]
public class PatientFile : UnityEngine.ScriptableObject
{
    public string PatientName;
    public int Age;
    public GameObject Prefab;
    public Difficulty Difficulty  = Difficulty.Beginner;
    public bool Active = true;
    
    public Objective[] Objectives;

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

    public void MarkObjectiveDone(string ObjectiveName)
    {
        Objective objective = Objectives.First(o => o.InternalName == ObjectiveName);
        if (objective != null)
        {
            objective.IsDone = true;
        }
    }
    
}