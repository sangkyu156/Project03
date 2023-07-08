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
        //����� �÷��̾��� ��ų����, Hp, ��� �ʱ�ȭ ��Ű�°� �´��� Ȯ���ؾ���
    }

    public void SetScreen()
    {
        player = Managers.Resource.Instantiate("Object/Player");
        camera.GetComponent<CameraController>().SetPlayer();//ī�޶� �÷��̾� ����
        player.GetComponent<Player>().SetCamera();//�÷��̾ ī�޶� ����

        ts = Managers.Resource.Instantiate("UI/Scene/TargetSpotUI");//����� UI
        ts.transform.SetParent(canvas, false);

        fieldMoney = Managers.Resource.Instantiate("UI/Scene/FieldMoney", canvas);//��ȭâ ����
        fieldMoney_Text = Util.FindChild<TextMeshProUGUI>(fieldMoney, null, true);//��ȭâ ����
        fieldDiamond = Util.FindChild<TextMeshProUGUI>(Managers.Resource.Instantiate("UI/Scene/FieldDiamond", canvas), null, true);//�پ߸��â ���� & ����
        Managers.Resource.Instantiate("UI/Scene/PauseButton", canvas);

        boss = Managers.Resource.Instantiate("Object/Boss");//��������
        Managers.Instance.FindBoss();

        GameObject bi = Managers.Resource.Instantiate("UI/Scene/BossImage");//���� UI
        bi.transform.SetParent(canvas, false);

        Managers.Resource.Instantiate("UI/Scene/Information");//�ȳ�UI

        Managers.Resource.Instantiate("Object/FirstPortal");//ù ��Ż����


        //���������� �°� �� ����
        switch (Managers.currStage)
        {
            case (int)Define.Stage.Stage01: Managers.Resource.Instantiate("UI/Scene/Stage01_BG");Debug.Log("123"); break;//���
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
