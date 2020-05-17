using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action OnStart;

    public event Action OnFinished;
    
    private class nodeHolder
    {
        public Head head;
        public Node currentNode;

        public nodeHolder(Head head)
        {
            this.head = head;
            currentNode = head;
        }
    }

    List<nodeHolder> nodeHolders = new List<nodeHolder>();
    private bool _isRunning;

    public void InitiateSequence()
    {
        if (_isRunning)
        {
            Debug.LogError("Sequence already running, wait for finish before you can run again.");
            return;
        }

        _isRunning = true;
        StartCoroutine(RunSequence());
        
    }

    private IEnumerator RunSequence()
    {
        OnStart?.Invoke();
        for (int i = 0; i < nodeHolders.Count; i++)
        {
            if (nodeHolders[i].currentNode == null)
            {
                nodeHolders[i].currentNode = nodeHolders[i].head;
            }

            bool _run = true;
            while (_run)
            {
                yield return StartCoroutine(nodeHolders[i].currentNode.Act());
                nodeHolders[i].currentNode = nodeHolders[i].currentNode.NextNode();

                if (nodeHolders[i].currentNode == null)
                {
                    _run = false;
                }
            }
        }
        _isRunning = false;
        OnFinished?.Invoke();
        
        yield return 0;
    }

    //set nodeHolders to contain all heads
    public void ResetNodeHolders()
    {
        nodeHolders = new List<nodeHolder>();
        Head[] allHeads = FindObjectsOfType<Head>();
        for (int i = 0; i < allHeads.Length; i++)
        {
            nodeHolders.Add(new nodeHolder(allHeads[i]));
        }
    }
}

