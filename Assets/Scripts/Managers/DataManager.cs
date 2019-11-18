using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : BaseManager<DataManager>
{
    public UserData MyData;

    private Dictionary<string, Sprite> SimpleSpriteCache;

    public override void Initialize()
    {
        base.Initialize();
        IsReady = true;
        SimpleSpriteCache = new Dictionary<string, Sprite>();
    }

    public void GetSpriteByUrl(string spriteUrl, Action<Sprite> callback)
    {
        StartCoroutine(GetSprite(spriteUrl, callback));
    }

    private IEnumerator GetSprite(string spriteUrl, Action<Sprite> callback)
    {
        Sprite spriteToDownload;

        if (!SimpleSpriteCache.TryGetValue(spriteUrl, out spriteToDownload))
        {
            UnityWebRequest req = UnityWebRequestTexture.GetTexture(spriteUrl);

            yield return req.SendWebRequest();

            if (req.isNetworkError ||
                req.isHttpError)
            {
                callback.Invoke(Sprite.Create(Texture2D.blackTexture,
                    new Rect(0, 0, 4, 4),
                    new Vector2(0.5f, 0.5f)));
                yield break;
            }

            var texture2D = DownloadHandlerTexture.GetContent(req);
            spriteToDownload = Sprite.Create(texture2D,
                new Rect(0, 0, texture2D.width, texture2D.height),
                new Vector2(0.5f, 0.5f));

            SimpleSpriteCache[spriteUrl] = spriteToDownload;
        }

        callback.Invoke(spriteToDownload);
    }
}
