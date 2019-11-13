using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ClubsModel : UIModel
{
    public ClubsData.ClubData[] ClubsList = new ClubsData.ClubData[0];

    public void RequestClubs()
    {
        ApiManager.Instance.GetClubs(null,OnGetClubsComplete);
    }
    private void OnGetClubsComplete(ClubsData.ClubData[] clubsData)
    {
        ClubsList = clubsData;
        NotifyOnPropertyChanged("ClubsList");
    }
}