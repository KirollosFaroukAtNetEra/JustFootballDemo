using System;
using UnityEngine;

public class AnimationBehaviour : MonoBehaviour
{
    protected bool AllowAnimate;

    public ScriptableObject AnimationSettings;
    public Action onComplete;

    public virtual void StartAnimate()
    {
        AllowAnimate = true;
    }

    public virtual void SetupSettings()
    { }

    public virtual void StopAnimate()
    {
        AllowAnimate = false;
        Destroy( this );
    }
}
