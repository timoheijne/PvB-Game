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
}