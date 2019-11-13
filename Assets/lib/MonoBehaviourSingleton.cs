using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Singleton
{
    void Init();
}
public class MonoBehaviourSingleton<T> : MonoBehaviour, Singleton where T : MonoBehaviour
{
    static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null) _instance = (T)FindObjectOfType(typeof(T));
            if (_instance == null)
            {
                var newGameObject = new GameObject(typeof(T).ToString(), typeof(T));
                _instance = newGameObject.GetComponent<T>();
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        _instance = (T)FindObjectOfType(typeof(T));
        if( _instance == null ){_instance = this as T;}
        else if (_instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    public void Init()
    {
    }
}