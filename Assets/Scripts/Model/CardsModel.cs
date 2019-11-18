public class CardsModel : UIModel
{
    public CardData[] CardsList = new CardData[0];

    public void GetCards()
    {
        ApiManager.Instance.GetCardsRequest( OnGetCardsComplete );
    }

    private void OnGetCardsComplete( CardData[] cardsData )
    {
        CardsList = cardsData;
        NotifyOnPropertyChanged( DataLoadedObserverName );
    }
}
