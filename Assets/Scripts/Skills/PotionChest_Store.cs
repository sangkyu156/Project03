using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotionChest_Store : MonoBehaviour
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;

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
        if (Player.Instance.potionChestLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:potionchest");
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                explanation.text = $"<size=120%><#E7E7E7>포션상자</color></size>\n<size=70%>Level {Player.Instance.potionChestLevel} -> <#3EFF3E>{Player.Instance.potionChestLevel + 1}</color></size>\n\n'포션상자' 나올 확률이 증가했습니다.";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                explanation.text = $"<size=120%><#E7E7E7>Potion Chest</color></size>\n<size=70%>Level {Player.Instance.potionChestLevel} -> <#3EFF3E>{Player.Instance.potionChestLevel + 1}</color></size>\n\nThe probability of a 'Potion Chest' appearing has increased.";
            }
        }
    }

    //구매
    public void PotionChestBuy()
    {
        if (Managers.fieldMoney < priceValue)
        {
            //GameManager.Instance.SFXPlay(GameManager.Sfx.DonotBuy);
            return;
        }

        Managers.fieldMoney -= priceValue;
        Managers.Data.paymentGold += priceValue;
        //GameManager.Instance.SFXPlay(GameManager.Sfx.Buy);

        Player.Instance.potionChestLevel++;
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
