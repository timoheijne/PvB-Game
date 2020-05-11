using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlock : FunctionBlock
{
    [SerializeField] private Movement movement;
    [SerializeField] private bool right;

    private void Start()
    {
        // TODO: Refactor to not need to lookup object, This needs to be set dynamically via the visual scripting UI
        movement = GameObject.FindWithTag("PawnObject").GetComponent<Movement>();
    }

    
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
