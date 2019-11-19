using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManagerCommand : Command
{
    private MonoBehaviour _monoBehaviourToRunCoroutine;
    private List<GameObject> _listOfManagers;

    public LoadManagerCommand( MonoBehaviour monoBehaviourClass, List<GameObject> listOfManagers )
    {
        _monoBehaviourToRunCoroutine = monoBehaviourClass;
        _listOfManagers = listOfManagers;
    }

    public override void Execute( Action onComplete )
    {
        _monoBehaviourToRunCoroutine.StartCoroutine( InitAllManagers() );
    }

    private IEnumerator InitAllManagers()
    {
        foreach( var item in _listOfManagers )
        {
            var someManager = GameObject.Instantiate( item );
            var someManagerScript = someManager.GetComponent<IManagers>();
            someManagerScript.Initialize();
            yield return new WaitUntil( () => someManagerScript.IsReady );
        }

        IsFinished = true;
    }
}
