using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField]
    List<GameObject> ManagersInGame = new List<GameObject>();
    public GameObject animationManager;
    Queue<Command> commands = new Queue<Command>();
    public override void Awake()
    {
        base.Awake();
        Command animationManagerCommand = new LoadManagerCommand(this, new List<GameObject> { animationManager });
        Command spalshAnimation = new TransitionAnimationCommand(this,AnimationType.SplashScene, false);
        Command transitionAnimation = new TransitionAnimationCommand(this, AnimationType.Transition, true);
        Command managersCommand = new LoadManagerCommand(this, ManagersInGame);
        Command loadSceneCommand = new LoadSceneCOmmand(this, ScenesType.MainScene);
        Command loadMainViewCommand = new LoadViewCommand(this, ViewType.HomeView);
        Command transitionAnimationClose = new TransitionAnimationCommand(this, AnimationType.Transition, false);
        commands.Enqueue(animationManagerCommand);
        commands.Enqueue(spalshAnimation);
        commands.Enqueue(transitionAnimation);
        commands.Enqueue(managersCommand);
        commands.Enqueue(loadSceneCommand);
        commands.Enqueue(loadMainViewCommand);
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
