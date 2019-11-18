using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ViewData
{
    public ViewType Type;
    public GameObject ViewObject;
}

public enum ViewType
{
    HomeView,
    CardsView,
    ClubsView,
    ProfileView
}

public enum ScenesType
{
    SplashScene,
    MainScene,
}

public class ViewsManager : BaseManager<ViewsManager>
{
    private List<UIViewBase> _viewsStack;
    private Command _transitionViewsAnimationIn;
    private Command _transitionViewsAnimationOut;
    private bool _isWaitingForLoading = false;

    public GameObject AlertGameObject;
    public List<ViewData> ViewsObjectsList;

    public override void Initialize()
    {
        _viewsStack = new List<UIViewBase>();
        _transitionViewsAnimationIn = new TransitionAnimationCommand( this, AnimationType.Transition, true );
        _transitionViewsAnimationOut = new TransitionAnimationCommand( this, AnimationType.Transition, false );
        IsReady = true;
    }

    private void Update()
    {
        if( Input.GetKeyDown( KeyCode.Escape ) )
        {
            CloseOnTopOfStack();
        }
    }

    public void ShowAlert( string alertMessage )
    {
        StartCoroutine( SetupAlert( alertMessage ) );
    }

    private IEnumerator SetupAlert( string alertMessage )
    {
        var alertObject = Instantiate( AlertGameObject, _viewsStack[ _viewsStack.Count - 1 ].transform );
        AnimationManager.Instance.AddAnimation( AnimationType.ScaleIn, alertObject );
        alertObject.GetComponent<AlertMessage>().SetAlertMessage( alertMessage );
        yield return new WaitForSeconds( 1.5f );
        Destroy( alertObject );
    }

    public void OpenView( ViewType viewType, object dataObject = null, Action OnComplete = null )
    {
        if( _isWaitingForLoading )
        {
            return;
        }

        StartCoroutine( LoadView( viewType, dataObject, OnComplete ) );
    }

    IEnumerator LoadView( ViewType viewType, object dataObject = null, Action OnComplete = null )
    {
        _isWaitingForLoading = true;
        _transitionViewsAnimationIn.Execute( null );
        yield return new WaitUntil( () => _transitionViewsAnimationIn.IsFinished );

        DisableOnTopOfStack();
        var viewObject = Instantiate( ViewsObjectsList.First( view => view.Type == viewType ).ViewObject );
        var viewToOpen = viewObject.GetComponent<UIViewBase>();
        viewToOpen.SetupView( dataObject );
        _viewsStack.Add( viewToOpen );
        yield return new WaitUntil( () => viewToOpen.isViewLoaded );

        OnComplete?.Invoke();

        _transitionViewsAnimationOut.Execute( null );
        yield return new WaitUntil( () => _transitionViewsAnimationOut.IsFinished );
        _isWaitingForLoading = false;
    }

    private void DisableOnTopOfStack()
    {
        if( _viewsStack.Count == 0 )
        {
            return;
        }

        _viewsStack[ _viewsStack.Count - 1 ].gameObject.SetActive( false );
    }

    public void CloseOnTopOfStack()
    {
        if( _isWaitingForLoading )
        {
            return;
        }

        StartCoroutine( CloseViewOnTopOfStack() );
    }

    private IEnumerator CloseViewOnTopOfStack()
    {
        _isWaitingForLoading = true;
        if( _viewsStack.Count == 1 )
        {
            yield break;
        }

        _transitionViewsAnimationIn.Execute( null );
        yield return new WaitUntil( () => _transitionViewsAnimationIn.IsFinished );

        var viewToClose = _viewsStack[ _viewsStack.Count - 1 ];
        _viewsStack.Remove( viewToClose );
        Destroy( viewToClose.gameObject );

        EnableOnTopOfStack();

        _transitionViewsAnimationOut.Execute( null );
        yield return new WaitUntil( () => _transitionViewsAnimationOut.IsFinished );
        _isWaitingForLoading = false;
    }

    private void EnableOnTopOfStack()
    {
        _viewsStack[ _viewsStack.Count - 1 ].gameObject.SetActive( true );
    }
}