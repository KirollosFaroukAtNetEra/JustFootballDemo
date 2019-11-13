using UnityEngine;

namespace Assets.Scripts
{
    public abstract class JsonClass
    {
        public virtual string ToJson()
        {
            return "{}";
        }
    }

    public class Token : JsonClass
    {
        // This will be User id
        public string clientToken;

        public Token(string uniqueId)
        {
            clientToken = uniqueId;
        }
        
        public override string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }

    public class UserName : JsonClass
    {
        public string username;

        public UserName(string name)
        {
            username = name;
        }

        public override string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }

    public class Club : JsonClass
    {
        public string club;

        public Club(string club)
        {
            this.club = club;
        }

        public override string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }

    public class GPS : JsonClass
    {
        public float lat;
        public float lng;

        public GPS(float latitude, float longtitude)
        {
            lat = latitude;
            lng = longtitude;
        }

        public override string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
