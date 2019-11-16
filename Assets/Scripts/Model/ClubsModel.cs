public class ClubsModel : UIModel
{
    public ClubData[] ClubsList = new ClubData[0];

    public void RequestClubs()
    {
        ApiManager.Instance.GetClubsRequest( null, OnGetClubsComplete );
    }

    private void OnGetClubsComplete( ClubData[] clubsData )
    {
        ClubsList = clubsData;
        NotifyOnPropertyChanged( DataLoadedObserverName );
    }
}