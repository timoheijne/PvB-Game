using System;
using UnityEngine;

public class ResetPatientSystemHook : ResetBehaviour
{
    public override void InvokeReset()
    {
        PatientSystem.Instance?.ActivePatient?.ResetObjectives();
    }
}