using System;
using UnityEngine;

[Serializable]
public class Animate
{
    public AnimationType AnimationType;
    public ScriptableObject animationSettings;
}

public class AnimationHandler
{
    private AnimationBehaviour animationBehaviour;

    public AnimationType AnimationType;
    public GameObject ReferenceObject;
    public ScriptableObject AnimationSettings;
    public Vector3 OriginalPosition;
    public bool ResetToOriginal;
    public Action OnComplete;

    public void Start()
    {
        OriginalPosition = ReferenceObject.transform.position;
        animationBehaviour =
            ReferenceObject.AddComponent( AnimationFactory.MakeAnimation( AnimationType ) ) as AnimationBehaviour;

        if( AnimationSettings != null )
            animationBehaviour.AnimationSettings = AnimationSettings;

        animationBehaviour.SetupSettings();
        animationBehaviour.OnComplete = OnComplete;
        animationBehaviour.StartAnimate();
    }

    public void Stop()
    {
        ReferenceObject.GetComponent<AnimationBehaviour>().StopAnimate();
        if( ResetToOriginal )
        {
            ReferenceObject.transform.position = OriginalPosition;
        }
    }
}