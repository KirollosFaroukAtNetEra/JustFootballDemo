using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManagerCommand : Command
{
    MonoBehaviour _monoBehavourToRunCoroutine;
    List<GameObject> _listOfManagers;
    public LoadManagerCommand(MonoBehaviour MonoBehaviourClass, List<GameObject> listOfManagers)
    {
        _monoBehavourToRunCoroutine = MonoBehaviourClass;
        _listOfManagers = listOfManagers;
    }


    public override void Execute(Action OnComplete)
    {
        _monoBehavourToRunCoroutine.StartCoroutine(InitAllManagers());
    }
    IEnumerator InitAllManagers()
    {
        foreach (var item in _listOfManagers)
        {
            var SomeManager = GameObject.Instantiate(item);
            var SomeManagerScript = SomeManager.GetComponent<IManagers>();
            SomeManagerScript.Initialize();
            yield return new WaitUntil(() => SomeManagerScript.IsReady);
        }
        IsFinished = true;
    }

}
