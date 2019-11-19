using UnityEngine;

public class MoveAnimation : AnimationBehaviour
{
    private float _timeStartedLerping;
    private MoveAnimationSettings _moveAnimationSettings;

    public override void SetupSettings()
    {
        base.SetupSettings();
        _moveAnimationSettings = AnimationSettings as MoveAnimationSettings;
        _timeStartedLerping = Time.time;
    }

    private void FixedUpdate()
    {
        if( AllowAnimate )
        {
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / _moveAnimationSettings.timeToReachTarget;

            transform.localPosition = Vector3.Lerp( _moveAnimationSettings.startPosition,
                _moveAnimationSettings.target,
                percentageComplete );

            if( percentageComplete >= 1.0f )
            {
                AllowAnimate = false;
                OnComplete?.Invoke();
            }
        }
    }
}
