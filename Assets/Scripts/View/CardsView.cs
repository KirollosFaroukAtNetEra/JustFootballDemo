using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardsView : UIView<CardsModel, CardsController>
{
    public RectTransform CardsContent;
    public GameObject CardItemPrefab;
    public List<CardItem> CardsList;
    public CardsScroll ScrollRect;

    public override void DataLoaded()
    {
        if( Model.CardsList.Length > 0 &&
            ScrollRect != null )
        {
            ScrollRect.Initialize( Model.CardsList.ToList() );
            for( int i = 0; i < ScrollRect.ActiveElements.Count; i++ )
            {
                HandleClubItemData( ScrollRect.ActiveElements[ i ] );
            }

            LoadingAnimation.SetActive( false );
        }
    }

    private void HandleClubItemData( CardItem cardItem )
    {
        cardItem.CardButton.onClick.RemoveAllListeners();
        cardItem.CardButton.onLongPress.RemoveAllListeners();
        cardItem.CardButton.onLongPressCanceled.RemoveAllListeners();
        cardItem.CardButton.onClick.AddListener( () => { OnCardClicked( cardItem ); } );
        cardItem.CardButton.onLongPress.AddListener( () => { OnCardLongPressed( cardItem ); } );
        cardItem.CardButton.onLongPressCanceled.AddListener( () => { OnLongPressCanceled( cardItem.gameObject ); } );
        cardItem.CardButton.onLongPressStart.AddListener( () => { StartShakeAnimation( cardItem.gameObject ); } );
    }

    private void OnCardClicked( CardItem cardItem )
    {
        Controller.OnCardItemClicked( cardItem.Data.id );
    }

    private void OnCardLongPressed( CardItem cardItem )
    {
        ScrollRect.Remove( cardItem.Data );
        ViewsManager.Instance.ShowAlert( "Card Deleted" );
        AnimationManager.Instance.StopAnimation( cardItem.gameObject, AnimationType.Shake );
    }

    private void OnLongPressCanceled( GameObject button )
    {
        AnimationManager.Instance.StopAnimation( button, AnimationType.Shake );
    }

    private void StartShakeAnimation( GameObject button )
    {
        AnimationManager.Instance.AddAnimation( AnimationType.Shake, button );
    }
}