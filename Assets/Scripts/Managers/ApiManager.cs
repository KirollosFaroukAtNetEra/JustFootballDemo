using Assets.Scripts;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ApiManager : BaseManager<ApiManager>
{
    private const string ApiUrl = "https://demo.dev.justfootball.io/api";
    private const string AuthorizationUrl = ApiUrl + "/auth/token";
    private const string GetMyUserUrl = ApiUrl + "/user/me";
    private const string GetUserUrl = ApiUrl + "/user/get/"; // + user id
    private const string PostUsernameUrl = ApiUrl + "/user/set/username";
    private const string PostClubUrl = ApiUrl + "/user/set/club";
    private const string PostUserLocationUrl = ApiUrl + "/user/set/location";
    private const string GetCardsUrl = ApiUrl + "/cards";
    private const string GetAllClubsUrl = ApiUrl + "/clubs";
    private const string GetClubUrl = ApiUrl + "/club/"; // + club id

    private static string _deviceID;
    private static string _authToken;

    private UnityWebRequest _request;

    public bool IsNewUser = true;

    private bool finishLoadUser;
    private bool finishLoadClubs;
    private bool finishLoadCards;

    public bool IsConnected;

    public override void Awake()
    {
        base.Awake();
        _deviceID = SystemInfo.deviceUniqueIdentifier;
    }

    public override void Initialize()
    {
        StartCoroutine( GetAuthToken() );
        IsReady = true;
    }

    private void SetRequestInfo( string jsonBody = "" )
    {
        _request.SetRequestHeader( "Authorization", $"Bearer {_authToken}" );
        _request.downloadHandler = new DownloadHandlerBuffer();

        if( !string.IsNullOrEmpty( jsonBody ) )
        {
            var rawBytes = Encoding.UTF8.GetBytes( jsonBody );
            _request.uploadHandler = new UploadHandlerRaw( rawBytes );

            _request.SetRequestHeader( "Content-Type", "application/json" );
        }
    }

    public void GetUserRequest( UserName userName = null, Action<UserData> onComplete = null )
    {
        string playerName = userName != null ? userName.username : "";
        if( finishLoadUser )
            StopCoroutine( GetUser( playerName, onComplete ) );
        StartCoroutine( GetUser( playerName, onComplete ) );
    }

    public void PostUserNameRequest( UserName userName, Action onComplete = null )
    {
        StartCoroutine( PostUserName( UserName.ToJson( userName ), onComplete ) );
    }

    public void SetClubRequest( Club club, Action onComplete = null )
    {
        StartCoroutine( PostClub( Club.ToJson(club), onComplete ) );
    }

    public void GetClubsRequest( Club club = null, Action<ClubsData.ClubData[]> onComplete = null )
    {
        string clubName = club != null ? club.club : "";
        if( finishLoadClubs )
            StopCoroutine( GetClubs( clubName, onComplete ) );
        StartCoroutine( GetClubs( clubName, onComplete ) );
    }

    public void GetCardsRequest( Action<CardsData.CardData[]> onComplete = null )
    {
        if( finishLoadCards )
            StopCoroutine( GetCards( onComplete ) );
        StartCoroutine( GetCards( onComplete ) );
    }

    public void SendGpsData( LocationData locInfo, Action onComplete = null )
    {
        StartCoroutine( PostUserLocation( JsonUtility.ToJson( locInfo ), onComplete ) );
    }

    private IEnumerator GetAuthToken()
    {
        while( IsConnected == false )
        {
            _deviceID = IsNewUser ? Guid.NewGuid().ToString() : _deviceID;
            var token = new Token( _deviceID );
            var rawBytes = Encoding.UTF8.GetBytes( Token.ToJson(token) );
            Debug.Log( "Getting authorization token using this id \"" + _deviceID + "\"" );

            _request = new UnityWebRequest(AuthorizationUrl, "POST")
            {
                uploadHandler = new UploadHandlerRaw(rawBytes),
                downloadHandler = new DownloadHandlerBuffer()
            };

            _request.SetRequestHeader( "Content-Type", "application/json" );

            yield return _request.SendWebRequest();

            IsConnected = _request.responseCode == 200L;
            _authToken = _request.downloadHandler.text;
            Debug.Log( "Calling other requests are allowed? " + IsConnected );
            yield return new WaitForSeconds( 15 );
        }
    }

    private IEnumerator GetUser( string userId = "", Action<UserData> onComplete = null )
    {
        StringBuilder url = new StringBuilder();
        if( !string.IsNullOrEmpty( userId ) )
            url.Append( GetUserUrl ).Append( userId );
        else
            url.Append( GetMyUserUrl );

        _request = new UnityWebRequest( url.ToString(), "GET" );

        SetRequestInfo();

        yield return _request.SendWebRequest();

        Debug.Log( _request.downloadHandler.text );
        var usr = UserData.CreateFromJson( _request.downloadHandler.text );
        onComplete?.Invoke( usr );
        Debug.Log( usr.ToString() );
    }

    private IEnumerator GetCards( Action<CardsData.CardData[]> onComplete = null )
    {
        _request = new UnityWebRequest( GetCardsUrl, "GET" );
        SetRequestInfo();

        yield return _request.SendWebRequest();

        StringBuilder response = new StringBuilder();
        response.Append( _request.downloadHandler.text ).Insert( 0, "{\"cards\":" ).Append( '}' );
        var cards = CardsData.CreateFromJson( response.ToString() );
        onComplete?.Invoke( cards.cards );
    }

    private IEnumerator GetClubs( string clubId = "", Action<ClubsData.ClubData[]> onComplete = null )
    {
        Debug.Log( "GetClubs Started > " );
        StringBuilder url = new StringBuilder();
        if( !string.IsNullOrEmpty( clubId ) )
            url.Append( GetClubUrl + clubId );
        else
            url.Append( GetAllClubsUrl );

        _request = new UnityWebRequest( url.ToString(), "GET" );

        SetRequestInfo();

        yield return _request.SendWebRequest();
        Debug.Log( "GetClubs SendWebRequest > " );

        StringBuilder response = new StringBuilder();
        response.Append( _request.downloadHandler.text ).Insert( 0, "{\"clubs\":" ).Append( '}' );
        var clubs = ClubsData.CreateFromJson( response.ToString() );
        DataManager.Instance.DownloadSprites( clubs );
        onComplete?.Invoke( clubs.clubs );
        Debug.Log( "GetClubs Finished > " );

    }

    private IEnumerator PostUserName( string newUserName, Action onComplete = null )
    {
        _request = new UnityWebRequest( PostUsernameUrl, "POST" );
        SetRequestInfo( newUserName );
        yield return _request.SendWebRequest();
        Debug.Log( _request.downloadHandler.text );

        onComplete?.Invoke();
    }

    private IEnumerator PostClub(string clubId, Action onComplete = null)
    {
        _request = new UnityWebRequest(PostClubUrl, "POST");
        SetRequestInfo(clubId);
        yield return _request.SendWebRequest();
        Debug.Log(_request.downloadHandler.text);

        onComplete?.Invoke();
    }

    private IEnumerator PostUserLocation(string locJson, Action onComplete = null)
    {
        _request = new UnityWebRequest(PostUserLocationUrl, "POST");
        SetRequestInfo(locJson);
        yield return _request.SendWebRequest();
        Debug.Log(_request.downloadHandler.text);

        onComplete?.Invoke();
    }
}