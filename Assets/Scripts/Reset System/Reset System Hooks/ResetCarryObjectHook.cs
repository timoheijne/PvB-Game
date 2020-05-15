using System;
using UnityEngine;

public class ResetCarryObjectHook : ResetBehaviour
{
    private CarryObject _carryObject;

    private void Start()
    {
        _carryObject = GetComponent<CarryObject>();

        if (_carryObject == null)
        {
            Debug.LogError("CarryObject not found on GameObject, Destroying");
            Destroy(this);
        }
    }

    public override void InvokeReset()
    {
        _carryObject?.StopCarry();
    }
}