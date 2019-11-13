using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAnimationCommand : Command
{
    MonoBehaviour _monoBehavourToRunCoroutine;
    bool _open;
    AnimationType _animationType;
    public TransitionAnimationCommand(MonoBehaviour MonoBehaviourClass,AnimationType animationType,bool open)
    {
        _monoBehavourToRunCoroutine = MonoBehaviourClass;
        _open = open;
        _animationType = animationType;
    }
    public void UpdateCommand(AnimationType animationType, bool open)
    {
        IsFinished = false;
        _open = open;
        _animationType = animationType;
        _monoBehavourToRunCoroutine.StopCoroutine(startAnimation());
    }
    public override void Execute(Action OnComplete)
    {
        IsFinished = false;
        _monoBehavourToRunCoroutine.StartCoroutine(startAnimation());
    }
    IEnumerator startAnimation()
    {
        if (_animationType == AnimationType.SplashScene)
        {
            AnimationManager.Instance.SplashAnimation();
            yield return new WaitForSeconds(0.5f);
        }
        if (_animationType == AnimationType.Transition)
        {
            AnimationManager.Instance.TransitionAnimation(_open);
        }
        yield return new WaitForSeconds(1f);
        IsFinished = true;
    }
}
