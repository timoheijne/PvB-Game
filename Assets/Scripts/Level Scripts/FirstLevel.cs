using System;
using UnityEngine;


// This script is for controlling the tutorials in the first level
// Preferably this would've been made dynamically with scriptable objects and such
// However, due to time limitations, This will be done the ugly but fast way.

public class FirstLevel : MonoBehaviour
{
    private void Start()
    {
        TutorialSystem.Instance.ActivateTutorial("basic-ui");
    }
}