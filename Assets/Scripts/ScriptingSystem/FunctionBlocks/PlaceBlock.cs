using System.Collections;
using UnityEngine;

public class PlaceBlock : FunctionBlock
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

        if (_actorObject.CarryObject.IsCarrying())
        {
            Patient _target = _actorObject.CarryObject.StopCarry().GetComponent<Patient>();
            if (_target != null)
            {
                _interactableObject.PlacePatient(_target);
            }
        }
        
        yield return new WaitForSeconds(_timeout);
        
        // TODO: Create notification if not carrying patient. Make visual scripting fail.

        yield return 0;
    }
}