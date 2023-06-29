using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageCanvas : UI_Base
{
    enum Buttons//버튼오브젝트 이름과 같아야 찾을수있음
    {
        PauseButton,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));//Dictionary에 버튼 종류를 저장함

        //각각의 버튼에 클릭했을때 함수 연결해줌
        GetButton((int)Buttons.PauseButton).gameObject.BindEvent(PauseButton);
    }

    void Start()
    {
        Init();
    }

    public void PauseButton(PointerEventData data)
    {
        Time.timeScale = 0;
        Managers.Resource.Instantiate("UI/Popup/PausePopup", this.transform);
    }
}
