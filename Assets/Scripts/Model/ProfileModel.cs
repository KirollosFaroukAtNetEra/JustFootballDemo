using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

[System.Serializable]
public class ProfileModel : UIModel
{
    private UserData _playerData;
    public UserData PlayerData
    {
        set
        {
            _playerData = value;
            NotifyOnPropertyChanged("PlayerData");
        }
        get => _playerData;
    }

    public bool IsMyProfile;

    public void RequestProfileData(string userId="")
    {
        IsMyProfile = userId == string.Empty;
        ApiManager.Instance.GetUser(new UserName( userId ), OnGetUserData);
    }
    private void OnGetUserData(UserData userData)
    {
        PlayerData = userData;
    }
}