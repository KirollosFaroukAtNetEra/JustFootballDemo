using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField]
    List<GameObject> ManagersInGame = new List<GameObject>();
    public GameObject animationManager;
    [SerializeField]
    Queue<Command> commands = new Queue<Command>();
    public override void Awake()
    {
        base.Awake();
        Command animationManagerCommand = new LoadManagerCommand(this, new List<GameObject> { animationManager });
        commands.Enqueue(animationManagerCommand);
        Command spalshAnimation = new TransitionAnimationCommand(this,AnimationType.SplashScene, false);
        commands.Enqueue(spalshAnimation);
        Command transitionAnimation = new TransitionAnimationCommand(this, AnimationType.Transition, true);
        commands.Enqueue(transitionAnimation);
        Command managersCommand = new LoadManagerCommand(this, ManagersInGame);
        commands.Enqueue(managersCommand);
        Command loadSceneCommand = new LoadSceneCOmmand(this, ScenesType.MainScene);
        commands.Enqueue(loadSceneCommand);
        Command loadMainViewCommand = new LoadViewCommand(this, ViewType.HomeView);
        commands.Enqueue(loadMainViewCommand);
        Command transitionAnimationClose = new TransitionAnimationCommand(this, AnimationType.Transition, false);
        commands.Enqueue(transitionAnimationClose);
        StartLoadGame();
    }

    public void StartLoadGame()
    {
        StartCoroutine(InitAllManagers());
    }
    IEnumerator InitAllManagers()
    {
        while (commands.Count > 0)
        {
            Command commandToExecute =  commands.Dequeue();
            commandToExecute.Execute();
            yield return new WaitUntil(() => commandToExecute.IsFinished);
        }
    }
}
