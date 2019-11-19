using System;

[Serializable]
public abstract class UIModel : NotifyPropertyChanged
{
    public Action OnLoadDataCompleted;
    public string DataLoadedObserverName = "DataLoaded";
}