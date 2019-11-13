using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler,IPointerClickHandler
{

    [SerializeField]
    [Tooltip("How long must pointer be down on this object to trigger a long press")]
    private float holdTime = 5f;
    private float longPressDelay = 0.5f;

    private bool held = false;
    public bool AllowLongPress = false;
    public UnityEvent onClick = new UnityEvent();
    public UnityEvent onLongPress = new UnityEvent();
    public UnityEvent onLongPressCanceled = new UnityEvent();
    public UnityEvent onPointerDown = new UnityEvent();
    public UnityEvent onLongPressStart = new UnityEvent();

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown.Invoke();
        if (!AllowLongPress)
            return;
        held = false;
        Invoke("OnLongPress", holdTime + longPressDelay);
        Invoke("OnLongPressStarted", longPressDelay);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!AllowLongPress)
            return;
        if (held)
            onLongPressCanceled.Invoke();
        CancelInvoke("OnLongPress");
        CancelInvoke("OnLongPressStarted");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
    }
    private void OnLongPress()
    {
        held = true;
        onLongPress.Invoke();
    }
    private void OnLongPressStarted()
    {
        held = true;
        onLongPressStart.Invoke();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!held || !AllowLongPress)
            onClick.Invoke();
    }
}
