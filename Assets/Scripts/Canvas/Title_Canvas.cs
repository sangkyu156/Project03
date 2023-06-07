using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Title_Canvas : UI_Base
{
    enum Buttons
    {
        StartButton,
        SetupButton,
        QuitButton,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));//Dictionary에 버튼 종류를 저장함

        GetButton((int)Buttons.StartButton).gameObject.BindEvent(Button1);
        GetButton((int)Buttons.SetupButton).gameObject.BindEvent(Button2);
        GetButton((int)Buttons.QuitButton).gameObject.BindEvent(Button3);
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    public void Button1(PointerEventData data)
    {
        Debug.Log("버튼1 눌림");
    }

    public void Button2(PointerEventData data)
    {
        Debug.Log("버튼2 눌림");
    }

    public void Button3(PointerEventData data)
    {
        Debug.Log("버튼3 눌림");
    }
}
