using Assets.Scripts.PooledScrollList;
using UnityEngine;
using UnityEngine.UI;

public class ClubItem : PooledElement<ClubData>
{
    public Image clubImage;
    public Text clubName;
    public Text clubLeague;
    public Button clubButton;
    public GameObject CheckMarkImage;

    [SerializeField]
    private ClubData _data;
    public override ClubData Data
    {
        get => _data;
        set
        {
            _data = value;
            SetupView( value );
        }
    }

    public override void UpdateData()
    {
        CheckMarkImage.SetActive( Data.id == DataManager.Instance.MyData.club );
    }

    public void SetupView( ClubData data )
    {
        clubName.text = data.name;
        clubLeague.text = data.league;
        CheckMarkImage.SetActive( data.id == DataManager.Instance.MyData.club );
        DataManager.Instance.GetSpriteByUrl( data.logoUrl,
            ( image ) =>
            {
                if( clubImage == null )
                    return;
                clubImage.sprite = image;
            } );
    }
}
