using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PooledScrollList;
using UnityEngine;
using UnityEngine.UI;

public class ClubItem : PooledElement<ClubsData.ClubData>
{
    public Image clubImage;
    public Text clubName;
    public Text clubLeague;
    public Button clubButton;
    public GameObject CheckMarkImage;
    [SerializeField]
    private ClubsData.ClubData _data;
    public override ClubsData.ClubData Data
    {
        get => _data;
        set
        {
            _data = value;
            SetupView(value);
        }
    }
    public override void Updatedata()
    {
        CheckMarkImage.SetActive(Data.id == DataManager.Instance.MyData.club);
    }

    public void SetupView(ClubsData.ClubData data)
    {
        clubName.text = data.name;
        clubLeague.text = data.league;
        CheckMarkImage.SetActive( data.id == DataManager.Instance.MyData.club );
        DataManager.Instance.GetSpriteByUrl(data.logoUrl, (image) => { if (clubImage == null) return; clubImage.sprite = image; });
    }

}
