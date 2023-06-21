using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장된다
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다
    static public int currStage = (int)Define.Stage.Stage01;
    static public int currScene = (int)Define.Scene.Title;
    static public int fieldMoney { get; set; } = 0;

    //ObjectManager _oj = new ObjectManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    //UIManager _ui = new UIManager();

    //public static ObjectManager Object { get { return Instance._oj; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    //public static UIManager UI { get { return Instance._ui; } }

    void Start()
    {
        Init();
        //TEMP
        fieldMoney = 5000;
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
}
