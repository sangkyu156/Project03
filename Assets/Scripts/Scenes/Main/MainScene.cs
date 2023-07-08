using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainScene : BaseScene
{
    GameObject canvas;
    GameObject diamond;


    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas");

        Init();
    }

    void Update()
    {
        
    }

    protected override void Init()
    {
        base.Init();

        Managers.currScene = (int)Define.Scene.Main;
        Managers.Instance.GetMainScene();

        SetScreen();
        PrintDiamond();
    }

    public void SetScreen()
    {
        GameObject menu = Managers.Resource.Instantiate("UI/Scene/Menu", canvas.transform);//메뉴 1,2,3,4 버튼생성
        GameObject playButton = Managers.Resource.Instantiate("UI/Scene/PlayButton", canvas.transform);//Playe 버튼
        GameObject exitButton = Managers.Resource.Instantiate("UI/Scene/ExitButton", canvas.transform);//Exit 버튼
        diamond = Managers.Resource.Instantiate("UI/Scene/MainDiamond", canvas.transform);//diamond Text
    }

    public void PrintDiamond()
    {
        diamond.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Managers.diamond.ToString();
    }

    public override void Clear()
    {
        
    }


}
