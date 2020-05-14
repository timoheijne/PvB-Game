using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResetSystem : MonoBehaviour
{
    private List<ResetBehaviour> _resetObjects = new List<ResetBehaviour>();
    
    private void Start()
    {
        _resetObjects = GameObject.FindObjectsOfType<ResetBehaviour>().ToList();
    }

    public void InvokeResets()
    {
        foreach (ResetBehaviour _behaviour in _resetObjects)
        {
            _behaviour.InvokeReset();
        }
    }

    public void RegisterBehaviour(ResetBehaviour _behaviour)
    {
        if (_behaviour == null)
        {
            throw new ArgumentNullException("ResetBehaviour");
        }
        
        _resetObjects.Add(_behaviour);
    }

    public void DeregisterBehaviour(ResetBehaviour _behaviour)
    {
        if (_behaviour == null)
        {
            throw new ArgumentNullException("ResetBehaviour");
        }

        _resetObjects.Remove(_behaviour);
    }
}