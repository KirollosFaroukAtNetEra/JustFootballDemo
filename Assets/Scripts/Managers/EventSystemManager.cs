using System.Collections.Generic;

public class GameEvent
{
}

public class Events
{
    private static Events _instanceInternal = null;
    public static Events Instance => _instanceInternal ?? ( _instanceInternal = new Events() );

    public delegate void EventDelegate<T>( T e ) where T : GameEvent;

    private delegate void EventDelegate( GameEvent e );

    private Dictionary<System.Type, EventDelegate> _delegates = new Dictionary<System.Type, EventDelegate>();
    private Dictionary<System.Delegate, EventDelegate>
        _delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

    public void AddListener<T>( EventDelegate<T> del ) where T : GameEvent
    {
        // Early-out if we've already registered this delegate
        if( _delegateLookup.ContainsKey( del ) )
            return;

        // Create a new non-generic delegate which calls our generic one.
        // This is the delegate we actually invoke.
        EventDelegate internalDelegate = ( e ) => del( (T) e );
        _delegateLookup[ del ] = internalDelegate;

        if( _delegates.TryGetValue( typeof( T ), out var tempDel ) )
        {
            _delegates[ typeof( T ) ] = tempDel += internalDelegate;
        }
        else
        {
            _delegates[ typeof( T ) ] = internalDelegate;
        }
    }

    public void RemoveListener<T>( EventDelegate<T> del ) where T : GameEvent
    {
        if( _delegateLookup.TryGetValue( del, out var internalDelegate ) )
        {
            if( _delegates.TryGetValue( typeof( T ), out var tempDel ) )
            {
                tempDel -= internalDelegate;
                if( tempDel == null )
                {
                    _delegates.Remove( typeof( T ) );
                }
                else
                {
                    _delegates[ typeof( T ) ] = tempDel;
                }
            }

            _delegateLookup.Remove( del );
        }
    }

    public void Raise( GameEvent e )
    {
        if( _delegates.TryGetValue( e.GetType(), out var del ) )
        {
            del.Invoke( e );
        }
    }
}

public class ClubDataUpdated : GameEvent
{
    public string ClubName;

    public ClubDataUpdated(string clubName)
    {
        ClubName = clubName;
    }
}

public class ProfileNameUpdated : GameEvent
{
    public string ProfileName;

    public ProfileNameUpdated( string newProfileName )
    {
        ProfileName = newProfileName;
    }
}