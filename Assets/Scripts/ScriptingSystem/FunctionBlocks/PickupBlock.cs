using System.Collections;
using UnityEngine;

public class PickupBlock : FunctionBlock
{
    private ActorObject _actorObject;
    
    private void Start()
    {
        // TODO: Refactor to not need to lookup object, This needs to be set dynamically via the visual scripting UI
        _actorObject = GameObject.FindWithTag("PawnObject").GetComponent<ActorObject>();
    }
    
    public override IEnumerator Act()
    {
        InteractableObject _interactableObject = _actorObject.ObjectInteractor.FindInteractableObject();
        if (_interactableObject == null)
        {
            yield break;
        }

        if (_interactableObject.HasPatient())
        {
            _actorObject.CarryObject.StartCarry(_interactableObject.TakePatient());
        }
        
        yield return new WaitForSeconds(_timeout);
        
        // TODO: Create notification if object does not contain patient. Make visual scripting fail.

        yield return 0;
    }
}