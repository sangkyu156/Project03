using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Regenerate_Store : MonoBehaviour
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;
    float healingAmount = 0;

    void Start()
    {
        int min = (int)Math.Round((int)Define.SkillPrice.Normal * 0.9f);
        int max = (int)Math.Round((int)Define.SkillPrice.Normal * 1.1f);

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
        if (Player.Instance.regenerateLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:regenerate");
        }
        else if (Player.Instance.regenerateLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>체력 회복</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n회복량 <#FF2D2D>{healingAmount}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>Regenerate</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nHealing Amount <#FF2D2D>{healingAmount}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>체력 회복</color></size>\n<size=70%>Level {Player.Instance.regenerateLevel} -> <#3EFF3E>{Player.Instance.regenerateLevel + 1}</color></size>\n\n회복량 {healingAmount} -> <#3EFF3E>{healingAmount + 1}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>Regenerate</color></size>\n<size=70%>Level {Player.Instance.regenerateLevel} -> <#3EFF3E>{Player.Instance.regenerateLevel + 1}</color></size>\n\nHealing Amount {healingAmount} -> <#3EFF3E>{healingAmount + 1}</color>";
            }
        }
    }

    public void SetAbility()
    {
        switch (Player.Instance.regenerateLevel)
        {
            case 0:
                healingAmount = 0;
                break;
            case 1:
                healingAmount = 1;
                break;
            case 2:
                healingAmount = 3;
                break;
            case 3:
                healingAmount = 5;
                break;
            case 4:
                healingAmount = 7;
                break;
            case 5:
                healingAmount = 9;
                break;
            case 6:
                healingAmount = 11;
                break;
            case 7:
                healingAmount = 15;
                break;
        }
    }

    //구매
    public void RegeneratesBuy()
    {
        if (Managers.fieldMoney < priceValue)
        {
            //GameManager.Instance.SFXPlay(GameManager.Sfx.DonotBuy);
            return;
        }

        Managers.fieldMoney -= priceValue;
        Managers.Data.paymentGold += priceValue;
        //GameManager.Instance.SFXPlay(GameManager.Sfx.Buy);

        Player.Instance.regenerateLevel++;
        Player.Instance.regenerateCooldown = 10;
        Player.Instance.regenerate = true;

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
