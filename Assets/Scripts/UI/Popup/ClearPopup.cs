using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearPopup : MonoBehaviour
{
    public GameObject rewardPopup;
    public GameObject rewardBox;
    public TextMeshProUGUI clearRewarText;
    public TextMeshProUGUI totalText;

    public void RewardPopupOn()
    {
        //GameManager.Instance.SFXPlay(GameManager.Sfx.ClearBox);

        switch (Managers.Data.curStage)
        {
            case 1:
                Managers.Data.stageCheck[0] = true;
                break;
            case 2:
                Managers.Data.stageCheck[1] = true;
                break;
        }
        rewardPopup.SetActive(true);
        rewardBox.SetActive(false);

        clearRewarText.text = $"-    {Managers.Data.clearRewardDiamond}";
        totalText.text = $"-    {(int)(Managers.Data.killCount * 0.01) + Managers.Data.clearRewardDiamond}";

        Managers.diamond += (int)(Managers.Data.killCount * 0.01) + Managers.Data.clearRewardDiamond;//보상지급
    }

    public void ExitButton_Home()
    {
        switch (Managers.Data.curStage)
        {
            case 1:
                Managers.Data.stageCheck[0] = true;
                break;
            case 2:
                Managers.Data.stageCheck[1] = true;
                break;
        }
        //GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);

        Managers.Scene.LoadScene(Define.Scene.Main);
    }
}
