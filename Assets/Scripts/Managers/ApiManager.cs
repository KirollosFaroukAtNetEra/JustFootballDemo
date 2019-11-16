using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ApiManager : BaseManager<ApiManager>
{
    public static string _authToken;

    public bool IsNewUser = true;

    public bool IsConnected;

    public override void Initialize()
    {
        StartCoroutine( SendRequest( new AuthenticationRequest( IsNewUser,
            ( b, s ) =>
            {
                IsConnected = b;
                _authToken = s;
            } ) ) );

        IsReady = true;
    }

    public void GetUserRequest( string playerName = "", Action<UserData> onComplete = null )
    {
        StartCoroutine( SendRequest( new GetUserRequest()
        {
            UserId = playerName,
            callBack = onComplete
        } ) );
    }

    public void PostUserNameRequest( UserName userName, Action onComplete = null )
    {
        StartCoroutine( SendRequest( new UpdateUsernameRequest()
        {
            NewUsername =  userName,
            callBack = onComplete
        } ) );
    }

    public void SetClubRequest( Club club, Action onComplete = null )
    {
        StartCoroutine( SendRequest( new UpdateClubRequest()
        {
            NewClub = club,
            callBack = onComplete
        } ) );
    }

    public void GetClubsRequest( string clubId = "", Action<ClubData[]> onComplete = null )
    {
        StartCoroutine( SendRequest( new GetClubsRequest()
        {
            ClubId = clubId,
            callBack = onComplete
        } ) );
    }

    public void GetCardsRequest( Action<CardData[]> onComplete = null )
    {
        StartCoroutine( SendRequest( new GetCardsRequest()
        {
            callBack = onComplete
        } ) );
    }

    public void SendGpsData( LocationData locInfo, Action onComplete = null )
    {
        StartCoroutine( SendRequest( new UpdateUserLocationRequest()
        {
            NewLocation = locInfo,
            callBack = onComplete
        } ) );
    }

    private IEnumerator SendRequest<T>(T req) where T : RequestBase
    {
        var _request = req.GetRequest();

        yield return _request.SendWebRequest();

        req.HandleResponse( _request );
    }
}