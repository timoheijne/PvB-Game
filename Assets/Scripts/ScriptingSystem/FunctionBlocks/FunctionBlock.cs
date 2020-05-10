using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FunctionBlock : MonoBehaviour
{
    public abstract IEnumerator Act();
}
