using UnityEngine;

public class Patient : MonoBehaviour
{
    public PatientFile PatientFile
    {
        get => _patientFile;
        set => _patientFile = value;
    }
    
    [SerializeField]
    private PatientFile _patientFile;
}