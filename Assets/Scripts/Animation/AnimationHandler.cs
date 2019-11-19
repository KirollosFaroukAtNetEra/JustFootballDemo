using UnityEngine;
[System.Serializable]
public class Animate
{
    public AnimationType AnimationType;
    public ScriptableObject animationSettings;

}
public class AnimationHandler
{
    public AnimationType AnimationType;
    AnimationBehaviour animationBehaviour;
    public GameObject RefrenceObject;
    public ScriptableObject animationSettings;
    public Vector3 OriginalPosition;
    public bool resetToOriginal;

    public void Start()
    {
        
        OriginalPosition = RefrenceObject.transform.position;
        animationBehaviour = RefrenceObject.AddComponent(AnimationFactory.MakeAnimation(AnimationType)) as AnimationBehaviour;
        if (animationSettings != null) animationBehaviour.AnimationSettings = animationSettings;
    }

    public void Stop()
    {
        RefrenceObject.GetComponent<AnimationBehaviour>().StopAnimate();
        if( resetToOriginal )
        {
            RefrenceObject.transform.position = OriginalPosition;
        }
    }
}