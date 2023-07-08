using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeadPopup : MonoBehaviour
{
    public TextMeshProUGUI progressValue;
    public TextMeshProUGUI killValue;
    public TextMeshProUGUI goldPaymentValue;
    public TextMeshProUGUI diamondAcquisitionValue;
    public TextMeshProUGUI storCountValue;

    GameObject homeCanvas;
    public GameObject gameoverText;
    public GameObject[] smokeFX;
    bool first = false;

    private void OnEnable()
    {
        first = false;
        SmokeFX_Off();
        Calculate();//보상 지급
        progressValue.text = $"{(Managers.Data.countBG * 100) / 30} %";
        killValue.text = $"{Managers.Data.killCount}";
        goldPaymentValue.text = $"{Managers.Data.paymentGold}";
        diamondAcquisitionValue.text = $"{Managers.diamond}";
        storCountValue.text = $"{Managers.Data.storCount}";
    }

    void Update()
    {
        if (gameoverText.transform.localPosition.y <= 320 && first == false)
        {
            first = true;
            SmokeFX_On();
        }
    }

    void SmokeFX_Off()
    {
        for (int i = 0; i < smokeFX.Length; i++)
        {
            smokeFX[i].SetActive(false);
        }
    }

    void SmokeFX_On()
    {
        for (int i = 0; i < smokeFX.Length; i++)
        {
            smokeFX[i].SetActive(true);
        }
    }
    public void ExitButton_Home()
    {
        //GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);

        Managers.Scene.LoadScene(Define.Scene.Main);
    }

    public void Calculate()
    {
        int A = 0;
        int B = 0;
        if (((Managers.Data.countBG * 100) / 30) < 10)
            A = 0;
        else if (((Managers.Data.countBG * 100) / 30) >= 10 && ((Managers.Data.countBG * 100) / 30) < 20)
            A = 1;
        else if (((Managers.Data.countBG * 100) / 30) >= 20 && ((Managers.Data.countBG * 100) / 30) < 30)
            A = 2;
        else if (((Managers.Data.countBG * 100) / 30) >= 30 && ((Managers.Data.countBG * 100) / 30) < 40)
            A = 3;
        else if (((Managers.Data.countBG * 100) / 30) >= 40 && ((Managers.Data.countBG * 100) / 30) < 50)
            A = 4;
        else if (((Managers.Data.countBG * 100) / 30) >= 50 && ((Managers.Data.countBG * 100) / 30) < 60)
            A = 5;
        else if (((Managers.Data.countBG * 100) / 30) >= 70 && ((Managers.Data.countBG * 100) / 30) < 80)
            A = 6;
        else if (((Managers.Data.countBG * 100) / 30) >= 80 && ((Managers.Data.countBG * 100) / 30) < 90)
            A = 7;
        else if (((Managers.Data.countBG * 100) / 30) >= 90 && ((Managers.Data.countBG * 100) / 30) <= 100)
            A = 8;

        if (Managers.Data.killCount < 100)
            B = 0;
        else
            B = (int)(Managers.Data.killCount * 0.01);

        Managers.diamond += A + B;
    }
}
