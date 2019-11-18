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
    public List<ViewData> ViewsObjectsList;
    private bool iswaitingForLoading = false;
    public GameObject AlertGameObject;
    public override void Initialize()
    {
        _viewsStack = new List<UIViewBase>();
        IsReady = true;
    }

    void Update()
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
        if( iswaitingForLoading )
        {
            return;
        }
        StartCoroutine( LoadView( viewType, dataObject, OnComplete ) );
    }

    IEnumerator LoadView( ViewType viewType, object dataObject = null, Action OnComplete = null )
    {
        iswaitingForLoading = true;
        DisableOnTopOfStack();
        var viewobject = Instantiate( ViewsObjectsList.FirstOrDefault( view => view.Type == viewType ).ViewObject);
        var viewToOpen = viewobject.GetComponent<UIViewBase>();
        viewToOpen.SetupView( dataObject );
        _viewsStack.Add( viewToOpen );
        yield return new WaitUntil( () => viewToOpen.isViewLoaded );
        if( OnComplete != null )
        {
            OnComplete.Invoke();
        }
        iswaitingForLoading = false;
    }

    private void DisableOnTopOfStack()
    {
        if( _viewsStack.Count == 0 )
        {
            return;
        }

        _viewsStack[_viewsStack.Count - 1].HideView();
    }

    public void CloseOnTopOfStack()
    {
        if( iswaitingForLoading )
        {
            return;
        }

        StartCoroutine( CloseViewOnTopOfStack() );
    }

    private IEnumerator CloseViewOnTopOfStack()
    {
        iswaitingForLoading = true;
        if( _viewsStack.Count == 1 )
        {
            yield break;
        }
        var viewToClose = _viewsStack[ _viewsStack.Count - 1 ];
        _viewsStack.Remove( viewToClose );
        Destroy( viewToClose.gameObject );
        EnableOnTopOfStack();
        iswaitingForLoading = false;
    }

    private void EnableOnTopOfStack()
    {
        _viewsStack[ _viewsStack.Count - 1 ].gameObject.SetActive( true );
    }
}