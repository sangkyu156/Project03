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

        GetButton((int)Buttons.StartButton).gameObject.BindEvent(StartButton);
        GetButton((int)Buttons.SetupButton).gameObject.BindEvent(SetupButton);
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

    public void SetupButton(PointerEventData data)
    {
        Managers.Resource.Instantiate("UI/Popup/Set-upPopup", this.transform);
    }

    public void Button3(PointerEventData data)
    {
        Application.Quit();
    }

    public void StartButton(PointerEventData data)
    {
        //TODO
        //������ ����� ���Կ� ����Ǿ� �ִ� �����͸� �о�ͼ� ����Ǿ��ִ� �����Ͱ� ������ ����ȭ������ �����ϰ�
        //����Ǿ� �ִ� �����Ͱ� ������ GoIntro() �Լ� ȣ���ؾ���.
        //�����͸� �����ϰ� �ҷ����� ����� ���� �����ɷ� ������Ѿ���.

        //Temp
        GoIntro();
    }

    public void GoIntro()
    {
        //��Ʈ�� ������ �Ѿ����
        Managers.Scene.LoadScene(Define.Scene.Intro);
    }
}
