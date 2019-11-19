public class ClubsController : UIController<ClubsModel>
{
    public override void Setup( ClubsModel model, object dataObject )
    {
        base.Setup( model );
        Model.RequestClubs();
    }

    public void OnClubItemClicked( string ClubID )
    {
        ApiManager.Instance.SetClubRequest( new Club( ClubID ),
            () =>
            {
                DataManager.Instance.MyData.club = ClubID;
                Events.Instance.Raise( new ClubDataUpdated( ClubID ) );
            } );
    }
}