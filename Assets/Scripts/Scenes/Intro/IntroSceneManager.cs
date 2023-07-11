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
            Destroy(this.gameObject);
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
        Managers.Sound.Play("IntroBGM", Define.Sound.Bgm);

        Invoke("GoStage", 9.8f);

        //Invoke("GoStage", 2.3f);
    }

    void GoStage()
    {
        Managers.Scene.LoadScene(Define.Scene.Stage);
    }

    public override void Clear()
    {
        //비어있음
    }
}
