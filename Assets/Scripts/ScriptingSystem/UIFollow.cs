using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{
    [SerializeField] GameObject objToFollow = null;

    void OnGUI()
    {
        transform.position = Camera.main.WorldToScreenPoint(objToFollow.transform.position);
    }
}
