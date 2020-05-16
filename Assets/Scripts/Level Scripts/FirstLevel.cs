using System;
using UnityEngine;

public class FirstLevel : MonoBehaviour
{
    private void Start()
    {
        TutorialSystem.Instance.ActivateTutorial("basic-ui");
    }
}