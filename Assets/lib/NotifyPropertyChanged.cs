using System;
using System.Collections;
using System.Collections.Generic;

public abstract class NotifyPropertyChanged
{
    Dictionary<string, List<Action>> propertyActionData = new Dictionary<string, List<Action>>();

    public void ListenOnPropertyChanged(string propertyKey,Action method)
    {
        if (!propertyActionData.ContainsKey(propertyKey))
            propertyActionData.Add(propertyKey, new List<Action>());
        propertyActionData[propertyKey].Add(method);
    }
    public void NotifyOnPropertyChanged(string propertyKey = "")
    {
        if (propertyKey == "")
        {
            NotifyAllPropertyChanged();
            return;
        }
        if (propertyActionData.ContainsKey(propertyKey))
        {
            for (int i = propertyActionData[propertyKey].Count-1 ; i >= 0 ; i--)
            {
                propertyActionData[propertyKey][i].Invoke();
            }
        }
    }
    private void NotifyAllPropertyChanged()
    {
        foreach (var key in propertyActionData.Keys)
        {
            for (int i = propertyActionData[key].Count-1; i >= 0; i--)
            {
                propertyActionData[key][i].Invoke();
            }
        }
    }
}
