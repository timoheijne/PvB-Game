using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlock : FunctionBlock
{
    [SerializeField] private Movement movement;
    [SerializeField] private bool right;

    public override IEnumerator Act()
    {
        if (right)
        {
            yield return StartCoroutine(movement.Right());
        }
        else
        {
            yield return StartCoroutine(movement.Left());
        }

        yield return 0;
    }
}
