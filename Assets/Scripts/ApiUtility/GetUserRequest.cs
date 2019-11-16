using System;
using UnityEngine.Networking;

public class GetUserRequest : RequestBase
{
    protected override string RequestUrl
    {
        get
        {
            if( string.IsNullOrEmpty( UserId ) )
            {
                return ApiUrl + "/user/me";
            }

            return ApiUrl + "/user/get/" + UserId;
        }
    }

    public string UserId;

    public Action<UserData> SuccessCallBack;

    public override void HandleResponse( UnityWebRequest response )
    {
        base.HandleResponse( response );
        SuccessCallBack?.Invoke( response.downloadHandler.text.CreateFromJson<UserData>() );
    }
}
