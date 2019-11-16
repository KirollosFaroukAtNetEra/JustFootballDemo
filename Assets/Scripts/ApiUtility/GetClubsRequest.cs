using System;
using UnityEngine.Networking;

public class GetClubsRequest : RequestBase
{
    protected override string RequestUrl
    {
        get
        {
            if( string.IsNullOrEmpty( ClubId ) )
            {
                return ApiUrl + "/clubs";
            }

            return ApiUrl + "/club/" + ClubId;
        }
    }

    public string ClubId;

    public Action<ClubData[]> SuccessCallBack;

    public override UnityWebRequest GetRequest()
    {
        RequestType = HTTPReqType.GET;
        return base.GetRequest();
    }

    public override void HandleResponse( UnityWebRequest response )
    {
        base.HandleResponse( response );
        SuccessCallBack?.Invoke( response.downloadHandler.text.CreateArrayFromJson<ClubData>() );
    }
}
