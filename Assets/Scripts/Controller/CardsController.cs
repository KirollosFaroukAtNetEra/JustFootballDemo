public class CardsController : UIController<CardsModel>
{
    public override void Setup(CardsModel model, object dataObject)
    {
        base.Setup(model);
        Model.GetCards();
    }

    public void OnCardItemClicked(string cardID)
    {
        ViewsManager.Instance.OpenView( ViewType.ProfileView , cardID);
    }
}
