using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    GameObject canvas;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas");
    }

    public void StagePopupOn()
    {
        Managers.Sound.Play("Button01");
        Managers.Resource.Instantiate("UI/Popup/StagePopup", canvas.transform);
    }
}
