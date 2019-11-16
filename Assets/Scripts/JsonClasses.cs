using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class JsonClass<T>
    {
        public static string ToJson( JsonClass<T> obj )
        {
            return JsonUtility.ToJson( obj );
        }

        public static T CreateFromJson( string jsonString )
        {
            return JsonUtility.FromJson<T>( jsonString );
        }
    }

    public class Token : JsonClass<Token>
    {
        // This will be User id
        public string clientToken;

        public Token( string uniqueId )
        {
            clientToken = uniqueId;
        }
    }

    public class UserName : JsonClass<UserName>
    {
        public string username;

        public UserName( string name )
        {
            username = name;
        }
    }

    public class Club : JsonClass<Club>
    {
        public string club;

        public Club( string club )
        {
            this.club = club;
        }
    }

    public class GPS : JsonClass<GPS>
    {
        public float lat;
        public float lng;

        public GPS( float latitude, float longitude )
        {
            lat = latitude;
            lng = longitude;
        }
    }

    [Serializable]
    public class LocationData : JsonClass<LocationData>
    {
        public double lat;
        public double lng;
    }

    [Serializable]
    public class UserData : JsonClass<UserData>
    {
        public string username;
        public string club;
        public LocationData location;
        public string pictureUrl;
        public string clubPictureUrl;
    }

    [Serializable]
    public class CardsData : JsonClass<CardsData>
    {
        [Serializable]
        public class CardData
        {
            public string id;
            public string username;
            public string pictureUrl;
            public string clubPictureUrl;
        }

        public CardData[] cards;
    }

    [Serializable]
    public class ClubsData : JsonClass<ClubsData>
    {
        [Serializable]
        public class ClubData
        {
            public string id;
            public string logoUrl;
            public string name;
            public string league;
        }
        public ClubData[] clubs;

        public Dictionary<string, Texture2D> clubsSprites;
    }
}
