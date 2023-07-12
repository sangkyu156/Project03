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

        Managers.Sound.Play("TitleBGM", Define.Sound.Bgm);
    }

    public void StartButton(PointerEventData data)
    {
        Managers.Sound.Play("Button01");

        GameObject popup = Managers.Resource.Instantiate("UI/Popup/SavedFilePopup", transform);
    }

    public void SetupButton(PointerEventData data)
    {
        Managers.Sound.Play("Button01");

        GameObject popup = Managers.Resource.Instantiate("UI/Popup/SetupPopup", transform);//SetupPopup ������ �˾� ����
    }

    public void QuitButton(PointerEventData data)
    {
        Managers.Sound.Play("Button01");
        Application.Quit();
    }
}
