using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage01 : MonoBehaviour
{
    public void GoStage01()
    {
        Managers.currStage = (int)Define.Stage.Stage01;
        Managers.currScene = (int)Define.Scene.Stage;
        Managers.Data.tryCount++;

        Managers.Scene.LoadScene(Define.Scene.Stage);
    }
}
