using UnityEngine;

public class UIViewBase : MonoBehaviour
{
    public GameObject ViewParent;
    public GameObject LoadingAnimation;
    public bool IsViewLoaded;
    public bool IsDisabled;
    public bool IsDataLoadedFromServer;

    public Animate OpenViewAnimation;
    public Animate CloseViewAnimation;

    public virtual void Awake()
    {
        ViewParent.SetActive( false );
        LoadingAnimation.SetActive( true );
    }

    public virtual void RegisterDependency()
    { }

    public virtual void SetupView( object dataObject = null )
    { }

    public virtual void ShowView()
    {
        ViewParent.transform.parent.gameObject.SetActive( true );

        AnimationManager.Instance.AddAnimation( OpenViewAnimation.AnimationType,
            ViewParent,
            false,
            OpenViewAnimation.animationSettings,
            () =>
            {
                AnimationManager.Instance.StopAnimation( ViewParent, OpenViewAnimation.AnimationType );
                IsViewLoaded = true;
                IsDisabled = false;
            } );
        ViewParent.SetActive( true );
    }

    public virtual void HideView()
    {
        AnimationManager.Instance.AddAnimation( CloseViewAnimation.AnimationType,
            ViewParent,
            false,
            CloseViewAnimation.animationSettings,
            () =>
            {
                AnimationManager.Instance.StopAnimation( ViewParent, CloseViewAnimation.AnimationType );
                gameObject.SetActive( false );
                IsViewLoaded = false;
                IsDisabled = true;
            } );
    }

    public virtual void CloseView()
    {
        AnimationManager.Instance.AddAnimation( CloseViewAnimation.AnimationType,
            ViewParent,
            false,
            CloseViewAnimation.animationSettings,
            () =>
            {
                AnimationManager.Instance.StopAnimation( ViewParent, CloseViewAnimation.AnimationType );
                ViewsManager.Instance.CloseOnTopOfStack();
                IsViewLoaded = false;
                IsDisabled = true;
            } );

    }
}