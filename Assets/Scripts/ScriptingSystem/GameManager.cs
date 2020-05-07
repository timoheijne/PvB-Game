using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    private bool paused = true;
    private float time = 0;
    [SerializeField] private float TickTimeInSeconds = 1;
    private int tick = 0;


    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        ResetNodeHolders();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            time += Time.deltaTime;
        }
        if (time >= TickTimeInSeconds)
        {
            time -= TickTimeInSeconds;
            Tick();
            tick += 1;
        }
    }

    private void Tick()
    {
        for (int i = 0; i < nodeHolders.Count; i++)
        {
            if(nodeHolders[i].currentNode == null)
            {
                nodeHolders[i].currentNode = nodeHolders[i].head;
            }
            nodeHolders[i].currentNode.Act();
            nodeHolders[i].currentNode = nodeHolders[i].currentNode.NextNode();
        }
    }

    public void Pause(bool input)
    {
        paused = input;
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

