[System.Serializable]
public class HomeModel : UIModel
{
    public UserData playerData;

    public HomeModel()
    {
        Events.Instance.AddListener<ProfileNameUpdated>(OnProfileNameUpdated);
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
        Events.Instance.RemoveListener<ProfileNameUpdated>(OnProfileNameUpdated);
    }
}
