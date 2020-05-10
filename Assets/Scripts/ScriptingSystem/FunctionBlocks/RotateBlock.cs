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
            movement.Right();
        }
        else
        {
            movement.Left();
        }

        yield return 0;
    }
}
