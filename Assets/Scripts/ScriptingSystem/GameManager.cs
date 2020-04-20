using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<Head> heads = new List<Head>();
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

    public void AddHead(Head headToAdd)
    {
        heads.Add(headToAdd);
    }

    public void Pause(bool input)
    {
        paused = input;
    }
}
