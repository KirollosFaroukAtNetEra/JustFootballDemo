using System;
using UnityEngine.Networking;

public class UpdateUsernameRequest : RequestBase
{
    protected override string RequestUrl => ApiUrl + "/user/set/username";

    public UserName NewUsername;

    public Action SuccessCallBack;

    public override UnityWebRequest GetRequest()
    {
        RequestType = HTTPReqType.POST;
        Body = NewUsername.ToJson();
        return base.GetRequest();
    }

    public override void HandleResponse( UnityWebRequest response )
    {
        base.HandleResponse( response );
        SuccessCallBack?.Invoke();
    }
}
