using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class State_Potion : MonoBehaviour
{
    public GameObject butten01;
    public GameObject butten02;
    public TextMeshProUGUI stateLevel;
    public TextMeshProUGUI stateValue;
    public TextMeshProUGUI diaValue;
    public TextMeshProUGUI diaValue2;

    void Start()
    {
        ListSet();
        Managers.Data.buyAction += ButtenSet;

        Managers.Data.buyAction();
    }

    private void OnDestroy()
    {
        Managers.Data.buyAction -= ButtenSet;
    }

    //레벨별 색 값  =  FFFFFF , FFE1E1 , FFC3C3 , FFA5A5 , FF8787 , FF6969 , FF4B4B , FF2D2D , FF0F0F , FF0F5F , FF00FA
    public void ListSet()
    {
        switch (Managers.Data.state_PotionRecoverLevel)
        {
            case 0:
                stateLevel.text = "Lv.<#FFFFFF>1</color>";
                stateValue.text = "+0";
                diaValue.text = "7";
                diaValue2.text = "7";
                break;
            case 1:
                stateLevel.text = "Lv.<#FFE1E1>2</color>";
                stateValue.text = "+1";
                diaValue.text = "14";
                diaValue2.text = "14";
                break;
            case 2:
                stateLevel.text = "Lv.<#FFC3C3>3</color>";
                stateValue.text = "+2";
                diaValue.text = "21";
                diaValue2.text = "21";
                break;
            case 3:
                stateLevel.text = "Lv.<#FFA5A5>4</color>";
                stateValue.text = "+3";
                diaValue.text = "28";
                diaValue2.text = "28";
                break;
            case 4:
                stateLevel.text = "Lv.<#FF8787>5</color>";
                stateValue.text = "+4";
                diaValue.text = "35";
                diaValue2.text = "35";
                break;
            case 5:
                stateLevel.text = "Lv.<#FF6969>6</color>";
                stateValue.text = "+5";
                diaValue.text = "42";
                diaValue2.text = "42";
                break;
            case 6:
                stateLevel.text = "Lv.<#FF4B4B>7</color>";
                stateValue.text = "+6";
                diaValue.text = "49";
                diaValue2.text = "49";
                break;
            case 7:
                stateLevel.text = "Lv.<#FF2D2D>8</color>";
                stateValue.text = "+7";
                diaValue.text = "56";
                diaValue2.text = "56";
                break;
            case 8:
                stateLevel.text = "Lv.<#FF0F0F>9</color>";
                stateValue.text = "+8";
                diaValue.text = "63";
                diaValue2.text = "63";
                break;
            case 9:
                stateLevel.text = "Lv.<#FF0F5F>10</color>";
                stateValue.text = "+9";
                diaValue.text = "70";
                diaValue2.text = "70";
                break;
            case 10:
                stateLevel.text = "Lv.<#FF00FA>MAX</color>";
                stateValue.text = "+10";
                diaValue.text = "0";
                diaValue2.text = "0";
                break;
        }
    }

    //구매할때마다 모든 버튼이 이 함수를 호출해야함 (Action 으로)
    public void ButtenSet()
    {
        if (Managers.diamond < int.Parse(diaValue.text) || Managers.Data.state_PotionRecoverLevel == 10)
        {
            butten01.SetActive(true);
            butten02.SetActive(false);
        }
        else
        {
            butten01.SetActive(false);
            butten02.SetActive(true);
        }
    }

    public void PotionRecoverBuy()
    {
        Managers.diamond -= int.Parse(diaValue.text);
        Managers.Sound.Play("Upgrade");

        Managers.Data.state_PotionRecoverLevel++;
        PotionRecoverSet();
        Managers.Instance.PrintDiamond();

        ListSet();

        //Action호출
        Managers.Data.buyAction();
    }

    //실제 체력 적용
    void PotionRecoverSet()
    {
        switch (Managers.Data.state_PotionRecoverLevel)
        {
            case 1:
                Managers.Data.state_PotionRecover = 1;
                break;
            case 2:
                Managers.Data.state_PotionRecover = 2;
                break;
            case 3:
                Managers.Data.state_PotionRecover = 3;
                break;
            case 4:
                Managers.Data.state_PotionRecover = 4;
                break;
            case 5:
                Managers.Data.state_PotionRecover = 5;
                break;
            case 6:
                Managers.Data.state_PotionRecover = 6;
                break;
            case 7:
                Managers.Data.state_PotionRecover = 7;
                break;
            case 8:
                Managers.Data.state_PotionRecover = 8;
                break;
            case 9:
                Managers.Data.state_PotionRecover = 9;
                break;
            case 10:
                Managers.Data.state_PotionRecover = 10;
                break;
        }
    }
}
