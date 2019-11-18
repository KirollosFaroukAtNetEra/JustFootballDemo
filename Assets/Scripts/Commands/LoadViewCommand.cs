using System;
using UnityEngine;

public class LoadViewCommand : Command
{
    private MonoBehaviour _monoBehaviourToRunCoroutine;
    private ViewType _viewType;

    public LoadViewCommand( MonoBehaviour monoBehaviourClass, ViewType viewType )
    {
        _monoBehaviourToRunCoroutine = monoBehaviourClass;
        _viewType = viewType;
    }

    public override void Execute( Action onComplete )
    {
        LoadView();
    }

    private void LoadView()
    {
        ViewsManager.Instance.OpenView( _viewType, null, () => { IsFinished = true; } );
    }
}
