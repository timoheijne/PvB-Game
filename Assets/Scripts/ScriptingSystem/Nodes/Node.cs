using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node next = null;
    public Node previous = null;

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

    public void InsertNode(Node node ,float nodeHeight)
    {
        Snap(node);

        if (next != null)
        {
            next.MoveVertical(nodeHeight);
            next.previous = node;
        }

        node.next = next;
        node.previous = this;
        next = node;
    }

    public void RemoveNode()
    {
        if (next != null)
        {
            next.previous = previous;
            next.MoveVertical(-100);
        }
        if (previous != null)
        {
            previous.next = next;
        }
        next = null;
        previous = null;
    }

    public bool HasHead()
    {
        if (previous == null)
        {
            return GetComponent<Head>() != null;
        }
        else
        {
            return previous.HasHead();
        }
    }

    public bool IsHead()
    {
        return GetComponent<Head>() != null;
    }

    private void MoveVertical(float amount)
    {
        RectTransform temp = GetComponent<RectTransform>();
        temp.position = (Vector2)temp.position + Vector2.down * amount;

        if (next != null)
        {
            next.MoveVertical(amount);
        }
    }

    public bool IsColliding(Vector2 colliderPosition)
    {
        return ((Vector2)GetComponent<RectTransform>().position - colliderPosition).magnitude < 50;
    }

    private void Snap(Node node)
    {
        node.GetComponent<RectTransform>().position = (Vector2)GetComponent<RectTransform>().position + Vector2.down * 100;
    }

    public void Act(){}

    public Node NextNode()
    {
        return next;
    }
}
