using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePopup : MonoBehaviour
{
    GameObject stage;
    int curStage = 0;

    void Start()
    {
        SetStage();
    }

    void Update()
    {
        
    }

    public void NextStage()
    {
        if(stage != null)
            Destroy(stage);

        curStage++;
        if (curStage == 5)
            curStage--;

        SetStage();
    }

    public void PreviousStage()
    {
        if (stage != null)
            Destroy(stage);

        curStage--;
        if (curStage == -1)
            curStage++;

        SetStage();
    }

    void SetStage()
    {
        for (int i = 0; i < 5; i++)//스테이지 개수만큼 반복
        {
            if (curStage == i)
            {
                stage = Managers.Resource.Instantiate($"UI/Popup/Stage0{i + 1}", transform);
                stage.transform.SetAsLastSibling();

                switch (i)
                {
                    case 0: Managers.currStage = (int)Define.Stage.Stage01; break;
                    case 1: Managers.currStage = (int)Define.Stage.Stage02; break;
                    case 2: Managers.currStage = (int)Define.Stage.Stage03; break;
                    case 3: Managers.currStage = (int)Define.Stage.Stage04; break;
                    case 4: Managers.currStage = (int)Define.Stage.Stage05; break;
                }
            }
        }
    }
}
