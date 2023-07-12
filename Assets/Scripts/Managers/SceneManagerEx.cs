using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    //public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }
    Color fadeColor = Color.black;

	public void LoadScene(Define.Scene type)
    {
        Managers.SaveLode.SaveData();

        Managers.Clear();

        Initiate.Fade(GetSceneName(type), fadeColor, 1.2f);
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        //CurrentScene.Clear();
    }
}
