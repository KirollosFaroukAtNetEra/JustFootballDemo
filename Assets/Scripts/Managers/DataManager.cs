using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : BaseManager<DataManager>
{
    public UserData MyData;

    public override void Initialize()
    {
        base.Initialize();
        IsReady = true;
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
            callback.Invoke( Sprite.Create( Texture2D.blackTexture,
                new Rect( 0, 0, 100, 100 ),
                new Vector2( 0.5f, 0.5f ) ) );
            yield break;
        }

        var texture2D = DownloadHandlerTexture.GetContent( req );
        var sprite = Sprite.Create( texture2D,
            new Rect( 0, 0, texture2D.width, texture2D.height ),
            new Vector2( 0.5f, 0.5f ) );

        callback.Invoke( sprite );
    }
}
