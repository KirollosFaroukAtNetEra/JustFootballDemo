using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class AuthenticationRequest : RequestBase
{
    private readonly string _deviceID = SystemInfo.deviceUniqueIdentifier;

    private Token _authToken;
    private Action<bool, string> callBack;

    private const string RequestUrl = ApiUrl + "/auth/token";

    public AuthenticationRequest(bool isNewUser, Action<bool, string> callBack)
    {
        _deviceID = isNewUser ? Guid.NewGuid().ToString() : _deviceID;
        _authToken = new Token( _deviceID );

        this.callBack = callBack;
    }

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
        callBack?.Invoke( response.responseCode == 200L, response.downloadHandler.text );
    }
}
