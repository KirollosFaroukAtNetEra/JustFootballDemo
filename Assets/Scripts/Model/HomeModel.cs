using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

[System.Serializable]
public class HomeModel : UIModel
{
    public UserData playerData;

    public HomeModel()
    {
        Events.instance.AddListener<ProfileNameUpdated>(OnProfileNameUpdated);
    }

    private void OnProfileNameUpdated(ProfileNameUpdated e)
    {
        playerData.username = e.ProfileName;
        NotifyOnPropertyChanged(DataLoadedObserverName);
    }

    public void RequestProfileData()
    {
        ApiManager.Instance.GetUserRequest(null, OnGetUserData);
    }
    private void OnGetUserData(UserData userData)
    {
        DataManager.Instance.MyData = userData;
        playerData = userData;
        NotifyOnPropertyChanged(DataLoadedObserverName);
    }

    ~HomeModel()
    {
        Events.instance.RemoveListener<ProfileNameUpdated>(OnProfileNameUpdated);
    }
}
