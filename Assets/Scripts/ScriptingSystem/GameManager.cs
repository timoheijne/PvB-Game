using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private class nodeHolder
    {
        Head head;
        Node currentNode;

        public nodeHolder(Head head, Node node)
        {
            this.head = head;
            currentNode = node;
        }
    }

    List<nodeHolder> heads = new List<nodeHolder>();
    private bool paused;
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
        for (int i = 0; i < heads.Count; i++)
        {
            //heads[i].
        }
    }

    public void Pause(bool input)
    {
        paused = input;
    }


}

