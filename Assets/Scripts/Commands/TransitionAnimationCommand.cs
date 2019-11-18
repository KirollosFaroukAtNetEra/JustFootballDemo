using System;
using System.Collections;
using UnityEngine;

public class TransitionAnimationCommand : Command
{
    private MonoBehaviour _monoBehaviourToRunCoroutine;
    private bool _open;
    private AnimationType _animationType;

    public TransitionAnimationCommand( MonoBehaviour monoBehaviourClass, AnimationType animationType, bool open )
    {
        _monoBehaviourToRunCoroutine = monoBehaviourClass;
        _open = open;
        _animationType = animationType;
    }

    public void UpdateCommand( AnimationType animationType, bool open )
    {
        IsFinished = false;
        _open = open;
        _animationType = animationType;
        _monoBehaviourToRunCoroutine.StopCoroutine( startAnimation() );
    }

    public override void Execute( Action onComplete )
    {
        IsFinished = false;
        _monoBehaviourToRunCoroutine.StartCoroutine( startAnimation() );
    }

    private IEnumerator startAnimation()
    {
        if( _animationType == AnimationType.SplashScene )
        {
            AnimationManager.Instance.SplashAnimation();
            yield return new WaitForSeconds( 0.5f );
        }

        if( _animationType == AnimationType.Transition )
        {
            AnimationManager.Instance.TransitionAnimation( _open );
        }

        yield return new WaitForSeconds( 1f );
        IsFinished = true;
    }
}