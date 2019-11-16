using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class AuthenticationRequest : RequestBase
{
    private const string RequestUrl = ApiUrl + "/auth/token";
    private string _deviceID = SystemInfo.deviceUniqueIdentifier;

    private Token _authToken => new Token( _deviceID );

    public bool IsNewUser
    {
        set => _deviceID = value ? Guid.NewGuid().ToString() : _deviceID;
    }

    public Action<bool, string> SuccessCallBack;

    public override UnityWebRequest GetRequest()
    {
        var rawBytes = Encoding.UTF8.GetBytes(_authToken.ToJson());

        var request = new UnityWebRequest( RequestUrl, "POST" )
        {
            uploadHandler = new UploadHandlerRaw( rawBytes ),
            downloadHandler = new DownloadHandlerBuffer()
        };

        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }

    public override void HandleResponse(UnityWebRequest response)
    {
        base.HandleResponse( response );
        SuccessCallBack?.Invoke( response.responseCode == 200L, response.downloadHandler.text );
    }
}
