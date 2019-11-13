using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : UIController<HomeModel>
{
    public override void Setup(HomeModel model, object dataObject)
    {
        base.Setup(model);
        if (!ApiManager.Instance.IsConnected) return;
            Model.RequestProfileData();
    }

    public void OpenClubView()
    {
        ViewsManager.Instance.OpenView(ViewType.ClubsView);
    }
    public void OpenCardsView()
    {
        ViewsManager.Instance.OpenView(ViewType.CardsView);
    }
    public void OpenProfileView()
    {
        ViewsManager.Instance.OpenView(ViewType.ProfileView);
    }
    public override void RetryLoadData()
    {
        if (!ApiManager.Instance.IsConnected) return;
        Model.RequestProfileData();
    }
}
