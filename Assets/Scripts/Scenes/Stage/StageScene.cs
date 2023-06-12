using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageScene : BaseScene
{
    public new GameObject camera;
    public Transform canvas;
    GameObject player;
    TextMeshProUGUI fieldMoney;
    TextMeshProUGUI fieldDiamond;

    private void Awake()
    {

    }

    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Stage;

        SetScreen();
    }

    public override void Clear()
    {
        //여기다 플레이어의 스킬레벨, Hp, 등등 초기화 시키는게 맞는지 확인해야함
    }

    public void SetScreen()
    {
        player = Managers.Object.CreateObject("Player");
        camera.GetComponent<CameraController>().SetPlayer();//카메라에 플레이어 연결
        player.GetComponent<Player>().SetCamera();//플레이어에 카메라 연결

        TargetSpotUI ts = Managers.UI.ShowSceneUI<TargetSpotUI>("TargetSpotUI");//진행률 UI
        ts.transform.SetParent(canvas, false);

        fieldMoney = Util.FindChild<TextMeshProUGUI>(Managers.Resource.Instantiate("UI/Scene/FieldMoney", canvas), null, true);//금화창 생성 & 연결
        fieldDiamond = Util.FindChild<TextMeshProUGUI>(Managers.Resource.Instantiate("UI/Scene/FieldDiamond", canvas), null, true);//다야몬드창 생성 & 연결
        Managers.Resource.Instantiate("UI/Scene/PauseButton", canvas);

        //스테이지에 맞게 맵 구현
        switch (Managers.currStage)
        {
            case (int)Define.Stage.Stage01: Managers.UI.ShowSceneUI<StageBG>("Stage01_BG"); break;//배경
            case (int)Define.Stage.Stage02: break;
            case (int)Define.Stage.Stage03: break;
            case (int)Define.Stage.Stage04: break;
            case (int)Define.Stage.Stage05: break;
        }
    }

    void UISite()
    {

    }
}
