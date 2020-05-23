using System;
using UnityEngine;

public class Patient : MonoBehaviour
{
    public PatientFile PatientFile
    {
        get => _patientFile;
        set => _patientFile = value;
    }
    
    private PatientFile _patientFile;

    public PatientParticles PatientParticles => _patientParticles;
    private PatientParticles _patientParticles;

    private void Start()
    {
        _patientParticles.GetComponent<PatientParticles>();
    }
}