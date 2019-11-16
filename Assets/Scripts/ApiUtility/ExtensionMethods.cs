using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Wrapper<T>
{
    public T[] Items;
}

public static class ExtensionMethods
{
    public static string ToJson<T>( this T obj )
    {
        return JsonUtility.ToJson( obj );
    }

    public static T CreateFromJson<T>( this string jsonString )
    {
        return JsonUtility.FromJson<T>( jsonString );
    }

    public static T[] CreateArrayFromJson<T>( this string arrayJsonString )
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>( "{\"Items\":" + arrayJsonString + "}" );
        return wrapper.Items;
    }

    public static void SetRequestInfo( this UnityWebRequest req, string jsonBody = "" )
    {
        req.SetRequestHeader( "Authorization", $"Bearer {ApiManager._authToken}" );
        req.downloadHandler = new DownloadHandlerBuffer();

        if( !string.IsNullOrEmpty( jsonBody ) )
        {
            var rawBytes = Encoding.UTF8.GetBytes( jsonBody );
            req.uploadHandler = new UploadHandlerRaw( rawBytes );

            req.SetRequestHeader( "Content-Type", "application/json" );
        }
    }
}
