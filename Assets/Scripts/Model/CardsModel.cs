using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class CardsModel : UIModel
{
    public CardsData.CardData[] CardsList = new CardsData.CardData[0];

    public void GetCards()
    {
        ApiManager.Instance.GetCardsRequest(OnGetCardsComplete);
    }
    private void OnGetCardsComplete(CardsData.CardData[] cardsData)
    {
        CardsList = cardsData;
        NotifyOnPropertyChanged(DataLoadedObserverName);
    }

}
