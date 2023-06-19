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

        Managers.currScene = (int)Define.Scene.Stage;

        SetScreen();
    }

    public override void Clear()
    {
        //����� �÷��̾��� ��ų����, Hp, ��� �ʱ�ȭ ��Ű�°� �´��� Ȯ���ؾ���
    }

    public void SetScreen()
    {
        player = Managers.Object.CreateObject("Player");
        camera.GetComponent<CameraController>().SetPlayer();//ī�޶� �÷��̾� ����
        player.GetComponent<Player>().SetCamera();//�÷��̾ ī�޶� ����

        TargetSpotUI ts = Managers.UI.ShowSceneUI<TargetSpotUI>("TargetSpotUI");//����� UI
        ts.transform.SetParent(canvas, false);

        fieldMoney = Util.FindChild<TextMeshProUGUI>(Managers.Resource.Instantiate("UI/Scene/FieldMoney", canvas), null, true);//��ȭâ ���� & ����
        fieldDiamond = Util.FindChild<TextMeshProUGUI>(Managers.Resource.Instantiate("UI/Scene/FieldDiamond", canvas), null, true);//�پ߸��â ���� & ����
        Managers.Resource.Instantiate("UI/Scene/PauseButton", canvas);

        //���������� �°� �� ����
        switch (Managers.currStage)
        {
            case (int)Define.Stage.Stage01: Managers.UI.ShowSceneUI<StageBG>("Stage01_BG"); break;//���
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
