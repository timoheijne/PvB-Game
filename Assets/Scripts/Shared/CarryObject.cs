using System;
using UnityEngine;

public class CarryObject : MonoBehaviour
{
    [SerializeField, Tooltip("The object the target should follow, leave empty for the current transform")]
    private Transform _targetMaster;
    private Transform _object;

    public bool IsCarrying()
    {
        return _object != null;
    }

    public void StartCarry(Transform _target)
    {
        if (_target == null)
        {
            throw new ArgumentNullException("Carrier");
        }
        
        _object = _target;
        
        _object.parent = (_targetMaster != null) ? _targetMaster : transform;
        _object.position = (_targetMaster != null) ? _targetMaster.position : transform.position;
    }

    public Transform StopCarry()
    {
        _object.parent = null;
        
        Transform _target = _object;
        _object = null;

        return _target;
    }
}