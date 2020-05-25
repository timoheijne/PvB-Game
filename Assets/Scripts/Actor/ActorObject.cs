using System;
using UnityEngine;

public class ActorObject : MonoBehaviour
{
    public Movement Movement => _movement;
    public CarryObject CarryObject => _carryObject;
    public ObjectInteractor ObjectInteractor => _objectInteractor;
    public Animator Animator => _animator;
    
    private Movement _movement;
    private CarryObject _carryObject;
    private ObjectInteractor _objectInteractor;
    private Animator _animator;
    
    private void Start()
    {
        _movement = GetComponent<Movement>();
        _carryObject = GetComponent<CarryObject>();
        _objectInteractor = GetComponent<ObjectInteractor>();
        _animator = GetComponent<Animator>();
    }
}