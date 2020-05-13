using System.Collections;
using UnityEngine;

public class PickupBlock : FunctionBlock
{
    [SerializeField] private ObjectInteractor _interactor;
    [SerializeField] private CarryObject _carryObject;
    
    private void Start()
    {
        // TODO: Refactor to not need to lookup object, This needs to be set dynamically via the visual scripting UI
        GameObject _pawn = GameObject.FindWithTag("PawnObject");
        _interactor = _pawn.GetComponent<ObjectInteractor>();
        _carryObject = _pawn.GetComponent<CarryObject>();
    }
    
    public override IEnumerator Act()
    {
        InteractableObject _interactableObject = _interactor.FindInteractableObject();
        if (_interactableObject == null)
        {
            yield break;
        }

        if (_interactableObject.HasPatient())
        {
            _carryObject.StartCarry(_interactableObject.TakePatient());
        }
        
        yield return new WaitForSeconds(_timeout);
        
        // TODO: Create notification if object does not contain patient. Make visual scripting fail.

        yield return 0;
    }
}