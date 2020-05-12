using System.Collections;
using UnityEngine;

public class PickupBlock : FunctionBlock
{
    [SerializeField] private ObjectInteractor _interactor;
    [SerializeField] private CarryObject _carryObject;
    
    private void Start()
    {
        // TODO: Refactor to not need to lookup object, This needs to be set dynamically via the visual scripting UI
        _interactor = GameObject.FindWithTag("PawnObject").GetComponent<ObjectInteractor>();
    }
    
    public override IEnumerator Act()
    {
        InteractableObject _interactableObject = _interactor.FindInteractableObject();
        if (_interactableObject == null)
        {
            yield break;
        }

        yield return 0;
    }
}