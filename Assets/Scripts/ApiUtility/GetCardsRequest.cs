using System;
using UnityEngine.Networking;

public class GetCardsRequest : RequestBase
{
    protected override string RequestUrl => ApiUrl + "/cards";

    public Action<CardData[]> SuccessCallBack;

    public override void HandleResponse(UnityWebRequest response)
    {
        base.HandleResponse(response);
        SuccessCallBack?.Invoke( response.downloadHandler.text.CreateArrayFromJson<CardData>() );
    }
}
