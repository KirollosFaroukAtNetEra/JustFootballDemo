using System.Collections;
using System.Collections.Generic;
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
        NotifyOnPropertyChanged("PlayerData");
    }

    public void RequestProfileData()
    {
        ApiManager.Instance.GetUser(null, OnGetUserData);
    }
    private void OnGetUserData(UserData userData)
    {
        DataManager.Instance.MyData = userData;
        playerData = userData;
        NotifyOnPropertyChanged("PlayerData");
    }

    ~HomeModel()
    {
        Events.instance.RemoveListener<ProfileNameUpdated>(OnProfileNameUpdated);
    }
}
