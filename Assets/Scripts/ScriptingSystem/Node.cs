using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node next = null;
    public Node previous = null;

    public FunctionBlock functionBlock = null;

    public Node GetNode(int nodeNumber)
    {
        if(nodeNumber > 0)
        {
            if (next == null)
            {
                return null;
            }
            return next.GetNode(nodeNumber - 1);
        }
        return this;
    }

    public Node GetLastNode()
    {
        if (next != null)
        {
            return GetLastNode();
        }
        return this;
    }

    public int GetNodeCount()
    {
        if (next != null)
        {
            return next.GetNodeCount() + 1;
        }
        return 1;
    }

    public void InsertNode(Node node)
    {
        node.next = next;
        next.previous = node;
        node.previous = this;
        next = node;

    }

    public void RemoveNode()
    {
        next.previous = previous;
        previous.next = next;
        next = null;
        previous = null;
    }

    public bool HasHead()
    {
        if (previous != null)
        {
            return GetComponent<Head>() != null;
        }
        else
        {
            return previous.HasHead();
        }
    }

    private float start = -1;
    public void Go()
    {
        start = Time.time;
        functionBlock.Act();
    }

    private void Update()
    {
        if (Mathf.Min(Time.time - start, 1) == 1 && start != -1 && next != null)
        {
            start = -1;
            next.Go();
        }
    }
}
