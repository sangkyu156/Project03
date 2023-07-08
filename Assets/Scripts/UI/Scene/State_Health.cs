using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class State_Health : MonoBehaviour
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
        switch (Managers.Data.state_HealthLevel)
        {
            case 0:
                stateLevel.text = "Lv.<#FFFFFF>1</color>";
                stateValue.text = "10  ->  15";
                diaValue.text = "7";
                diaValue2.text = "7";
                break;
            case 1:
                stateLevel.text = "Lv.<#FFE1E1>2</color>";
                stateValue.text = "15  ->  20";
                diaValue.text = "14";
                diaValue2.text = "14";
                break;
            case 2:
                stateLevel.text = "Lv.<#FFC3C3>3</color>";
                stateValue.text = "20  ->  25";
                diaValue.text = "21";
                diaValue2.text = "21";
                break;
            case 3:
                stateLevel.text = "Lv.<#FFA5A5>4</color>";
                stateValue.text = "30  ->  35";
                diaValue.text = "28";
                diaValue2.text = "28";
                break;
            case 4:
                stateLevel.text = "Lv.<#FF8787>5</color>";
                stateValue.text = "35  ->  40";
                diaValue.text = "35";
                diaValue2.text = "35";
                break;
            case 5:
                stateLevel.text = "Lv.<#FF6969>6</color>";
                stateValue.text = "40  ->  45";
                diaValue.text = "42";
                diaValue2.text = "42";
                break;
            case 6:
                stateLevel.text = "Lv.<#FF4B4B>7</color>";
                stateValue.text = "45  ->  50";
                diaValue.text = "49";
                diaValue2.text = "49";
                break;
            case 7:
                stateLevel.text = "Lv.<#FF2D2D>8</color>";
                stateValue.text = "50  ->  55";
                diaValue.text = "56";
                diaValue2.text = "56";
                break;
            case 8:
                stateLevel.text = "Lv.<#FF0F0F>9</color>";
                stateValue.text = "55  ->  60";
                diaValue.text = "63";
                diaValue2.text = "63";
                break;
            case 9:
                stateLevel.text = "Lv.<#FF0F5F>10</color>";
                stateValue.text = "60  ->  70";
                diaValue.text = "70";
                diaValue2.text = "70";
                break;
            case 10:
                stateLevel.text = "Lv.<#FF00FA>MAX</color>";
                stateValue.text = "70";
                diaValue.text = "0";
                diaValue2.text = "0";
                break;
        }
    }

    //구매할때마다 모든 버튼이 이 함수를 호출해야함 (Action 으로)
    public void ButtenSet()
    {
        if (Managers.diamond < int.Parse(diaValue.text) || Managers.Data.state_HealthLevel == 10)
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

    public void HealthBuy()
    {
        Managers.diamond -= int.Parse(diaValue.text);
        //GameManager.Instance.SFXPlay(GameManager.Sfx.Upgrade);

        Managers.Data.state_HealthLevel++;
        HealthSet();
        Managers.Instance.PrintDiamond();

        ListSet();

        //Action호출
        Managers.Data.buyAction();
    }

    //실제 체력 적용
    void HealthSet()
    {
        switch (Managers.Data.state_HealthLevel)
        {
            case 1:
                Managers.Data.state_Health = 5;
                break;
            case 2:
                Managers.Data.state_Health = 10;
                break;
            case 3:
                Managers.Data.state_Health = 15;
                break;
            case 4:
                Managers.Data.state_Health = 20;
                break;
            case 5:
                Managers.Data.state_Health = 25;
                break;
            case 6:
                Managers.Data.state_Health = 30;
                break;
            case 7:
                Managers.Data.state_Health = 35;
                break;
            case 8:
                Managers.Data.state_Health = 40;
                break;
            case 9:
                Managers.Data.state_Health = 45;
                break;
            case 10:
                Managers.Data.state_Health = 55;
                break;
        }
    }
}
