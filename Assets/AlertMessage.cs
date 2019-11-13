using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AlertMessage : MonoBehaviour
{
    public Text AlertMessageText;

    public void SetAlertMessage(string alertMessage)
    {
        AlertMessageText.text = alertMessage;
    }
}
