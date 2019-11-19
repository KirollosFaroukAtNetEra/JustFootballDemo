using UnityEngine;
using UnityEngine.UI;

public class AlertMessage : MonoBehaviour
{
    public Text AlertMessageText;

    public void SetAlertMessage( string alertMessage )
    {
        AlertMessageText.text = alertMessage;
    }
}
