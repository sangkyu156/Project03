using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement_pig3 : AchievementBase
{
    void Start()
    {
        SetSlider();
        SetButton();
    }

    protected override void SetSlider()
    {
        slider.maxValue = 1000;
        slider.value = Managers.Data.pigCount;
    }

    public void SetButton()
    {
        switch (Managers.Data.achievement03)
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
        Managers.Data.achievement03 = 2;
        Managers.diamond += 3;

        SetButton();
        Managers.Instance.PrintDiamond();
    }
}
