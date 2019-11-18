using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Move",menuName ="Animation Settings/Move Animation")]
public class MoveAnimationSettings : ScriptableObject
{
    public Vector3 startPosition;
    public Vector3 target;
    public float timeToReachTarget;
}
