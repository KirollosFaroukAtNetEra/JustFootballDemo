using Assets.Scripts.PooledScrollList;
using UnityEngine;
using UnityEngine.UI;

public class CardItem : PooledElement<CardData>
{
    public Image backGround;
    public Image playerImage;
    public Image clubIcon;
    public Text playerName;
    public UIButton CardButton;
    [SerializeField]
    private CardData _data;
    public override CardData Data {
        get => _data;
        set
        {
            _data = value;
            SetupView( value );
        }
    }
    public override void UpdateData()
    {
        Debug.Log( "Updateing Cards" );
    }

    public void SetupView( CardData data)
    {
        playerName.text = data.username;
        DataManager.Instance.GetSpriteByUrl(data.pictureUrl,(image) =>{
            if (playerImage == null) return; playerImage.sprite = image;} );
        DataManager.Instance.GetSpriteByUrl(data.clubPictureUrl, (image) => { if (clubIcon == null) return;  clubIcon.sprite = image; });
    }
}
