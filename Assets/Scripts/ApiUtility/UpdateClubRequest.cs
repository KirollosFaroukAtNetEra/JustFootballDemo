using System;
using UnityEngine.Networking;

public class UpdateClubRequest : RequestBase
{
    protected override string RequestUrl => ApiUrl + "/user/set/club";

    public Club NewClub;

    public Action callBack;

    public override UnityWebRequest GetRequest()
    {
        RequestType = HTTPReqType.POST;
        Body = NewClub.ToJson();
        return base.GetRequest();
    }

    public override void HandleResponse( UnityWebRequest response )
    {
        base.HandleResponse( response );
        callBack?.Invoke();
    }
}
