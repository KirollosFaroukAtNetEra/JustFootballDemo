using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Assets.Scripts;

public class DataManager : BaseManager<DataManager>
{
    public UserData MyData;

    public override void Initialize()
    {
        base.Initialize();
        IsReady = true;
    }

    public void DownloadSprites( ClubsData clubObject )
    {
        clubObject.clubsSprites = new Dictionary<string, Texture2D>();
    }

    public void GetSpriteByUrl( string spriteUrl, Action<Sprite> callback )
    {
        StartCoroutine( GetSprite( spriteUrl, callback ) );
    }

    private IEnumerator GetSprite( string spriteUrl, Action<Sprite> callback )
    {
        UnityWebRequest req = UnityWebRequestTexture.GetTexture( spriteUrl );

        yield return req.SendWebRequest();

        if( req.isNetworkError ||
            req.isHttpError )
        {
            Debug.Log( req.error?? "" );

            callback.Invoke( null );
            yield break;
        }

        var texture2D = DownloadHandlerTexture.GetContent( req );
        var sprite = Sprite.Create( texture2D,
            new Rect( 0, 0, texture2D.width, texture2D.height ),
            new Vector2( 0.5f, 0.5f ) );

        callback.Invoke( sprite );
    }
}
