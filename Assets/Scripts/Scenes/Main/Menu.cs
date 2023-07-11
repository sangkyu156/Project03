using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    GameObject canvas;

    void Start()
    {
        canvas = gameObject.transform.parent.gameObject;
    }

    void Update()
    {
        
    }

    //StorePopupOn

    public void SetupPopupOn()
    {
        Managers.Sound.Play("Button01");
        GameObject setup = Managers.Resource.Instantiate("UI/Popup/SetupPopup", canvas.transform);
    }

    public void StorePopupOn()
    {
        Managers.Sound.Play("Button01");
        GameObject store = Managers.Resource.Instantiate("UI/Popup/DiaStorePopup", canvas.transform);
    }

    public void AchievementPopupOn()
    {
        Managers.Sound.Play("Button01");
        Managers.Data.AchievementCheck();
        GameObject achievement = Managers.Resource.Instantiate("UI/Popup/AchievementPopup", canvas.transform);
    }

    public void CreditPopupOn()
    {
        Managers.Sound.Play("Button01");
        GameObject credit = Managers.Resource.Instantiate("UI/Popup/CreditPopup", canvas.transform);
    }
}
