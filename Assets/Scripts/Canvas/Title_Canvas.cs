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
        Bind<Button>(typeof(Buttons));//Dictionary�� ��ư ������ ������

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
        Debug.Log("��ư1 ����");
    }

    public void Button2(PointerEventData data)
    {
        Debug.Log("��ư2 ����");
    }

    public void Button3(PointerEventData data)
    {
        Debug.Log("��ư3 ����");
    }
}
