using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement_try : AchievementBase
{
    void Start()
    {
        SetSlider();
        SetButton();
    }

    protected override void SetSlider()
    {
        slider.maxValue = 3;
        slider.value = Managers.Data.tryCount;
    }

    public void SetButton()
    {
        switch (Managers.Data.achievement04)
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
        Managers.Data.achievement04 = 2;
        Managers.diamond += 1;

        SetButton();
        Managers.Instance.PrintDiamond();
    }
}
