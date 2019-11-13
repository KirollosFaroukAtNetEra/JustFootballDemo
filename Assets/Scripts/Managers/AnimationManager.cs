using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum AnimationType
{
    Shake,
    FadeIn,
    FadeOut,
    ScaleIn,
    ScaleOut,
    SplashScene,
    Transition,
    None
}
public class AnimationManager : BaseManager<AnimationManager>
{
    private Dictionary<GameObject, List<AnimationHandler>> AnimationList = new Dictionary<GameObject, List<AnimationHandler>>();
    public Animator transitionAnimator;
    public Animator SplashAnimationGO;



    public override void Initialize()
    {
        IsReady = true;
    }

    public void AddAnimation(AnimationType animationType,GameObject refrenceGameObject,bool resetToOriginal = true)
    {
        if (!AnimationList.ContainsKey(refrenceGameObject))
            AnimationList.Add(refrenceGameObject, new List<AnimationHandler>());

        var existsAnimationHandler = AnimationList[refrenceGameObject].Find(x => x.AnimationType == animationType);
        if (existsAnimationHandler != null)
            return;
        AnimationHandler animationHandler = new AnimationHandler();
        animationHandler.AnimationType = animationType;
        animationHandler.RefrenceObject = refrenceGameObject;
        animationHandler.resetToOriginal = resetToOriginal;
        AnimationList[refrenceGameObject].Add(animationHandler);
        animationHandler.Start();
        
    }

    public void StopAnimation( GameObject refrenceGameObject, AnimationType animationType)
    {
        if (!AnimationList.ContainsKey(refrenceGameObject))
            return;
        var animationHandlerIndex = AnimationList[refrenceGameObject].FindIndex(x => x.AnimationType == animationType);
        if(animationHandlerIndex == -1)
            return;
        AnimationList[refrenceGameObject][animationHandlerIndex].Stop();
        AnimationList[refrenceGameObject].RemoveAt(animationHandlerIndex);
    }
    public void TransitionAnimation(bool start)
    {
        transitionAnimator.SetBool("IsOpen", start);
    }
    public void SplashAnimation()
    {
        SplashAnimationGO.SetBool("IsOpen", true);
    }
    public void StopSplashAnimation()
    {
       SplashAnimationGO.gameObject.SetActive( false );
    }
}