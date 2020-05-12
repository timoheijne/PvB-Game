using System;
using UnityEngine;

public class CarryObject : MonoBehaviour
{
    [SerializeField, Tooltip("The object the target should follow, leave empty for the current transform")]
    private Transform _targetMaster;
    private Transform _object;
        
    private void Update()
    {
        if (_object != null)
        {
            _object.position = (_targetMaster != null) ? _targetMaster.position : transform.position;
        }
    }

    public void StartCarry(Transform _target)
    {
        if (_target == null)
        {
            throw new ArgumentNullException("Carrier");
        }

        _object = _target;
    }

    public void StopCarry()
    {
        _object = null;
    }
}