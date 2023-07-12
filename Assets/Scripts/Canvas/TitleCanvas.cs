using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleCanvas : UI_Base
{
    enum Buttons//버튼오브젝트 이름과 같아야 찾을수있음
    {
        StartButton,
        SetupButton,
        QuitButton,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));//Dictionary에 버튼 종류를 저장함

        //각각의 버튼에 클릭했을때 함수 연결해줌
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

        GameObject popup = Managers.Resource.Instantiate("UI/Popup/SetupPopup", transform);//SetupPopup 프리팹 팝업 생성
    }

    public void QuitButton(PointerEventData data)
    {
        Managers.Sound.Play("Button01");
        Application.Quit();
    }
}
