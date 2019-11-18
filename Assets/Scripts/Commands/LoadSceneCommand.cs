using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneCommand : Command
{
    private MonoBehaviour _monoBehaviourToRunCoroutine;
    private AsyncOperation _loadSceneOperation;
    private ScenesType _scenesType;

    public LoadSceneCommand( MonoBehaviour monoBehaviourClass, ScenesType scenesType )
    {
        _monoBehaviourToRunCoroutine = monoBehaviourClass;
        _scenesType = scenesType;
    }

    public override void Execute( Action onComplete )
    {
        _monoBehaviourToRunCoroutine.StartCoroutine( LoadScene() );
    }

    private IEnumerator LoadScene()
    {
        _loadSceneOperation = SceneManager.LoadSceneAsync( _scenesType.ToString() );
        yield return new WaitUntil( () => _loadSceneOperation.isDone );
        if( _scenesType == ScenesType.MainScene )
        {
            AnimationManager.Instance.StopSplashAnimation();
        }

        IsFinished = true;
    }
}
