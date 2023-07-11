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

    void Update()
    {
        
    }

    public void SetupButton(PointerEventData data)
    {
        Managers.Sound.Play("Button01");

        GameObject popup = Managers.Resource.Instantiate("UI/Popup/SetupPopup");//SetupPopup 프리팹 팝업 생성
        popup.transform.SetParent(this.transform, false);
    }

    public void QuitButton(PointerEventData data)
    {
        Managers.Sound.Play("Button01");
        Application.Quit();
    }

    public void StartButton(PointerEventData data)
    {
        Managers.Sound.Play("Button01");

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
