using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewBase : MonoBehaviour
{
    public GameObject ViewParent;
    public GameObject LoadingAnimation;
    public bool isViewLoaded;
    public bool isDataLoadedFromServer;
    public AnimationType AnimationOnOpen;
    public AnimationType AnimationOnClose;
    public ScriptableObject OpenAnimationSettings;
    public virtual void Awake()
    {
        ViewParent.SetActive(false);
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
        AnimationManager.Instance.AddAnimation(AnimationOnOpen, ViewParent,false, OpenAnimationSettings);
        ViewParent.SetActive(true);
    }
    public virtual void HideView()
    {
        gameObject.SetActive(false);
    }
    public virtual void CloseView()
    {
        ViewsManager.Instance.CloseOnTopOfStack();
    }
}
