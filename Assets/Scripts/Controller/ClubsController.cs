using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Networking;

public class ClubsController : UIController<ClubsModel>
{
    public override void Setup(ClubsModel model, object dataObject)
    {
        base.Setup(model);
        Model.RequestClubs();
    }
    public void OnClubItemClicked(string ClubID)
    {
        Debug.Log("ClubClicked " + ClubID);
        ApiManager.Instance.SetClub( new Club( ClubID ),
            () =>
            {
                DataManager.Instance.MyData.club = ClubID;
                Events.instance.Raise( new ClubDataUpdated(ClubID) );
            } );
    }
    public override void RetryLoadData()
    {
        Model.RequestClubs();
    }

}
