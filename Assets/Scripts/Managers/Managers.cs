using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Managers : MonoBehaviour
{
    GameObject stageScene;
    GameObject mainScene;
    [SerializeField]
    GameObject boss;

    public System.Action buyCheckAction;//스킬 구매시 남은돈으로 다른스킬 구매 가능한지 색구분하도록
    public System.Action skillLockAction;//공격스킬 4개 모두 정해졌을때 다른 공격스킬 구매못하도록

    static Managers s_instance; // 유일성이 보장된다
    static public Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다
    static public int currStage = (int)Define.Stage.Stage01;
    static public int currScene = (int)Define.Scene.Title;
    static public int fieldMoney { get; set; } = 0;
    static public int diamond { get; set; } = 0;

    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    DataManager _data = new DataManager();

    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static DataManager Data { get { return Instance._data; } }

    void Start()
    {
        Init();
        CreateManager.Init();

        //TEMP
        fieldMoney = 10000;
        diamond = 5000;
    }

    void Update()
    {
        //TEMP
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TextUtil.languageNumber = 2; //영어
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            TextUtil.languageNumber = 1; //한국어
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            fieldMoney += 500;
            PrintFieldMoney();
        }
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }

    public static void Clear()
    {
        //Scene.Clear();
        //Input.Clear();
        //Sound.Clear();
        //UI.Clear();
    }

    public void GetStageScene()
    {
        stageScene = GameObject.FindGameObjectWithTag("StageScene");
    }

    public void GetMainScene()
    {
        mainScene = GameObject.FindGameObjectWithTag("MainScene");
    }

    public void PrintFieldMoney()
    {
        if (currScene != (int)Define.Scene.Stage)
            return;

        if (stageScene == null)
            stageScene = GameObject.FindGameObjectWithTag("StageScene");
        else
            stageScene.GetComponent<StageScene>().PrintFieldMoney();
    }

    public void PrintDiamond()
    {
        if (currScene != (int)Define.Scene.Main)
            return;

        if (mainScene == null)
            mainScene = GameObject.FindGameObjectWithTag("Mainscene");
        else
            mainScene.GetComponent<MainScene>().PrintDiamond();
    }

    public void FindBoss()
    {
        if(boss == null)
            boss = GameObject.FindGameObjectWithTag("Boss");
    }

    public void SetBossSpeed(float speed)
    {
        if (boss == null)
            boss = GameObject.FindGameObjectWithTag("Boss");

        boss.GetComponent<Boss>().bossSpeed = speed;
    }
}
