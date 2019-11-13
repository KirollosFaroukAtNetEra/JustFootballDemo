using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PooledScrollList;
using UnityEngine;
using UnityEngine.UI;

public class CardItem : PooledElement<CardsData.CardData>
{
    public Image backGround;
    public Image playerImage;
    public Image clubIcon;
    public Text playerName;
    public UIButton CardButton;
    [SerializeField]
    private CardsData.CardData _data;
    public override CardsData.CardData Data {
        get => _data;
        set
        {
            _data = value;
            SetupView( value );
        }
    }
    public override void Updatedata()
    {
        Debug.Log( "Updateing Cards" );
    }

    public void SetupView( CardsData.CardData data)
    {
        playerName.text = data.username;
        DataManager.Instance.GetSpriteByUrl(data.pictureUrl,(image) =>{
            if (playerImage == null) return; playerImage.sprite = image;} );
        DataManager.Instance.GetSpriteByUrl(data.clubPictureUrl, (image) => { if (clubIcon == null) return;  clubIcon.sprite = image; });
    }
}
