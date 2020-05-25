using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResetSystem : MonoBehaviour
{
    public void InvokeResets()
    {
        // This is done each time reset is being invoked because ResetBehaviours will change in run time.
        // Technically this system could be a singleton and the reset could register it self on creation.
        // But since its a low call system this way reduces clutter.
        ResetBehaviour[] _resetObjects = GameObject.FindObjectsOfType<ResetBehaviour>();
        
        foreach (ResetBehaviour _behaviour in _resetObjects)
        {
            _behaviour?.InvokeReset();
        }
    }
}