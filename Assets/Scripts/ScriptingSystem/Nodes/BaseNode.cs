using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNode : MonoBehaviour
{
    public BaseNode next = null;

    public FunctionBlock functionBlock = null;

    public BaseNode GetNode(int nodeNumber)
    {
        if (nodeNumber > 0)
        {
            if (next == null)
            {
                return null;
            }
            return next.GetNode(nodeNumber - 1);
        }
        return this;
    }

    public BaseNode GetLastNode()
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

    public void InsertNode(BaseNode node, float nodeHeight)
    {
        Snap(node);

        if (next != null)
        {
            next.MoveVertical(nodeHeight);
        }

        node.next = next;
        next = node;
    }

    public void RemoveNode()
    {
        Debug.Log("missing function");
        return;
    }

    public bool HasHead()
    {
        Debug.Log("missing function");
        return false;
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

    private void Snap(BaseNode node)
    {
        node.GetComponent<RectTransform>().position = (Vector2)GetComponent<RectTransform>().position + Vector2.down * 100;
    }
}
