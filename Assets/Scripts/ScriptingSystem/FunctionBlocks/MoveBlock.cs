using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : FunctionBlock
{
    [SerializeField] private Movement movement;
    
    public override void Act()
    {
        if (movement.CanMoveForward())
        {
            movement.Forward();
        }
    }
}
