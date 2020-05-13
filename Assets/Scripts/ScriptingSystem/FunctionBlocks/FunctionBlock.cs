using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FunctionBlock : MonoBehaviour
{
    [SerializeField, Tooltip("Seconds this block should wait before preceding to the next block, This is purely cosmetic and is not for solving a race condiction")] 
    protected float _timeout;
    
    public abstract IEnumerator Act();
}
