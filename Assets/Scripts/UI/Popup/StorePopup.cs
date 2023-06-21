using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePopup : UI_Popup
{
    public void PopupClose()
    {
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }
}
