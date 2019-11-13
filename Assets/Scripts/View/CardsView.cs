using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardsView : UIView<CardsModel,CardsController>
{
    public RectTransform CardsContent;
    public GameObject CardItemPrefab;
    public List<CardItem> CardsList;
    public CardsScroll cardsScroll;

    public override void RegisterDependency()
    {
        base.RegisterDependency();
        Model.ListenOnPropertyChanged("CardsList", CardsListChanged);
        isLoaded = true;
    }
    private void CardsListChanged()
    {
        if(Model.CardsList.Length > 0 && cardsScroll != null)
        {
            cardsScroll.Initialize(Model.CardsList.ToList());
            for ( int i = 0; i < cardsScroll.ActiveElements.Count; i++ )
            {
                HandleClubItemData(cardsScroll.ActiveElements[ i ] );
            }
            LoadingAnimation.SetActive(false);
        }
    }
    private void HandleClubItemData( CardItem cardItem)
    {
        cardItem.CardButton.onClick.RemoveAllListeners();
        cardItem.CardButton.onLongPress.RemoveAllListeners();
        cardItem.CardButton.onLongPressCanceled.RemoveAllListeners();
        cardItem.CardButton.onClick.AddListener(() => { OnCardClicked(cardItem); });
        cardItem.CardButton.onLongPress.AddListener(() => { OnCardLongPressed(cardItem); });
        cardItem.CardButton.onLongPressCanceled.AddListener(() => { OnLongPressCanceled(cardItem.gameObject); });
        cardItem.CardButton.onLongPressStart.AddListener(() => { StartShakeAnimation(cardItem.gameObject); });

    }
    private void OnCardClicked(CardItem cardItem)
    {
        Controller.OnCardItemClicked(cardItem.Data.id);
    }

    private void OnCardLongPressed(CardItem cardItem)
    {
        cardsScroll.Remove(cardItem.Data);
        ViewsManager.Instance.ShowAlert("Card Deleted");
        AnimationManager.Instance.StopAnimation(cardItem.gameObject, AnimationType.Shake);
    }

    private void OnLongPressCanceled(GameObject button)
    {
        AnimationManager.Instance.StopAnimation(button, AnimationType.Shake);
    }
    private void StartShakeAnimation(GameObject button)
    {
        AnimationManager.Instance.AddAnimation(AnimationType.Shake, button);
    }
}
