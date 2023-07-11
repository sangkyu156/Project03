using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageCanvas : UI_Base
{
    enum Buttons//��ư������Ʈ �̸��� ���ƾ� ã��������
    {
        PauseButton,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));//Dictionary�� ��ư ������ ������

        //������ ��ư�� Ŭ�������� ȣ���ϴ� �Լ� ��������
        GetButton((int)Buttons.PauseButton).gameObject.BindEvent(PauseButton);
    }

    void Start()
    {
        Managers.Sound.Play("StageBGM", Define.Sound.Bgm);

        Init();
    }

    public void PauseButton(PointerEventData data)
    {
        Managers.Sound.Play("Button01");
        Time.timeScale = 0;
        Managers.Resource.Instantiate("UI/Popup/PausePopup", this.transform);
    }
}
