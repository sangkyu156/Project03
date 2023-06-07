using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneManager : BaseScene
{
    private static IntroSceneManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static IntroSceneManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Start()
    {
        Invoke("GoStage01", 9.8f);

        //Invoke("GoStage01", 0.3f);

        //GameObject homeManager = Instantiate(Resources.Load<GameObject>("Home/HomeManager"));
    }

    private void Update()
    {

    }

    void GoStage01()
    {
        Managers.Scene.LoadScene(Define.Scene.Stage01);
    }

    public override void Clear()
    {
        //비어있음
    }
}
