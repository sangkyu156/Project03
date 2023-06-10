using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScene : BaseScene
{
    public new GameObject camera;

    private void Awake()
    {

    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Stage;

        //스테이지에 맞게 맵 구현
        switch (Managers.currStage)
        {
            case (int)Define.Stage.Stage01:
                Managers.Object.CreateObject("Player");
                SetScreen();
                break;
            case (int)Define.Stage.Stage02:
                break;
            case (int)Define.Stage.Stage03:
                break;
            case (int)Define.Stage.Stage04:
                break;
            case (int)Define.Stage.Stage05:
                break;
        }
    }

    public override void Clear()
    {
        //여기다 플레이어의 스킬레벨, Hp, 등등 초기화 시키는게 맞는지 확인해야함
    }

    public void SetScreen()
    {
        camera.GetComponent<CameraController>().SetPlayer();
        Managers.UI.ShowSceneUI<StageBG>("Stage01_BG");
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }
}
