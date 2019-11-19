using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClubsView : UIView<ClubsModel, ClubsController>
{
    public RectTransform ClubsScrollContent;
    public GameObject ClubPrefab;
    public List<ClubItem> ClubsList;
    public ClubsScroll ScrollRect;

    private void OnEnable()
    {
        Events.Instance.AddListener<ClubDataUpdated>( OnClubDataUpdated );
    }

    private void OnDisable()
    {
        Events.Instance.RemoveListener<ClubDataUpdated>( OnClubDataUpdated );
    }

    private void OnClubDataUpdated( ClubDataUpdated e )
    {
        for( int i = 0; i < ScrollRect.ActiveElements.Count; i++ )
        {
            ScrollRect.ActiveElements[ i ].UpdateData();
        }
    }

    public override void DataLoaded()
    {
        if( Model.ClubsList.Length > 0 &&
            ScrollRect != null )
        {
            ScrollRect.Initialize( Model.ClubsList.ToList() );
            LoadingAnimation.SetActive( false );
        }

        for( int i = 0; i < ScrollRect.ActiveElements.Count; i++ )
        {
            HandleClubItemData( ScrollRect.ActiveElements[ i ] );
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
