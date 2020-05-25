using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlock : FunctionBlock
{
    [SerializeField] private bool right;

    private ActorObject _actorObject;
    
    private void Start()
    {
        // TODO: Refactor to not need to lookup object, This needs to be set dynamically via the visual scripting UI
        _actorObject = GameObject.FindWithTag("PawnObject").GetComponent<ActorObject>();
    }

    
    public override IEnumerator Act()
    {
        if (right)
        {
            yield return StartCoroutine(_actorObject.Movement.Right());
        }
        else
        {
            yield return StartCoroutine(_actorObject.Movement.Left());
        }

        yield return 0;
    }
}
