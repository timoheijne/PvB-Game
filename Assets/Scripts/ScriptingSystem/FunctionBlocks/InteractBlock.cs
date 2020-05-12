using System.Collections;
using UnityEngine;

public class InteractBlock : FunctionBlock
{
    [SerializeField] private ObjectInteractor _interactor;

    private void Start()
    {
        // TODO: Refactor to not need to lookup object, This needs to be set dynamically via the visual scripting UI
        _interactor = GameObject.FindWithTag("PawnObject").GetComponent<ObjectInteractor>();
    }
    
    public override IEnumerator Act()
    {
        _interactor.InteractWithObject();
        yield return 0;
    }
}