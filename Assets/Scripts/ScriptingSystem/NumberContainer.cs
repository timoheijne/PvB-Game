using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberContainer : MonoBehaviour
{
    private protected float Value = 0;

    public virtual float GetValue()
    {
        return Value;
    }
}
