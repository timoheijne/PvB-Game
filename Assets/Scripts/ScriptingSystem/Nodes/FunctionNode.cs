﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionNode : Node
{
    [SerializeField] private FunctionBlock functionBlock;

    override public IEnumerator Act()
    {
        yield return StartCoroutine(functionBlock.Act());
        
        yield return 0;
    }
}
