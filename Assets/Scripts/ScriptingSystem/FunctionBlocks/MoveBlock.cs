using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : FunctionBlock
{
    private ActorObject _actorObject;

    private void Start()
    {
        // TODO: Refactor to not need to lookup object, This needs to be set dynamically via the visual scripting UI
        _actorObject = GameObject.FindWithTag("PawnObject").GetComponent<ActorObject>();
    }

    public override IEnumerator Act()
    {
        if (_actorObject.Movement.CanMoveForward())
        {
            yield return StartCoroutine(_actorObject.Movement.Forward());
        }
        
        yield return new WaitForSeconds(_timeout);
        
        yield return 0;
    }
}
