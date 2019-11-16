using System;
using UnityEngine.Networking;

public class GetCardsRequest : RequestBase
{
    protected override string RequestUrl => ApiUrl + "/cards";

    public Action<CardData[]> callBack;

    public override void HandleResponse(UnityWebRequest response)
    {
        base.HandleResponse(response);
        callBack?.Invoke( response.downloadHandler.text.CreateArrayFromJson<CardData>() );
    }
}
