using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private float _walkSpeed = 1;
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public bool CanMoveForward()
    {
        return !Physics.BoxCast(transform.position - transform.forward*0.9f, Vector3.one * 0.45f, transform.forward, transform.rotation, 1.9f);
    }

    public IEnumerator Forward()
    {
        _animator.SetBool("Walk", true);
        
        Vector3 _startPosition = transform.position;
        Vector3 _targetPosition = transform.localPosition + transform.forward;
        
        float _startTime = Time.time;
        float _journeyLength = Vector3.Distance(_startPosition, _targetPosition);

        while (transform.position != _targetPosition)
        {
            float _distCovered = (Time.time - _startTime) * _walkSpeed;
            float _fractionOfJourney = _distCovered / _journeyLength;
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _fractionOfJourney);
            
            yield return null;
        }

        _animator.SetBool("Walk", false);

        yield return 0;
    }

    public IEnumerator Left()
    {
        _animator.SetTrigger("TurnLeft");
        
        yield return new WaitUntillAnimationIsPlaying(_animator, 0, "TurnLeft");
        
        yield return new WaitUntillAnimationIsFinished(_animator, 0, "TurnLeft");
        
        transform.Rotate(Vector3.up * -90);
        
        yield return 0;
    }

    public IEnumerator Right()
    {
        _animator.SetTrigger("TurnRight");
        
        yield return new WaitUntillAnimationIsPlaying(_animator, 0, "TurnRight");
        
        yield return new WaitUntillAnimationIsFinished(_animator, 0, "TurnRight");

        transform.Rotate(Vector3.up * 90);
        
        yield return 0;
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
