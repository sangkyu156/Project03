using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleCanvas : UI_Base
{
    enum Buttons//��ư������Ʈ �̸��� ���ƾ� ã��������
    {
        StartButton,
        SetupButton,
        QuitButton,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));//Dictionary�� ��ư ������ ������

        //������ ��ư�� Ŭ�������� �Լ� ��������
        GetButton((int)Buttons.StartButton).gameObject.BindEvent(StartButton);
        GetButton((int)Buttons.SetupButton).gameObject.BindEvent(SetupButton);
        GetButton((int)Buttons.QuitButton).gameObject.BindEvent(QuitButton);
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    public void SetupButton(PointerEventData data)
    {
        GameObject popup = Managers.Resource.Instantiate("UI/Popup/SetupPopup");//SetupPopup ������ �˾� ����
        popup.transform.SetParent(this.transform, false);
    }

    public void QuitButton(PointerEventData data)
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
        Managers.currStage = (int)Define.Stage.Stage01;
    }
}
