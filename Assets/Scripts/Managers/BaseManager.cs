using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManager<T> : MonoBehaviourSingleton<T>, IManagers where T : MonoBehaviour
{
    private bool _isReady;
    public bool IsReady { get => _isReady; set => _isReady = value; }
    public virtual void Initialize() { }
} 