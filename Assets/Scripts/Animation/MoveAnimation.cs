using UnityEngine;

public class MoveAnimation : AnimationBehaviour
{
    private float _timeStartedLerping;
    private MoveAnimationSettings moveAnimationSettings;

    public override void SetupSettings()
    {
        base.SetupSettings();
        moveAnimationSettings = AnimationSettings as MoveAnimationSettings;
        _timeStartedLerping = Time.time;
    }
    
    private void FixedUpdate()
    {
        if (AllowAnimate)
        {
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / moveAnimationSettings.timeToReachTarget;

            transform.localPosition = Vector3.Lerp(moveAnimationSettings.startPosition, moveAnimationSettings.target, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                AllowAnimate = false;
                onComplete?.Invoke();
            }
        }
    }
}
