using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class LocationData
{
    public double lat;
    public double lng;
}

[Serializable]
public class UserData
{
    public string username;
    public string club;
    public LocationData location;
    public string pictureUrl;
    public string clubPictureUrl;
}

[Serializable]
public class CardsData
{
    [Serializable]
    public class CardData
    {
        public string id;
        public string username;
        public string pictureUrl;
        public string clubPictureUrl;
    }

    public CardData[] cards;
}

[Serializable]
public class ClubsData
{
    [System.Serializable]
    public class ClubData
    {
        public string id;
        public string logoUrl;
        public string name;
        public string league;
    }
    public ClubData[] clubs;

    public Dictionary<string, Texture2D> clubsSprites;

}

public class DataManager : BaseManager<DataManager>
{
    public UserData MyData;

    private float downloadTimeStart;

    public override void Initialize()
    {
        base.Initialize();
        IsReady = true;
    }
    public void DownloadSprites(ClubsData clubObject)
    {
        clubObject.clubsSprites = new Dictionary<string, Texture2D>();
    }

    public void GetSpriteByUrl(string spriteUrl, Action<Sprite> callback)
    {
        StartCoroutine(GetSprite(spriteUrl, callback));
    }
    private IEnumerator GetSprite(string spriteUrl, Action<Sprite> callback)
    {
        UnityWebRequest req = UnityWebRequestTexture.GetTexture(spriteUrl);

        yield return req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
        }
        var texture2D = DownloadHandlerTexture.GetContent(req);
        var sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
        callback.Invoke(sprite);
    }

}
