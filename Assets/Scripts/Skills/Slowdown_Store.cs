using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Slowdown_Store : MonoBehaviour
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;
    float reducesSpeed = 0;

    void Start()
    {
        //GameManager.Instance.SFXPlay(GameManager.Sfx.LegendarySkill);

        int min = (int)Math.Round((int)Define.SkillPrice.Legendary * 0.9f);
        int max = (int)Math.Round((int)Define.SkillPrice.Legendary * 1.1f);

        price.text = UnityEngine.Random.Range(min, max).ToString();
        priceValue = Int32.Parse(price.text);

        Managers.Instance.buyCheckAction += BuyCheck;
        Managers.Instance.buyCheckAction();

        buyButton.transform.SetAsLastSibling();//버튼제일 아래로 위치

        PrintExplanation();
    }

    private void OnDestroy()
    {
        Managers.Instance.buyCheckAction -= BuyCheck;
    }

    //설명 텍스트 출력
    void PrintExplanation()
    {
        if (Player.Instance.slowdownLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:slowdown");
        }
        else if (Player.Instance.slowdownLevel == 4)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#FFFF32>감속</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n보스 이동속도 감소량 <#FF2D2D>{reducesSpeed}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#FFFF32>Slowdown</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nReduction amount of boss movement speed <#FF2D2D>{reducesSpeed}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#FFFF32>감속</color></size>\n<size=70%>Level {Player.Instance.slowdownLevel} -> <#3EFF3E>{Player.Instance.slowdownLevel + 1}</color></size>\n\n보스 이동속도 감소량 {reducesSpeed} -> <#3EFF3E>{reducesSpeed + 0.5f}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#FFFF32>Slowdown</color></size>\n<size=70%>Level {Player.Instance.slowdownLevel} -> <#3EFF3E>{Player.Instance.slowdownLevel + 1}</color></size>\n\nReduction amount of boss movement speed {reducesSpeed} -> <#3EFF3E>{reducesSpeed + 0.5f}</color>";
            }
        }
    }

    public void SetAbility()
    {
        switch (Player.Instance.slowdownLevel)
        {
            case 0:
                reducesSpeed = 0;
                break;
            case 1:
                reducesSpeed = 0.5f;
                Managers.Instance.SetBossSpeed(6.5f);
                break;
            case 2:
                reducesSpeed = 1;
                Managers.Instance.SetBossSpeed(6f);
                break;
            case 3:
                reducesSpeed = 1.5f;
                Managers.Instance.SetBossSpeed(5.5f);
                break;
            case 4:
                reducesSpeed = 2;
                Managers.Instance.SetBossSpeed(5f);
                break;
        }
    }

    //구매
    public void SlowdownBuy()
    {
        if (Managers.fieldMoney < priceValue)
        {
            //GameManager.Instance.SFXPlay(GameManager.Sfx.DonotBuy);
            return;
        }

        Managers.fieldMoney -= priceValue;
        Managers.Data.paymentGold += priceValue;
        //GameManager.Instance.SFXPlay(GameManager.Sfx.Buy);

        Managers.Data.legendSkillCount++;
        Player.Instance.slowdownLevel++;

        if (Player.Instance.firstStore)
            gameObject.transform.parent.parent.gameObject.GetComponent<FirstStoreItems>().PrintFieldMoney();
        else
            gameObject.transform.parent.parent.gameObject.GetComponent<StoreItems>().PrintFieldMoney();

        Managers.Instance.buyCheckAction();
        PrintExplanation();

        buyButton.interactable = false;
    }

    //구매가능여부체크
    public void BuyCheck()
    {
        if (priceValue > Managers.fieldMoney)
            price.color = Color.red;
        else
            price.color = Color.white;
    }
}
