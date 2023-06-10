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

        //각각의 버튼에 클릭했을때 함수 연결해줌
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
        Debug.Log("버튼1 눌림");
    }

    public void SetupButton(PointerEventData data)
    {
        SetupPopup popup = Managers.UI.ShowPopupUI<SetupPopup>();//SetupPopup 프리팹 팝업 생성
        GameObject go = popup.gameObject;
        go.transform.SetParent(this.transform, false);
    }

    public void Button3(PointerEventData data)
    {
        Application.Quit();
    }

    public void StartButton(PointerEventData data)
    {
        //TODO
        //슬롯을 만들고 슬롯에 저장되어 있는 데이터를 읽어와서 저장되어있는 데이터가 있으면 메인화면으로 가야하고
        //저장되어 있는 데이터가 없으면 GoIntro() 함수 호출해야함.
        //데이터를 저장하고 불러오는 방식을 강의 들은걸로 적용시켜야함.

        //Temp
        GoIntro();
    }

    public void GoIntro()
    {
        //인트로 씬으로 넘어가야함
        Managers.Scene.LoadScene(Define.Scene.Intro);
        Managers.currStage = (int)Define.Stage.Stage01;
    }
}
