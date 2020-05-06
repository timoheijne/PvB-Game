using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionNode : Node
{
    [SerializeField] private FunctionBlock functionBlock;

    new public void Act()
    {
        functionBlock.Act();
    }
}
