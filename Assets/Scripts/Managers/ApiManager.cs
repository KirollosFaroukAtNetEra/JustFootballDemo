using Assets.Scripts;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ApiManager : BaseManager<ApiManager>
{
    private const string _apiUrl = "https://demo.dev.justfootball.io/api";
    private const string _authorizationUrl = _apiUrl + "/auth/token";
    private const string _getMyUserUrl = _apiUrl + "/user/me";
    private const string _getUserUrl = _apiUrl + "/user/get/"; // + user id
    private const string _setUsernameUrl = _apiUrl + "/user/set/username";
    private const string _setClubUrl = _apiUrl + "/user/set/club";
    private const string _updateUserLocationUrl = _apiUrl + "/user/set/location";
    private const string _getCardsUrl = _apiUrl + "/cards";
    private const string _getAllClubsUrl = _apiUrl + "/clubs";
    private const string _getClubUrl = _apiUrl + "/club/"; // + club id

    private static string _deviceID;
    private static string _authToken;

    private UnityWebRequest request;

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
        StartCoroutine(GetAuthentication());
        IsReady = true;
    }

    private IEnumerator GetAuthentication()
    {
        while (IsConnected == false)
        {
            _deviceID = IsNewUser ? Guid.NewGuid().ToString() : _deviceID;
            var token = new Token(_deviceID);
            var rawBytes = Encoding.UTF8.GetBytes(token.ToJson());
            Log("Getting authorization token using this id \"" + _deviceID + "\"");

            request = new UnityWebRequest(_authorizationUrl, "POST");
            request.uploadHandler = new UploadHandlerRaw(rawBytes);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            IsConnected = request.responseCode == 200L;
            _authToken = request.downloadHandler.text;
            Log("Calling other requests are allowed? " + IsConnected);
            yield return new WaitForSeconds(15);
        }
    }

    private void SetRequestInfo(string jsonBody = "")
    {
        request.SetRequestHeader("Authorization", $"Bearer {_authToken}");
        request.downloadHandler = new DownloadHandlerBuffer();

        if (!string.IsNullOrEmpty(jsonBody))
        {
            var rawBytes = Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(rawBytes);

            request.SetRequestHeader("Content-Type", "application/json");
        }
    }

    public void GetUser(UserName userName = null, Action<UserData> onComplete = null)
    {
        string playerName = userName != null ? userName.username : "";
        if (finishLoadUser) StopCoroutine(GetUser(playerName, onComplete));
        StartCoroutine(GetUser(playerName, onComplete));
    }

    public void SetUserName(UserName userName, Action onComplete = null)
    {
        StartCoroutine(SetUserName(userName.ToJson(), onComplete));
    }

    public void SetClub(Club club, Action onComplete = null)
    {
        StartCoroutine(SetClub(club.ToJson(), onComplete));
    }

    public void GetClubs(Club club = null, Action<ClubsData.ClubData[]> onComplete = null)
    {
        string clubName = club != null ? club.club : "" ;
        if (finishLoadClubs) StopCoroutine(GetClubs(clubName, onComplete));
        StartCoroutine(GetClubs(clubName, onComplete));
    }

    public void GetCardss(Action<CardsData.CardData[]> onComplete = null)
    {
        if (finishLoadCards) StopCoroutine(GetCards(onComplete));
        StartCoroutine(GetCards(onComplete));
    }

    public void UpdUsrLocation(LocationData locInfo, Action onComplete = null)
    {
        StartCoroutine(UpdUserLocation(JsonUtility.ToJson(locInfo), onComplete));
    }

    private IEnumerator GetUser(string userId = "", Action<UserData> onComplete = null)
    {
        StringBuilder url = new StringBuilder();
        if (!string.IsNullOrEmpty(userId))
            url.Append(_getUserUrl).Append(userId);
        else
            url.Append(_getMyUserUrl);

        request = new UnityWebRequest(url.ToString(), "GET");

        SetRequestInfo();

        yield return request.SendWebRequest();

        Log(request.downloadHandler.text);
        var usr = JsonUtility.FromJson<UserData>(request.downloadHandler.text);
        onComplete?.Invoke(usr);
        Log(usr.ToString());
    }

    private IEnumerator SetUserName(string newUserName, Action onComplete = null)
    {
        request = new UnityWebRequest(_setUsernameUrl, "POST");
        SetRequestInfo(newUserName);
        yield return request.SendWebRequest();
        Log(request.downloadHandler.text);

        onComplete?.Invoke();
    }

    private IEnumerator SetClub(string clubId, Action onComplete = null)
    {
        request = new UnityWebRequest(_setClubUrl, "POST");
        SetRequestInfo(clubId);
        yield return request.SendWebRequest();
        Log(request.downloadHandler.text);

        onComplete?.Invoke();
    }

    private IEnumerator UpdUserLocation(string locJson, Action onComplete = null)
    {
        request = new UnityWebRequest(_updateUserLocationUrl, "POST");
        SetRequestInfo(locJson);
        yield return request.SendWebRequest();
        Log(request.downloadHandler.text);

        onComplete?.Invoke();
    }

    private IEnumerator GetCards(Action<CardsData.CardData[]> onComplete = null)
    {
        request = new UnityWebRequest(_getCardsUrl, "GET");
        SetRequestInfo();

        yield return request.SendWebRequest();

        StringBuilder response = new StringBuilder();
        response.Append(request.downloadHandler.text).Insert(0, "{\"cards\":").Append('}');
        var cards = JsonUtility.FromJson<CardsData>(response.ToString());
        onComplete?.Invoke(cards.cards);
    }

    private IEnumerator GetClubs(string clubId = "", Action<ClubsData.ClubData[]> onComplete = null)
    {
        Debug.Log("GetClubs Started > ");
        StringBuilder url = new StringBuilder();
        if (!string.IsNullOrEmpty(clubId))
            url.Append(_getClubUrl + clubId);
        else
            url.Append(_getAllClubsUrl);

        request = new UnityWebRequest(url.ToString(), "GET");

        SetRequestInfo();

        yield return request.SendWebRequest();
        Debug.Log("GetClubs SendWebRequest > ");

        StringBuilder response = new StringBuilder();
        response.Append(request.downloadHandler.text).Insert(0, "{\"clubs\":").Append('}');
        var clubs = JsonUtility.FromJson<ClubsData>(response.ToString());
        DataManager.Instance.DownloadSprites(clubs);
        onComplete?.Invoke(clubs.clubs);
        Debug.Log("GetClubs Finished > ");

    }

    private void Log(string strToLog)
    {
        Debug.Log("[ApiManager] " + strToLog);
    }

}