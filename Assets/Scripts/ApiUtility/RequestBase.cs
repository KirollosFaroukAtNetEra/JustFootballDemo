using UnityEngine;
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

    public virtual UnityWebRequest GetRequest()
    {
        var request = new UnityWebRequest( RequestUrl, RequestType.ToString() );

        request.SetRequestInfo( Body );

        return request;
    }

    public virtual void HandleResponse(UnityWebRequest response)
    {
        if (response.isHttpError || response.isNetworkError)
        {
            ViewsManager.Instance.ShowAlert("Network Error");
            Debug.LogError("Net Error");
        }
    }
}

