using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement_pig : AchievementBase
{
    void Start()
    {
        SetSlider();
        SetButton();
    }

    protected override void SetSlider()
    {
        slider.maxValue = 100;
        slider.value = Managers.Data.pigCount;
    }

    public void SetButton()
    {
        switch (Managers.Data.achievement01)
        {
            case 0:
                button1.SetActive(true);
                button2.SetActive(false);
                button3.SetActive(false);
                break;
            case 1:
                button1.SetActive(false);
                button2.SetActive(true);
                button3.SetActive(false);
                break;
            case 2:
                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(true);
                break;
        }
    }

    public void Reward()
    {
        Managers.Sound.Play("DiamondReward");
        Managers.Data.achievement01 = 2;
        Managers.diamond += 1;

        SetButton();
        Managers.Instance.PrintDiamond();
    }
}
