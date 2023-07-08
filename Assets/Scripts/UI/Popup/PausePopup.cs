using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PausePopup : UI_Popup
{
    enum Buttons//버튼오브젝트 이름과 같아야 찾을수있음
    {
        SetupButton,
        MeueButton,
        ResumeButton,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));//Dictionary에 버튼 종류를 저장함

        //각각의 버튼에 클릭했을때 함수 연결해줌
        GetButton((int)Buttons.SetupButton).gameObject.BindEvent(SetupButton);
        GetButton((int)Buttons.MeueButton).gameObject.BindEvent(MeueButton);
        GetButton((int)Buttons.ResumeButton).gameObject.BindEvent(ResumeButton);
    }

    void Start()
    {
        Init();
    }

    public void SetupButton(PointerEventData data)
    {
        Managers.Resource.Instantiate("UI/Popup/SetupPopup", this.transform);
    }

    public void MeueButton(PointerEventData data)
    {
        Time.timeScale = 1;
        Managers.Scene.LoadScene(Define.Scene.Main);
    }

    public void ResumeButton(PointerEventData data)
    {
        Time.timeScale = 1;
        Managers.Resource.Destroy(gameObject);
    }
}
