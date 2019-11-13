using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class ProfileController : UIController<ProfileModel>
{
    private string playerID;
    public override void Setup(ProfileModel model,object dataObject)
    {
        base.Setup(model);
        playerID = string.Empty;

        if( dataObject != null )
        {
            playerID = dataObject as string;
        }

        Model.RequestProfileData(playerID);
    }

    public void OnChangeNameClicked(string newUserName)
    {
        ApiManager.Instance.SetUserName( new UserName( newUserName ),
            () =>
            {
                Debug.Log( "Update Name Complete" );
                Events.instance.Raise(new ProfileNameUpdated(newUserName) );
            } );
    }
    public override void RetryLoadData()
    {
        Model.RequestProfileData(playerID);
    }
}
