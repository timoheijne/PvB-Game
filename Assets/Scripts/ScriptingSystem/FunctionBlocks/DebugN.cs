using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugN : FunctionBlock
{
    [SerializeField] private int number = 0;

    private void Awake()
    {
        Debug.Log("awake");
    }

    public override void Act()
    {
        Debug.Log(number);
    }
}

