using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage02 : MonoBehaviour
{
    public void GoStage02()
    {
        if (Managers.Data.stageCheck[0] != true)
            return;

        Managers.currStage = (int)Define.Stage.Stage02;
        Managers.currScene = (int)Define.Scene.Stage;
        Managers.Data.tryCount++;

        Managers.Scene.LoadScene(Define.Scene.Stage);
    }
}
