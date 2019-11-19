using System;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    Shake,
    FadeIn,
    FadeOut,
    ScaleIn,
    ScaleOut,
    SplashScene,
    Transition,
    MoveTopDown,
    MoveDown,
    MoveLeft,
    MoveRight,
    None
}

public class AnimationManager : BaseManager<AnimationManager>
{
    private Dictionary<GameObject, List<AnimationHandler>> AnimationList =
        new Dictionary<GameObject, List<AnimationHandler>>();
    public Animator TransitionAnimator;
    public Animator SplashAnimationGO;

    public override void Initialize()
    {
        IsReady = true;
    }

    public void AddAnimation( AnimationType animationType,
        GameObject referenceGameObject,
        bool resetToOriginal = true,
        ScriptableObject animationSettings = null,
        Action onComplete = null )
    {
        if( animationType == AnimationType.None )
        {
            onComplete?.Invoke();
            return;
        }

        if( !AnimationList.ContainsKey( referenceGameObject ) )
            AnimationList.Add( referenceGameObject, new List<AnimationHandler>() );

        var existsAnimationHandler = AnimationList[ referenceGameObject ].Find( x => x.AnimationType == animationType );
        if( existsAnimationHandler != null )
        {
            StopAnimation( existsAnimationHandler.ReferenceObject, existsAnimationHandler.AnimationType );
            return;
        }

        AnimationHandler animationHandler = new AnimationHandler();
        animationHandler.AnimationType = animationType;
        animationHandler.ReferenceObject = referenceGameObject;
        animationHandler.ResetToOriginal = resetToOriginal;
        animationHandler.OnComplete = onComplete;
        AnimationList[ referenceGameObject ].Add( animationHandler );

        if( animationSettings != null )
            animationHandler.AnimationSettings = animationSettings;

        animationHandler.Start();
    }

    public void StopAnimation( GameObject refrenceGameObject, AnimationType animationType )
    {
        if( !AnimationList.ContainsKey( refrenceGameObject ) )
            return;

        var animationHandlerIndex =
            AnimationList[ refrenceGameObject ].FindIndex( x => x.AnimationType == animationType );

        if( animationHandlerIndex == -1 )
            return;

        AnimationList[ refrenceGameObject ][ animationHandlerIndex ]?.Stop();
        AnimationList[ refrenceGameObject ].RemoveAt( animationHandlerIndex );
    }

    public void TransitionAnimation( bool start )
    {
        TransitionAnimator.SetBool( "IsOpen", start );
    }

    public void SplashAnimation()
    {
        SplashAnimationGO.SetBool( "IsOpen", true );
    }

    public void StopSplashAnimation()
    {
        SplashAnimationGO.gameObject.SetActive( false );
    }
}