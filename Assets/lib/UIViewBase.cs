using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewBase : MonoBehaviour
{
    public GameObject ViewParent;
    public GameObject LoadingAnimation;
    public bool isViewLoaded;
    public bool isDataLoadedFromServer;

    public virtual void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        LoadingAnimation.SetActive(true);
    }
    public virtual void RegisterDependency()
    {
    }
    public virtual void SetupView(object dataObject = null)
    {
    }
    public virtual void ShowView()
    {
        ViewParent.SetActive(true);
    }
    public virtual void HideView()
    {
        ViewParent.SetActive(false);
    }
    public virtual void CloseView()
    {
        ViewsManager.Instance.CloseOnTopOfStack();
    }
}
