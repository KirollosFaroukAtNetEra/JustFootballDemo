using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBehaviour : MonoBehaviour
{
    public virtual void StartAnimate()
    {

    }
    public virtual void StopAnimate()
    {
        Destroy(this);
    }
}
