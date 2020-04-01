using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugNumber : FunctionBlock
{
    [SerializeField] NumberContainer numberIn = null;

    readonly ConnectTypes inConnection = ConnectTypes.Number;
    
    private void Awake()
    {
        gameObject.AddComponent<Slot>().Set(SetNumberContainer, inConnection, new Vector3(1.0f, 0, 0));
    }

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

