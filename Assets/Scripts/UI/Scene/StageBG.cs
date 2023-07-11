using UnityEngine;

public class StageBG : UI_Scene
{
    public GameObject[] map;
    GameObject player;
    GameObject stageScene;
    float reSpawnTime = 2f;
    float dist = 0f;
    bool bgMove = false;

    private void Start()
    {
        stageScene = GameObject.FindGameObjectWithTag("StageScene");
    }

    private void Update()
    {
        if (Managers.currScene != (int)Define.Scene.Stage)
            return;

        for (int i = 0; i < map.Length; i++)
        {
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player");

            dist = player.transform.position.x - 60;

            if (dist > map[i].transform.position.x)
            {
                map[i].transform.position += new Vector3(150, 0, 0);//���̵�
                Managers.Data.countBG++;
                bgMove = true;

                //���� ����
                if (Managers.Data.countBG % 2 == 0)
                {
                    if (Random.Range(0, 21) <= 10 + Player.Instance.regularLevel)
                    {
                        GameObject portal = Managers.Resource.Instantiate("Object/Portal");
                        portal.transform.position = new Vector3(map[i].transform.position.x - 50, -3.5f, 0);
                    }
                }
            }
        }

        //Pig����
        reSpawnTime -= Time.deltaTime;
        if (reSpawnTime <= 0)
        {
            reSpawnTime = 2f;
            switch (Managers.currStage)
            {
                case 1: Stage01PigCreate(Managers.Data.countBG); break;
                case 2: Stage02PigCreate(Managers.Data.countBG); break;
            }
        }

        if (bgMove)
        {
            bgMove = false;

            //����� ����
            stageScene.GetComponent<StageScene>().ts.GetComponent<TargetSpotUI>().SetProgress(Managers.Data.countBG);

            //������ ����
            CreateManager.Instance.CreatePuddle();

            //���� ����
            if (Random.Range(0, 142) <= 100) //70% Ȯ��
            {
                CreateManager.Instance.CreateUpRock();
                CreateManager.Instance.CreateDownRock();
            }
            else
            {
                switch (Random.Range(0, 2))
                {
                    case 0:
                        CreateManager.Instance.CreateUpRock(); break;
                    case 1:
                        CreateManager.Instance.CreateDownRock(); break;
                }
            }

            //��ȭ���� ����
            for (int q = 0; q < Player.Instance.goldChestLevel + 4; q++)
            {
                if (Random.Range(0, 10) == 2) //10% Ȯ��
                {
                    float ran_x = Random.Range(20, 48);
                    float ran_y = Random.Range(-9f, -5.5f);
                    GameObject box = Managers.Resource.Instantiate("Object/Box1");
                    box.transform.position = new Vector3(Player.Instance.transform.position.x + ran_x, ran_y, 0);
                }
            }

            //���ǻ��� ����
            for (int q = 0; q < Player.Instance.potionChestLevel + 4; q++)
            {
                if (Random.Range(0, 20) == 2) //5% Ȯ��
                {
                    float ran_x = Random.Range(20, 48);
                    float ran_y = Random.Range(-9f, -5.5f);
                    GameObject box = Managers.Resource.Instantiate("Object/Box2");
                    box.transform.position = new Vector3(Player.Instance.transform.position.x + ran_x, ran_y, 0);
                }
            }

            //���ʹ� ����
            switch (Managers.currStage)
            {
                case 1:
                    Stage01CreateEnemy(Managers.Data.countBG);
                    break;
                case 2:
                    Stage02CreateEnemy(Managers.Data.countBG);
                    break;
            }
        }
    }

    void Stage01CreateEnemy(int _countBG)
    {
        switch (_countBG)
        {
            case 2: CreateManager.Instance.Create_01(); break;
            case 4: CreateManager.Instance.Create_01_1(); break;
            case 6: CreateManager.Instance.Create_01_2(); break;
            case 8: CreateManager.Instance.Create_02(); break;
            case 10: CreateManager.Instance.Create_02_1(); break;
            case 12: CreateManager.Instance.Create_02_2(); break;
            case 14: CreateManager.Instance.Create_03(); break;
            case 16: CreateManager.Instance.Create_03_1(); break;
            case 18: CreateManager.Instance.Create_03_2(); break;
            case 20: CreateManager.Instance.Create_04(); break;
            case 22: CreateManager.Instance.Create_04_1(); break;
            case 23: CreateManager.Instance.Create_Orc(); break;
            case 25: CreateManager.Instance.Create_04_2(); break;
            case 27:
                CreateManager.Instance.Create_01_1();
                CreateManager.Instance.Create_02_1();
                CreateManager.Instance.Create_03_1();
                break;
            case 28:
                CreateManager.Instance.Create_01_2();
                CreateManager.Instance.Create_02_2();
                CreateManager.Instance.Create_03_2();
                break;
            case 29:
                CreateManager.Instance.Create_02_2();
                CreateManager.Instance.Create_03_2();
                CreateManager.Instance.Create_04_2();
                break;
            case 30:
                CreateManager.Instance.CreateClearPortal(); break;
            default:
                break;
        }
    }

    void Stage02CreateEnemy(int _countBG)
    {
        switch (_countBG)
        {
            case 2: CreateManager.Instance.Create_10(); break;
            case 4: CreateManager.Instance.Create_10_1(); break;
            case 6: CreateManager.Instance.Create_10_2(); break;
            case 8: CreateManager.Instance.Create_11(); break;
            case 10: CreateManager.Instance.Create_11_1(); break;
            case 12: CreateManager.Instance.Create_11_2(); break;
            case 14: CreateManager.Instance.Create_12(); break;
            case 16: CreateManager.Instance.Create_12_1(); break;
            case 18: CreateManager.Instance.Create_12_2(); break;
            case 20: CreateManager.Instance.Create_13(); break;
            case 22: CreateManager.Instance.Create_13_1(); break;
            case 23: CreateManager.Instance.Create_Orc2(); break;
            case 25: CreateManager.Instance.Create_13_2(); break;
            case 27:
                CreateManager.Instance.Create_10_1();
                CreateManager.Instance.Create_11_1();
                CreateManager.Instance.Create_12_1();
                break;
            case 28:
                CreateManager.Instance.Create_10_2();
                CreateManager.Instance.Create_11_2();
                CreateManager.Instance.Create_12_2();
                break;
            case 29:
                CreateManager.Instance.Create_11_2();
                CreateManager.Instance.Create_12_2();
                CreateManager.Instance.Create_13_2();
                break;
            case 30:
                CreateManager.Instance.CreateClearPortal(); break;
            default:
                break;
        }
    }

    void Stage01PigCreate(int _countBG)
    {
        if (1 <= _countBG && _countBG < 8)
            CreateManager.Instance.RepeatCreate_01();
        else if (8 <= _countBG && _countBG < 15)
            CreateManager.Instance.RepeatCreate_02();
        else if (15 <= _countBG && _countBG < 23)
            CreateManager.Instance.RepeatCreate_03();
        else if (23 <= _countBG && _countBG < 30)
            CreateManager.Instance.RepeatCreate_04();
    }

    void Stage02PigCreate(int _countBG)
    {
        if (1 <= _countBG && _countBG < 8)
            CreateManager.Instance.RepeatCreate_02();
        else if (8 <= _countBG && _countBG < 15)
            CreateManager.Instance.RepeatCreate_03();
        else if (15 <= _countBG && _countBG < 23)
            CreateManager.Instance.RepeatCreate_04();
        else if (23 <= _countBG && _countBG < 30)
            CreateManager.Instance.RepeatCreate_05();
    }
}
