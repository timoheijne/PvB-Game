using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public delegate void OnConnect(NumberContainer numberContainer);
    OnConnect onConnect;

    public ConnectTypes connectType;

    Vector3 connectLocationOffset = Vector3.zero;

    public void Connect(Transform self)
    {
        self.transform.SetParent(transform);
        self.transform.localPosition = connectLocationOffset;
        onConnect(self.GetComponent<NumberContainer>());
        Debug.Log("c");
    }

    public void Disconnect(Transform self)
    {
        self.transform.SetParent(null);
        onConnect(null);
        Debug.Log("Disc");
    }

    public Vector3 GetConnectLocation()
    {
        return transform.position + connectLocationOffset;
    }

    public void Set(OnConnect f, ConnectTypes connect, Vector3 offset)
    {
        onConnect = f;
        connectType = connect;
        connectLocationOffset = offset;
    }
}
