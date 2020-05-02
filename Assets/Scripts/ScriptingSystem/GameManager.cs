﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private class nodeHolder
    {
        Head head;
        public Node currentNode;

        public nodeHolder(Head head, Node node)
        {
            this.head = head;
            currentNode = node;
        }
    }

    List<nodeHolder> nodeHolders = new List<nodeHolder>();
    private bool paused = true;
    private float time;
    private float TickTimeInSeconds = 1;
    private int tick = 0;


    // Start is called before the first frame update
    void Start()
    {

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
            nodeHolders[i].currentNode.Act();
            nodeHolders[i].currentNode = nodeHolders[i].currentNode.NextNode();
        }
    }

    public void Pause(bool input)
    {
        paused = input;
    }
}

