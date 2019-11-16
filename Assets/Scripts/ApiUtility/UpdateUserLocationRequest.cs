using System;
using UnityEngine.Networking;

public class UpdateUserLocationRequest : RequestBase
{
    protected override string RequestUrl => ApiUrl + "/user/set/location";

    public LocationData NewLocation;

    public Action SuccessCallBack;

    public override UnityWebRequest GetRequest()
    {
        RequestType = HTTPReqType.POST;
        Body = NewLocation.ToJson();
        return base.GetRequest();
    }

    public override void HandleResponse(UnityWebRequest response)
    {
        base.HandleResponse(response);
        SuccessCallBack?.Invoke();
    }
}
