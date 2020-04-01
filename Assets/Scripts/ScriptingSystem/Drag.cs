using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField] ConnectTypes connectType;

    Slot[] slots;

    Slot mySlot = null;

    private void Start()
    {
        slots = (Slot[])FindObjectsOfType(typeof(Slot));
    }

    private void OnMouseDown()
    {
        if (mySlot != null)
        {
            mySlot.Disconnect(transform);
            mySlot = null;
        }
    }

    private void OnMouseDrag()
    {
        //drag
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].connectType == connectType && Vector3.Distance(slots[i].GetConnectLocation(), transform.position) < 1)
            {
                transform.position = Vector3.Lerp(transform.position, slots[i].GetConnectLocation(), 0.5f);
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        //attach
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].connectType == connectType && Vector3.Distance(slots[i].GetConnectLocation(), transform.position) < 1)
            {
                slots[i].Connect(transform);
                mySlot = slots[i];
            }
        }
    }
}
