using UnityEngine;

public class MoveTopDown : AnimationBehaviour
{
    float time;
    MoveAnimationSettings moveAnimationSettings;
    void Awake()
    {
        moveAnimationSettings = AnimationSettings as MoveAnimationSettings;
    }
    void Update()
    {
        time += Time.deltaTime / moveAnimationSettings.timeToReachTarget;
        transform.localPosition = Vector3.Lerp(moveAnimationSettings.startPosition, moveAnimationSettings.target, time);
    }
}
