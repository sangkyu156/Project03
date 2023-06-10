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

        //���������� �°� �� ����
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
        //����� �÷��̾��� ��ų����, Hp, ��� �ʱ�ȭ ��Ű�°� �´��� Ȯ���ؾ���
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
