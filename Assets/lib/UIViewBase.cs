using UnityEngine;

public class UIViewBase : MonoBehaviour
{
    public GameObject ViewParent;
    public GameObject LoadingAnimation;
    public bool IsViewLoaded;
    public bool IsDataLoadedFromServer;

    public Animate OpenViewAnimation;
    public Animate CloseViewAnimation;
    public virtual void Awake()
    {
        ViewParent.SetActive( false );


    }

    public virtual void RegisterDependency()
    { }

    public virtual void SetupView( object dataObject = null )
    { }

    public virtual void ShowView()
    {
        AnimationManager.Instance.AddAnimation(OpenViewAnimation.AnimationType, ViewParent, true, OpenViewAnimation.animationSettings);
        ViewParent.SetActive( true );
    }

    public virtual void HideView()
    {
        AnimationManager.Instance.AddAnimation(OpenViewAnimation.AnimationType, ViewParent, true, OpenViewAnimation.animationSettings);
        gameObject.SetActive( false );
    }

    public virtual void CloseView()
    {
        ViewsManager.Instance.CloseOnTopOfStack();
    }
}