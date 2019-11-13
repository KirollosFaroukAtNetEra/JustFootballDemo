using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadViewCommand : Command
{
    MonoBehaviour _monoBehavourToRunCoroutine;
    ViewType _viewType;
    public LoadViewCommand(MonoBehaviour MonoBehaviourClass, ViewType viewType)
    {
        _monoBehavourToRunCoroutine = MonoBehaviourClass;
        _viewType = viewType;
    }


    public override void Execute(Action OnComplete)
    {
        LoadView();
    }
    void LoadView()
    {
        ViewsManager.Instance.OpenView(_viewType,null,()=> { IsFinished = true; });
    }
     
}
