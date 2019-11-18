using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApiManager : BaseManager<ApiManager>
{
    public static string AuthToken;

    public bool IsNewUser = true;
    public bool IsConnected;

    private readonly float _resyncTime = 10f;

    public Queue<RequestBase> Requests = new Queue<RequestBase>();

    public override void Initialize()
    {
        StartCoroutine( SendRequest( new AuthenticationRequest()
        {
            IsNewUser = true,
            SuccessCallBack = ( isDone, token ) =>
            {
                IsConnected = IsReady = isDone;
                AuthToken = token;
            }
        } ) );

        StartCoroutine( CheckQueue() );
    }

    private IEnumerator CheckQueue()
    {
        var delay = new WaitForSeconds( _resyncTime );
        while( true )
        {
            yield return delay;

            while( Requests.Count > 0 )
            {
                var failedReq = Requests.Dequeue();
                StartCoroutine( SendRequest( failedReq ) );
            }
        }
    }

    public void GetUserRequest( string playerName = "", Action<UserData> onComplete = null )
    {
        StartCoroutine( SendRequest( new GetUserRequest()
        {
            UserId = playerName,
            SuccessCallBack = onComplete
        } ) );
    }

    public void PostUserNameRequest( UserName userName, Action onComplete = null )
    {
        StartCoroutine( SendRequest( new UpdateUsernameRequest()
        {
            NewUsername = userName,
            SuccessCallBack = onComplete
        } ) );
    }

    public void SetClubRequest( Club club, Action onComplete = null )
    {
        StartCoroutine( SendRequest( new UpdateClubRequest()
        {
            NewClub = club,
            SuccessCallBack = onComplete
        } ) );
    }

    public void GetClubsRequest( string clubId = "", Action<ClubData[]> onComplete = null )
    {
        StartCoroutine( SendRequest( new GetClubsRequest()
        {
            ClubId = clubId,
            SuccessCallBack = onComplete
        } ) );
    }

    public void GetCardsRequest( Action<CardData[]> onComplete = null )
    {
        StartCoroutine( SendRequest( new GetCardsRequest()
        {
            SuccessCallBack = onComplete
        } ) );
    }

    public void SendGpsData( LocationData locInfo, Action onComplete = null )
    {
        StartCoroutine( SendRequest( new UpdateUserLocationRequest()
        {
            NewLocation = locInfo,
            SuccessCallBack = onComplete
        } ) );
    }

    private static IEnumerator SendRequest<T>( T request ) where T : RequestBase
    {
        var webRequest = request.GetRequest();

        yield return webRequest.SendWebRequest();

        request.HandleResponse( webRequest );
    }
}