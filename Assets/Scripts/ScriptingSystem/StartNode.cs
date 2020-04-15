using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNode : MonoBehaviour
{
    public Node next = null;
    
    private void Start()
    {
        next.Go();
    }
}
