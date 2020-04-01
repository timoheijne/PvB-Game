using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sum : NumberContainer
{
    NumberContainer numberInA = null;
    NumberContainer numberInB = null;

    readonly ConnectTypes inConnection = ConnectTypes.Number;

    private void Awake()
    {
        gameObject.AddComponent<Slot>().Set(SetNumberContainerA, inConnection, new Vector3(0, -1.5f, 0));
        gameObject.AddComponent<Slot>().Set(SetNumberContainerB, inConnection, new Vector3(0, 1.5f, 0));
    }

    public void SetNumberContainerA(NumberContainer numberContainer)
    {
        numberInA = numberContainer;
    }

    public void SetNumberContainerB(NumberContainer numberContainer)
    {
        numberInB = numberContainer;
    }

    public override float GetValue()
    {
        if (numberInA != null && numberInB != null)
        {
            return numberInA.GetValue() + numberInB.GetValue();
        }
        else
        {
            return 0;
        }
    }
}
