using UnityEngine;

public class StageBG : UI_Scene
{
    //public int countBG = 0;
    public GameObject[] map;
    GameObject player;
    GameObject stageScene;

    float dist = 0f;

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
                map[i].transform.position += new Vector3(150, 0, 0);//맵이동
                Managers.Data.countBG++;

                //진행률 증가
                stageScene.GetComponent<StageScene>().ts.GetComponent<TargetSpotUI>().SetProgress(Managers.Data.countBG);

                //웅덩이 생성
                CreateManager.Instance.CreatePuddle();

                //바위 생성
                if (Random.Range(0, 142) <= 100) //70% 확률
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

                //상점 생성
                if (Managers.Data.countBG % 2 == 0)
                {
                    if (Random.Range(0, 21) <= 10 + Player.Instance.regularLevel)
                    {
                        GameObject portal = Managers.Resource.Instantiate("Object/Portal");
                        portal.transform.position = new Vector3(map[i].transform.position.x - 50, -3.5f, 0);
                    }
                }

                //금화상자 생성
                for (int q = 0; q < Player.Instance.goldChestLevel + 4; q++)
                {
                    if (Random.Range(0, 10) == 2) //10% 확률
                    {
                        float ran_x = Random.Range(20, 48);
                        float ran_y = Random.Range(-9f, -5.5f);
                        GameObject box = Managers.Resource.Instantiate("Object/Box1");
                        box.transform.position = new Vector3(Player.Instance.transform.position.x + ran_x, ran_y, 0);
                    }
                }

                //포션상자 생성
                for (int q = 0; q < Player.Instance.potionChestLevel + 4; q++)
                {
                    if (Random.Range(0, 20) == 2) //5% 확률
                    {
                        float ran_x = Random.Range(20, 48);
                        float ran_y = Random.Range(-9f, -5.5f);
                        GameObject box = Managers.Resource.Instantiate("Object/Box2");
                        box.transform.position = new Vector3(Player.Instance.transform.position.x + ran_x, ran_y, 0);
                    }
                }

                //에너미 생성
                switch (Managers.Data.curStage)
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
    }

    void Stage01CreateEnemy(int _countBG)
    {
        switch (_countBG)
        {
            //case 2: CreateManager.Instance.Create_01(); break;
            //case 4: CreateManager.Instance.Create_01_1(); break;
            //case 6: CreateManager.Instance.Create_01_2(); break;
            //case 8: CreateManager.Instance.Create_02(); break;
            //case 10: CreateManager.Instance.Create_02_1(); break;
            //case 12: CreateManager.Instance.Create_02_2(); break;
            //case 14: CreateManager.Instance.Create_03(); break;
            //case 16: CreateManager.Instance.Create_03_1(); break;
            //case 18: CreateManager.Instance.Create_03_2(); break;
            //case 20: CreateManager.Instance.Create_04(); break;
            //case 22: CreateManager.Instance.Create_04_1(); break;
            //case 23: CreateManager.Instance.Create_Orc(); break;
            //case 25: CreateManager.Instance.Create_04_2(); break;
            //case 27:
            //    CreateManager.Instance.Create_01_1();
            //    CreateManager.Instance.Create_02_1();
            //    CreateManager.Instance.Create_03_1();
            //    break;
            //case 28:
            //    CreateManager.Instance.Create_01_2();
            //    CreateManager.Instance.Create_02_2();
            //    CreateManager.Instance.Create_03_2();
            //    break;
            //case 29:
            //    CreateManager.Instance.Create_02_2();
            //    CreateManager.Instance.Create_03_2();
            //    CreateManager.Instance.Create_04_2();
            //    break;
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
}
