using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : FunctionBlock
{
    [SerializeField] private Movement movement;
    
    public override IEnumerator Act()
    {
        if (movement.CanMoveForward())
        {
            yield return StartCoroutine(movement.Forward());
        }
        
        yield return 0;
    }
}
