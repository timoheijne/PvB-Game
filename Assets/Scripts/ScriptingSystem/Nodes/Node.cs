using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

abstract public class Node : MonoBehaviour
{
    public Node next;
    public Node previous;

    public Vector2 UiOffset => _UIOffset;
    
    [FormerlySerializedAs("yOffset")] [SerializeField]
    private Vector2 _UIOffset;

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
            next.MoveVertical(-GetComponent<RectTransform>().rect.height + _UIOffset.y);
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
        RectTransform transform = GetComponent<RectTransform>();
        transform.position = (Vector2)transform.position + Vector2.down * amount;

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
        node.GetComponent<RectTransform>().position = (Vector2)GetComponent<RectTransform>().position + (Vector2.down * GetComponent<RectTransform>().rect.height) + _UIOffset;
    }

    public abstract IEnumerator Act();

    public Node NextNode()
    {
        return next;
    }
}
