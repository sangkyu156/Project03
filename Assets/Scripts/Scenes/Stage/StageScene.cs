using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageScene : BaseScene
{
    public new GameObject camera;
    public Transform canvas;
    GameObject player;
    GameObject boss;
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
        player = Managers.Resource.Instantiate("Object/Player");
        camera.GetComponent<CameraController>().SetPlayer();//ī�޶� �÷��̾� ����
        player.GetComponent<Player>().SetCamera();//�÷��̾ ī�޶� ����

        GameObject ts = Managers.Resource.Instantiate("UI/Scene/TargetSpotUI");//����� UI
        ts.transform.SetParent(canvas, false);

        fieldMoney = Util.FindChild<TextMeshProUGUI>(Managers.Resource.Instantiate("UI/Scene/FieldMoney", canvas), null, true);//��ȭâ ���� & ����
        fieldDiamond = Util.FindChild<TextMeshProUGUI>(Managers.Resource.Instantiate("UI/Scene/FieldDiamond", canvas), null, true);//�پ߸��â ���� & ����
        Managers.Resource.Instantiate("UI/Scene/PauseButton", canvas);

        boss = Managers.Resource.Instantiate("Object/Boss");//��������

        GameObject bi = Managers.Resource.Instantiate("UI/Scene/BossImage");//���� UI
        bi.transform.SetParent(canvas, false);

        Managers.Resource.Instantiate("UI/Scene/Information");//�ȳ�UI

        Managers.Resource.Instantiate("Object/FirstPortal");//ù ��Ż����


        //���������� �°� �� ����
        switch (Managers.currStage)
        {
            case (int)Define.Stage.Stage01: Managers.Resource.Instantiate("UI/Scene/Stage01_BG"); break;//���
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
