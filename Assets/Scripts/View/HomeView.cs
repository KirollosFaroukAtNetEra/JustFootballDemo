using UnityEngine.UI;

public class HomeView : UIView<HomeModel, HomeController>
{
    public Text playerName;
    public Image playerImage;

    public override void DataLoaded()
    {
        if (Model.playerData != null && Model.playerData.pictureUrl != "")
        {
            DataManager.Instance.GetSpriteByUrl(Model.playerData.pictureUrl,
                (image) => { playerImage.sprite = image; });
            playerName.text = Model.playerData.username;
        }
        if (ApiManager.Instance.IsConnected)
        {
            LoadingAnimation.SetActive(false);
        }
    }

    public void OnClubClick()
    {
        Controller.OpenClubView();
    }
    public void OnCardsClick()
    {
        Controller.OpenCardsView();
    }
    public void OnProfileClick()
    {
        Controller.OpenProfileView();
    }

}