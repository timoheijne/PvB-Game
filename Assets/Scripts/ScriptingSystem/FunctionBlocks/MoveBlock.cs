using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : FunctionBlock
{
    [SerializeField] private Movement movement;

    private void Start()
    {
        // TODO: Refactor to not need to lookup object, This needs to be set dynamically via the visual scripting UI
        movement = GameObject.FindWithTag("PawnObject").GetComponent<Movement>();
    }

    public override IEnumerator Act()
    {
        if (movement.CanMoveForward())
        {
            yield return StartCoroutine(movement.Forward());
        }
        
        yield return 0;
    }
}
