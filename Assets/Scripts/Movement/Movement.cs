using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private float _walkSpeed = 1;
    
    public bool CanMoveForward()
    {
        return !Physics.BoxCast(transform.position - transform.forward*0.9f, Vector3.one * 0.45f, transform.forward, transform.rotation, 1.9f);
    }

    public IEnumerator Forward()
    {
        // Start walk animation
        // Start moving
        transform.localPosition += transform.forward;

        yield return new WaitForSeconds(5);
        // Stop walk animation
        
        yield return 0;
    }

    public void Left()
    {
        transform.Rotate(Vector3.up * -90);
    }

    public void Right()
    {
        transform.Rotate(Vector3.up * 90);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.color = Color.green;
        //Draw a cube in front of this gameobject
        Gizmos.DrawWireCube(transform.position + transform.forward, Vector3.one * 0.9f);
    }
}
