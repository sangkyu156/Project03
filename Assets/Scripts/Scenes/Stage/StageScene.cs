using TMPro;
using UnityEngine;

public class StageScene : BaseScene
{
    public new GameObject camera;
    public Transform canvas;
    public GameObject ts;
    GameObject player;
    GameObject boss;
    GameObject fieldMoney;
    TextMeshProUGUI fieldMoney_Text;
    TextMeshProUGUI fieldDiamond;

    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        Managers.currScene = (int)Define.Scene.Stage;
        Managers.Instance.GetStageScene();

        SetScreen();
        PrintFieldMoney();
    }

    public override void Clear()
    {
        //여기다 플레이어의 스킬레벨, Hp, 등등 초기화 시키는게 맞는지 확인해야함
    }

    public void SetScreen()
    {
        player = Managers.Resource.Instantiate("Object/Player");
        camera.GetComponent<CameraController>().SetPlayer();//카메라에 플레이어 연결
        player.GetComponent<Player>().SetCamera();//플레이어에 카메라 연결

        ts = Managers.Resource.Instantiate("UI/Scene/TargetSpotUI");//진행률 UI
        ts.transform.SetParent(canvas, false);

        fieldMoney = Managers.Resource.Instantiate("UI/Scene/FieldMoney", canvas);//금화창 생성
        fieldMoney_Text = Util.FindChild<TextMeshProUGUI>(fieldMoney, null, true);//금화창 연결
        fieldDiamond = Util.FindChild<TextMeshProUGUI>(Managers.Resource.Instantiate("UI/Scene/FieldDiamond", canvas), null, true);//다야몬드창 생성 & 연결
        Managers.Resource.Instantiate("UI/Scene/PauseButton", canvas);

        boss = Managers.Resource.Instantiate("Object/Boss");//보스생성
        Managers.Instance.FindBoss();

        GameObject bi = Managers.Resource.Instantiate("UI/Scene/BossImage");//보스 UI
        bi.transform.SetParent(canvas, false);

        Managers.Resource.Instantiate("UI/Scene/Information");//안내UI

        Managers.Resource.Instantiate("Object/FirstPortal");//첫 포탈상점


        //스테이지에 맞게 맵 구현
        switch (Managers.currStage)
        {
            case (int)Define.Stage.Stage01: Managers.Resource.Instantiate("UI/Scene/Stage01_BG");Debug.Log("123"); break;//배경
            case (int)Define.Stage.Stage02: break;
            case (int)Define.Stage.Stage03: break;
            case (int)Define.Stage.Stage04: break;
            case (int)Define.Stage.Stage05: break;
        }
    }

    public void FieldMoneyLastSibling()
    {
        fieldMoney.transform.SetAsLastSibling();
    }

    public void PrintFieldMoney()
    {
        fieldMoney_Text.text = Managers.fieldMoney.ToString();
    }
}
