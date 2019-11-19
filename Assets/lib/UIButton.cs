using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    [Tooltip( "How long must pointer be down on this object to trigger a long press" )]
    private float _holdTime = 5f;
    private float _longPressDelay = 0.5f;
    private bool _held = false;

    public bool AllowLongPress = false;
    public UnityEvent onClick = new UnityEvent();
    public UnityEvent onLongPress = new UnityEvent();
    public UnityEvent onLongPressCanceled = new UnityEvent();
    public UnityEvent onPointerDown = new UnityEvent();
    public UnityEvent onLongPressStart = new UnityEvent();

    public void OnPointerDown( PointerEventData eventData )
    {
        onPointerDown.Invoke();

        if( !AllowLongPress )
            return;

        _held = false;
        Invoke( nameof( OnLongPress ), _holdTime + _longPressDelay );
        Invoke( nameof( OnLongPressStarted ), _longPressDelay );
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        if( !AllowLongPress )
            return;

        if( _held )
            onLongPressCanceled.Invoke();

        CancelInvoke( nameof( OnLongPress ) );
        CancelInvoke( nameof( OnLongPressStarted ) );
    }

    public void OnPointerExit( PointerEventData eventData )
    { }

    private void OnLongPress()
    {
        _held = true;
        onLongPress.Invoke();
    }

    private void OnLongPressStarted()
    {
        _held = true;
        onLongPressStart.Invoke();
    }

    public void OnPointerClick( PointerEventData eventData )
    {
        if( !_held || !AllowLongPress )
            onClick.Invoke();
    }
}
