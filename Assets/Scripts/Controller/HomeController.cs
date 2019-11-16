public class HomeController : UIController<HomeModel>
{
    public override void Setup( HomeModel model, object dataObject )
    {
        base.Setup( model );
        Model.RequestProfileData();
    }

    public void OpenClubView()
    {
        ViewsManager.Instance.OpenView( ViewType.ClubsView );
    }

    public void OpenCardsView()
    {
        ViewsManager.Instance.OpenView( ViewType.CardsView );
    }

    public void OpenProfileView()
    {
        ViewsManager.Instance.OpenView( ViewType.ProfileView );
    }
}