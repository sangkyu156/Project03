using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Trident_Store : Trident_Skill
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;

    void Start()
    {
        int min = (int)Math.Round((int)Define.SkillPrice.Rare * 0.9f);
        int max = (int)Math.Round((int)Define.SkillPrice.Rare * 1.1f);

        price.text = UnityEngine.Random.Range(min, max).ToString();
        priceValue = Int32.Parse(price.text);

        Managers.Instance.buyCheckAction += BuyCheck;
        Managers.Instance.buyCheckAction();

        Managers.Instance.skillLockAction += SkillLock;

        buyButton.transform.SetAsLastSibling();//버튼제일 아래로 위치

        PrintExplanation();
    }

    private void OnDestroy()
    {
        Managers.Instance.buyCheckAction -= BuyCheck;
        Managers.Instance.skillLockAction -= SkillLock;
    }

    //설명 텍스트 출력
    void PrintExplanation()
    {
        if (Player.Instance.tridentLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:trident");
        }
        else if (Player.Instance.tridentLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>삼지창</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n공격력 <#FF2D2D>{curPower}</color>\n공격속도 <#FF2D2D>{Player.Instance.tridentCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>Trident</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nPower <#FF2D2D>{curPower}</color>\nCooldown <#FF2D2D>{Player.Instance.tridentCooldown}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>삼지창</color></size>\n<size=70%>Level {Player.Instance.tridentLevel} -> <#3EFF3E>{Player.Instance.tridentLevel + 1}</color></size>\n\n공격력 {curPower} -> <#3EFF3E>{nextPower}</color>\n공격속도 {Player.Instance.tridentCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>Trident</color></size>\n<size=70%>Level {Player.Instance.tridentLevel} -> <#3EFF3E>{Player.Instance.tridentLevel + 1}</color></size>\n\nPower {curPower} -> <#3EFF3E>{nextPower}</color>\nCooldown {Player.Instance.tridentCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
        }
    }

    public void TridentBuy()
    {
        if (Player.Instance.tridentLevel >= 7)
            return;

        if (Managers.fieldMoney < priceValue)
        {
            //GameManager.Instance.SFXPlay(GameManager.Sfx.DonotBuy);
            return;
        }

        Managers.fieldMoney -= priceValue;
        Managers.Data.paymentGold += priceValue;
        //GameManager.Instance.SFXPlay(GameManager.Sfx.Buy);

        if (Player.Instance.tridentLevel == 0)
        {
            Player.Instance.tridentLevel++;
            Player.Instance.attackSkillCount++;

            if (Player.Instance.attackSkillCount >= 4)
                Managers.Instance.skillLockAction();
        }
        else
            Player.Instance.tridentLevel++;

        switch (Player.Instance.tridentLevel)
        {
            case 1:
                Player.Instance.tridentCooldown = 0.9f; break;
            case 2:
                Player.Instance.tridentCooldown = 0.9f; break;
            case 3:
                Player.Instance.tridentCooldown = 0.8f; break;
            case 4:
                Player.Instance.tridentCooldown = 0.8f; break;
            case 5:
                Player.Instance.tridentCooldown = 0.7f; break;
            case 6:
                Player.Instance.tridentCooldown = 0.7f; break;
            case 7:
                Player.Instance.tridentCooldown = 0.5f; break;
        }

        PrintExplanation();
        gameObject.transform.parent.parent.gameObject.GetComponent<StoreItems>().PrintFieldMoney();
        Managers.Instance.buyCheckAction();
        buyButton.interactable = false;

        Player.Instance.TridentAction();
    }

    //구매가능여부체크
    public void BuyCheck()
    {
        if (priceValue > Managers.fieldMoney)
            price.color = Color.red;
        else
            price.color = Color.white;
    }

    //공격스킬 잠그기(공격스킬 4개 모두 정해졌을때)
    void SkillLock()
    {
        if(Player.Instance.tridentLevel == 0)
            buyButton.interactable = false;
    }
}
