using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIView<M, C> : UIViewBase
    where M : UIModel,new ()
    where C : UIController<M>, new()
{
    public M Model;
    protected C Controller;
    public override void SetupView(object dataObject=null)
    {
        base.SetupView();
        Controller = new C();
        Model = Model ?? new M(); 
        RegisterDependency();
        Controller.Setup(Model, dataObject);
        ShowView();
        isViewLoaded = true;
    }
    public override void RegisterDependency()
    {
        base.RegisterDependency();
        Model.ListenOnPropertyChanged(Model.DataLoadedObserverName, DataLoaded);
    }
    public virtual void DataLoaded()
    {

    }
}
