using System.Collections;
using UnityEngine;

public enum LocationState
{
    Disabled,
    TimedOut,
    Failed,
    Enabled
}

public class GPSManager : BaseManager<GPSManager>
{
    public LocationInfo LastData;
    private const float EarthRadius = 6371;
    private LocationState _state;

    public override void Initialize()
    {
        StartCoroutine( InitializeGpsService() );
        IsReady = true;
    }

    private IEnumerator InitializeGpsService()
    {
        if( !Input.location.isEnabledByUser )
        {
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while( Input.location.status == LocationServiceStatus.Initializing && maxWait > 0 )
        {
            yield return new WaitForSeconds( 1 );
            maxWait--;
        }

        if( maxWait < 1 )
        {
            print( "Timed out" );
            _state = LocationState.TimedOut;
            yield break;
        }

        if( Input.location.status == LocationServiceStatus.Failed )
        {
            print( "Unable to determine device location" );
            _state = LocationState.Failed;
        }
        else
        {
            _state = LocationState.Enabled;
            LastData = Input.location.lastData;
        }
    }

    //get delta movement
    private static float Haversine( float lastLatitude, float lastLongitude )
    {
        var newLatitude = Input.location.lastData.latitude;
        var newLongitude = Input.location.lastData.longitude;
        var deltaLatitude = ( newLatitude - lastLatitude ) * Mathf.Deg2Rad;
        var deltaLongitude = ( newLongitude - lastLongitude ) * Mathf.Deg2Rad;
        var a = Mathf.Pow( Mathf.Sin( deltaLatitude / 2 ), 2 ) +
            Mathf.Cos( lastLatitude * Mathf.Deg2Rad ) *
            Mathf.Cos( newLatitude * Mathf.Deg2Rad ) *
            Mathf.Pow( Mathf.Sin( deltaLongitude / 2 ), 2 );
        var c = 2 * Mathf.Atan2( Mathf.Sqrt( a ), Mathf.Sqrt( 1 - a ) );
        return EarthRadius * c;
    }

    private void Update()
    {
        if( _state != LocationState.Enabled )
        {
            return;
        }

        var deltaDistance = Haversine( LastData.latitude, LastData.longitude ) * 1000f;

        if( !( deltaDistance > 0f ) )
        {
            return;
        }

        LastData = Input.location.lastData;

        var newLocation = new LocationData
        {
            lat = LastData.latitude,
            lng = LastData.longitude
        };

        ApiManager.Instance.SendGpsData( newLocation );
    }
}
