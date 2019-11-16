using UnityEngine;

public class ClubsController : UIController<ClubsModel>
{
    public override void Setup( ClubsModel model, object dataObject )
    {
        base.Setup( model );
        Model.RequestClubs();
    }

    public void OnClubItemClicked( string ClubID )
    {
        Debug.Log( "ClubClicked " + ClubID );
        ApiManager.Instance.SetClubRequest( new Club( ClubID ),
            () =>
            {
                DataManager.Instance.MyData.club = ClubID;
                Events.instance.Raise( new ClubDataUpdated( ClubID ) );
            } );
    }
}