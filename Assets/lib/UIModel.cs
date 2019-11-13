using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class UIModel : NotifyPropertyChanged
{
    public Action OnLoadDataCompleted;
}