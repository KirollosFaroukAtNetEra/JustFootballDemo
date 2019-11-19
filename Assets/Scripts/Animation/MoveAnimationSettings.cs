using UnityEngine;

[CreateAssetMenu(fileName ="Move", menuName ="Animation Settings/Move Animation")]
public class MoveAnimationSettings : ScriptableObject
{
    public AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 5.0f, 5.0f);
    public Vector3 startPosition;
    public Vector3 target;
    public float timeToReachTarget;
}
