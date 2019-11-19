using System;
using UnityEngine.Networking;

public enum HTTPReqType
{
    GET,
    POST
}

public abstract class RequestBase
{
    protected const string ApiUrl = "https://demo.dev.justfootball.io/api";

    protected HTTPReqType RequestType = HTTPReqType.GET;
    protected virtual string RequestUrl { get; }
    protected string Body;

    public Action FailCallBack;

    public virtual UnityWebRequest GetRequest()
    {
        var request = new UnityWebRequest( RequestUrl, RequestType.ToString() );

        request.SetRequestInfo( Body );

        return request;
    }

    public virtual void HandleResponse( UnityWebRequest response )
    {
        var responseText = response.downloadHandler.text;
        if( response.isHttpError ||
            response.isNetworkError ||
            ( responseText.Contains( "success" ) &&
                responseText.CreateFromJson<ErrorJson>().success.ToLower() == "false" ) )
        {
            ViewsManager.Instance.ShowAlert( "Network Error, Retrying now..." );
            ApiManager.Instance.Requests.Enqueue( this );
            FailCallBack?.Invoke();
        }
    }
}

