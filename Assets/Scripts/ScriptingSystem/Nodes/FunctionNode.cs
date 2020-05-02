using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionNode : Node
{
    FunctionBlock functionBlock;

    new public void Act()
    {
        functionBlock.Act();
    }
}
