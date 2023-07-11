using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement_Redraw2 : AchievementBase
{
    void Start()
    {
        SetSlider();
        SetButton();
    }

    protected override void SetSlider()
    {
        slider.maxValue = 10;
        slider.value = Managers.Data.redrawCount;
    }

    public void SetButton()
    {
        switch (Managers.Data.achievement06)
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
        Managers.Data.achievement06 = 2;
        Managers.diamond += 2;

        SetButton();
        Managers.Instance.PrintDiamond();
    }
}
