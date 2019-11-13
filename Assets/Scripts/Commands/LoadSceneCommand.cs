using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneCOmmand : Command
{
    MonoBehaviour _monoBehavourToRunCoroutine;
    AsyncOperation loadSceneOperation;
    ScenesType _scenesType;
    public LoadSceneCOmmand(MonoBehaviour MonoBehaviourClass, ScenesType scenesType)
    {
        _monoBehavourToRunCoroutine = MonoBehaviourClass;
        _scenesType = scenesType;
    }
    public override void Execute(Action OnComplete)
    {
        _monoBehavourToRunCoroutine.StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        loadSceneOperation = SceneManager.LoadSceneAsync(_scenesType.ToString());
        yield return new WaitUntil(() => loadSceneOperation.isDone);
        if (_scenesType == ScenesType.MainScene)
        {
            AnimationManager.Instance.StopSplashAnimation();
        }
        IsFinished = true;
    }
}
