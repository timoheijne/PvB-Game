using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : Node
{
    public bool condition = true;

    new public Node NextNode()
    {
        return condition ? next : this;
    }
}
