using System;
using System.Collections;
using UnityEngine;

public class WaitUntillAnimationIsFinished : CustomYieldInstruction
{
    private readonly Animator _animator;
    private readonly int _layer;
    private readonly string _name;

    public override bool keepWaiting => _animator.GetCurrentAnimatorStateInfo(_layer).IsName(_name) &&  _animator.GetCurrentAnimatorStateInfo(_layer).normalizedTime < 1.0f;

    public WaitUntillAnimationIsFinished(Animator _animator, int _layer, string _name)
    {
        this._animator = _animator;
        this._layer = _layer;
        this._name = _name;
    }
}