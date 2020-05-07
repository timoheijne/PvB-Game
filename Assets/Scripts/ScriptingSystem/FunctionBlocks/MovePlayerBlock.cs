using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerBlock : FunctionBlock
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 movement = Vector3.zero;
    private Vector3 moveFrom;
    private Vector3 moveTo;
    private float start;


    public override void Act()
    {
        if (player != null)
        {
            moveFrom = player.localPosition;
            moveTo = player.localPosition + movement;
            start = Time.time;
        }
    }

    private void Update()
    {
        if (player != null && start != -1)
        {
            player.localPosition = Vector3.Lerp(moveFrom,moveTo,Mathf.Min(Time.time-start,1));
            if (Mathf.Min(Time.time - start, 1) == 1)
            {
                start = -1;
            }
        }
    }
}
