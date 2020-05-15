using System;
using UnityEngine;

public class ResetTransformHook : ResetBehaviour
{
    private Vector3 _originalPostion;
    private Quaternion _originalRotation;
    
    private void Start()
    {
        _originalPostion = transform.position;
        _originalRotation = transform.rotation;
    }

    public override void InvokeReset()
    {
        transform.SetPositionAndRotation(_originalPostion, _originalRotation); 
    }
}