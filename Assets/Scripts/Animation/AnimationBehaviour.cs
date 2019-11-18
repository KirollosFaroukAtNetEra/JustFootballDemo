using UnityEngine;

public class AnimationBehaviour : MonoBehaviour
{
    public ScriptableObject AnimationSettings;
    public virtual void StartAnimate()
    { }

    public virtual void StopAnimate()
    {
        Destroy( this );
    }
}
