using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler
{
    public AnimationType AnimationType;
    AnimationBehaviour animationBehaviour;
    public GameObject RefrenceObject;
    public Vector3 OriginalPosition;
    public bool resetToOriginal;
    public void Start()
    {
        OriginalPosition = RefrenceObject.transform.position;
        animationBehaviour = AnimationFactory.MakeAnimation(AnimationType);
        RefrenceObject.AddComponent(animationBehaviour.GetType());
    }
    public void Stop()
    {
        RefrenceObject.GetComponent<AnimationBehaviour>().StopAnimate();
        if (resetToOriginal)
        {
            RefrenceObject.transform.position = OriginalPosition;
        }
    }
    public void Reset()
    {

    }
}
