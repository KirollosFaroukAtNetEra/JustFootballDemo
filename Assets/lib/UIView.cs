using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class UIView : MonoBehaviour
{
    public GameObject ViewGameObject;
    public GameObject LoadingAnimation;
    public bool isLoaded;
    public bool isDataLoadedFromServer;
    protected int retryCount;
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
        ViewGameObject.SetActive(true);
    }
    public virtual void HideView()
    { 
        ViewGameObject.SetActive(false);
    }
    public virtual void CloseView()
    {
        ViewsManager.Instance.CloseOnTopOfStack();
    }
}
public abstract class UIView<M, C> : UIView
    where M : UIModel,new ()
    where C : UIController<M>, new()
{
    public M Model;
    protected C Controller;
    public override void SetupView(object dataObject=null)
    {
        Controller = new C();
        Model = Model ?? new M(); 
        RegisterDependency();
        Controller.Setup(Model, dataObject);
        ShowView();
        if (!isDataLoadedFromServer)
        {
            StartCoroutine(RetryLoadDataFromServert());
        }
    }
    public IEnumerator RetryLoadDataFromServert()
    {
        while (retryCount < 5)
        {
            yield return new WaitForSeconds(15f);
            if (isDataLoadedFromServer)
            {
                yield break;
            }
            Controller.RetryLoadData();
            retryCount++;
            Debug.Log("RetryLoadData ->"+this.name);
        }
        if (retryCount == 5)
        {
            ViewsManager.Instance.ShowAlert("Can't load data from server.");
        }
    }
    
}
