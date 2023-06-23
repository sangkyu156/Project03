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

    public System.Action buyCheckAction;//��ų ���Ž� ���������� �ٸ���ų ���� �������� �������ϵ���
    public System.Action skillLockAction;//���ݽ�ų 4�� ��� ���������� �ٸ� ���ݽ�ų ���Ÿ��ϵ���

    static Managers s_instance; // ���ϼ��� ����ȴ�
    static public Managers Instance { get { Init(); return s_instance; } } // ������ �Ŵ����� ����´�
    static public int currStage = (int)Define.Stage.Stage01;
    static public int currScene = (int)Define.Scene.Title;
    static public int fieldMoney { get; set; } = 0;

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
        fieldMoney = 5000;
    }

    void Update()
    {
        //TEMP
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TextUtil.languageNumber = 2; //����
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            TextUtil.languageNumber = 1; //�ѱ���
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

    public void PrintFieldMoney()
    {
        if (stageScene == null)
            stageScene = GameObject.FindGameObjectWithTag("StageScene");
        else
            stageScene.GetComponent<StageScene>().PrintFieldMoney();
    }
}
