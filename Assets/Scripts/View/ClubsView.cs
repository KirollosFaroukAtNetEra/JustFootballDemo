using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClubsView : UIView<ClubsModel, ClubsController>
{
    public RectTransform ClubsScrollContent;
    public GameObject ClubPrefab;
    public List<ClubItem> ClubsList;
    public ClubsScroll clubsScroll;

    private void OnEnable()
    {
        Events.instance.AddListener<ClubDataUpdated>( OnClubDataUpdated );
    }

    private void OnDisable()
    {
        Events.instance.RemoveListener<ClubDataUpdated>( OnClubDataUpdated );
    }

    private void OnClubDataUpdated( ClubDataUpdated e )
    {
        for( int i = 0; i < clubsScroll.ActiveElements.Count; i++ )
        {
            clubsScroll.ActiveElements[ i ].UpdateData();
        }
    }

    public override void DataLoaded()
    {
        if( Model.ClubsList.Length > 0 &&
            clubsScroll != null )
        {
            clubsScroll.Initialize( Model.ClubsList.ToList() );
            LoadingAnimation.SetActive( false );
        }

        for( int i = 0; i < clubsScroll.ActiveElements.Count; i++ )
        {
            HandleClubItemData( clubsScroll.ActiveElements[ i ] );
        }

    }

    private void HandleClubItemData( ClubItem clubGameObject )
    {
        clubGameObject.clubButton.onClick.RemoveAllListeners();
        clubGameObject.clubButton.onClick.AddListener( () => { OnClubClicked( clubGameObject.Data.id ); } );
    }

    private void OnClubClicked( string clubID )
    {
        Controller.OnClubItemClicked( clubID );
    }
}
