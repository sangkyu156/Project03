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
        //GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        GameObject setup = Managers.Resource.Instantiate("UI/Popup/SetupPopup", canvas.transform);
    }

    public void StorePopupOn()
    {
        //GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        GameObject store = Managers.Resource.Instantiate("UI/Popup/DiaStorePopup", canvas.transform);
    }
}
