using System.Collections;
using UnityEngine;

public class InteractBlock : FunctionBlock
{
    private ActorObject _actorObject;
    private void Start()
    {
        // TODO: Refactor to not need to lookup object, This needs to be set dynamically via the visual scripting UI
        _actorObject = GameObject.FindWithTag("PawnObject").GetComponent<ActorObject>();
    }
    
    public override IEnumerator Act()
    {
        _actorObject.Animator.SetTrigger("Interact");
        
        yield return new WaitUntillAnimationIsPlaying(_actorObject.Animator, 0, "Interact");
        
        _actorObject.ObjectInteractor.InteractWithObject();
        
        yield return new WaitUntillAnimationIsFinished(_actorObject.Animator, 0, "Interact");
        
        yield return 0;
    }
}