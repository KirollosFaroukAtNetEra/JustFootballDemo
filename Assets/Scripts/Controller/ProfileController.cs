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
        ApiManager.Instance.PostUserNameRequest( new UserName( newUserName ),
            () =>
            {
                Events.instance.Raise(new ProfileNameUpdated(newUserName) );
            } );
    }
}
