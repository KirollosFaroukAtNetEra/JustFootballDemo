[System.Serializable]
public class ProfileModel : UIModel
{
    private UserData _playerData;
    public UserData PlayerData
    {
        set
        {
            _playerData = value;
            NotifyOnPropertyChanged(DataLoadedObserverName);
        }
        get => _playerData;
    }

    public bool IsMyProfile;

    public void RequestProfileData(string userId="")
    {
        IsMyProfile = userId == string.Empty;
        ApiManager.Instance.GetUserRequest( userId, OnGetUserData );
    }
    private void OnGetUserData(UserData userData)
    {
        PlayerData = userData;
    }
}