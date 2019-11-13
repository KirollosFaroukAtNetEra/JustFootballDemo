using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIController<M> where M : UIModel
{
    protected M Model;
    public virtual void Setup(M model,object dataObject = null)
    {
        Model = model;
        Model.NotifyOnPropertyChanged();
    }
    public virtual void RetryLoadData()
    {
    }
    protected virtual void Close()
    {

    }
}