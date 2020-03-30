using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugNumber : FunctionBlock
{
    [SerializeField] NumberContainer numberIn = null;

    readonly ConnectTypes inConnection = ConnectTypes.Number;

    private void Update()
    {
        Act();
    }

    public override void Act()
    {
        if (numberIn != null)
        {
            Debug.Log(numberIn.GetValue());
        }
    }

    public void SetNumberContainer(NumberContainer numberContainer)
    {
        numberIn = numberContainer;

    }
}

