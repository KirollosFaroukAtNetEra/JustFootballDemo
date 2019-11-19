using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField]
    private List<GameObject> _managersInGame = new List<GameObject>();
    private Queue<Command> _commands = new Queue<Command>();

    public GameObject AnimationManager;

    public override void Awake()
    {
        base.Awake();

        Command animationManagerCommand = new LoadManagerCommand( this, new List<GameObject> { AnimationManager } );
        Command splashAnimation = new TransitionAnimationCommand( this, AnimationType.SplashScene, false );
        Command transitionAnimation = new TransitionAnimationCommand( this, AnimationType.Transition, true );
        Command managersCommand = new LoadManagerCommand( this, _managersInGame );
        Command loadSceneCommand = new LoadSceneCommand( this, ScenesType.MainScene );
        Command loadMainViewCommand = new LoadViewCommand( this, ViewType.HomeView );
        Command transitionAnimationClose = new TransitionAnimationCommand( this, AnimationType.Transition, false );

        _commands.Enqueue(animationManagerCommand);
        _commands.Enqueue(splashAnimation);
        _commands.Enqueue(transitionAnimation);
        _commands.Enqueue(managersCommand);
        _commands.Enqueue(loadSceneCommand);
        _commands.Enqueue(loadMainViewCommand);
        _commands.Enqueue( transitionAnimationClose );

        StartLoadGame();
    }

    public void StartLoadGame()
    {
        StartCoroutine( InitAllManagers() );
    }

    private IEnumerator InitAllManagers()
    {
        while( _commands.Count > 0 )
        {
            Command commandToExecute = _commands.Dequeue();
            commandToExecute.Execute();
            yield return new WaitUntil( () => commandToExecute.IsFinished );
        }
    }
}
