using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugN : FunctionBlock
{
    [SerializeField] private int number;

    private void Awake()
    {
        Debug.Log("awake");
    }

    public override void Act()
    {
        Debug.Log(number);
    }
}

