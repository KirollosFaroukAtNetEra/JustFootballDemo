using System;

[Serializable]
public class Token
{
    public string clientToken;

    public Token(string uniqueId)
    {
        clientToken = uniqueId;
    }
}

[Serializable]
public class UserName
{
    public string username;

    public UserName(string name)
    {
        username = name;
    }
}

[Serializable]
public class Club
{
    public string club;

    public Club(string club)
    {
        this.club = club;
    }
}

[Serializable]
public class GPS
{
    public float lat;
    public float lng;

    public GPS(float latitude, float longitude)
    {
        lat = latitude;
        lng = longitude;
    }
}

[Serializable]
public class LocationData
{
    public double lat;
    public double lng;
}

[Serializable]
public class UserData
{
    public string username;
    public string club;
    public LocationData location;
    public string pictureUrl;
    public string clubPictureUrl;
}

[Serializable]
public class CardData
{
    public string id;
    public string username;
    public string pictureUrl;
    public string clubPictureUrl;
}

[Serializable]
public class ClubData
{
    public string id;
    public string logoUrl;
    public string name;
    public string league;
}
