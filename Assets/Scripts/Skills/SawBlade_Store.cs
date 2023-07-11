using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SawBlade_Store : SawBlade_Skill
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
        if (Player.Instance.sawBladeLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:sawblade");
        }
        else if (Player.Instance.sawBladeLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>톱날</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n공격력 <#FF2D2D>{sb_CurPower}</color>\n톱날 개수 <#FF2D2D>8</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>SawBlade</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nPower <#FF2D2D>{sb_CurPower}</color>\nNnumber Of Saw Blades <#FF2D2D>8</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>톱날</color></size>\n<size=70%>Level {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 1}</color></size>\n\n공격력 {sb_CurPower} -> <#3EFF3E>{sb_NextPower}</color>\n톱날 개수 {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 1}</color>";

                if (Player.Instance.sawBladeLevel == 6)
                    explanation.text = $"<size=120%><#E7E7E7>톱날</color></size>\n<size=70%>Level {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 1}</color></size>\n\n공격력 {sb_CurPower} -> <#3EFF3E>{sb_NextPower}</color>\n톱날 개수 {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 2}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>SawBlade</color></size>\n<size=70%>Level {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 1}</color></size>\n\nPower {sb_CurPower} -> <#3EFF3E>{sb_NextPower}</color>\nNnumber Of Saw Blades {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 1}</color>";

                if (Player.Instance.sawBladeLevel == 6)
                    explanation.text = $"<size=120%><#E7E7E7>SawBlade</color></size>\n<size=70%>Level {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 1}</color></size>\n\nPower {sb_CurPower} -> <#3EFF3E>{sb_NextPower}</color>\nNnumber Of Saw Blades {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 2}</color>";
            }
        }
    }

    public void SawBladeBuy()
    {
        if (Player.Instance.sawBladeLevel >= 7)
            return;

        if (Managers.fieldMoney < priceValue)
        {
            Managers.Sound.Play("DonotBuy");
            return;
        }

        Managers.fieldMoney -= priceValue;
        Managers.Data.paymentGold += priceValue;

        Managers.Sound.Play("Buy");

        if (Player.Instance.sawBladeLevel == 0)
        {
            Player.Instance.sawBladeLevel++;
            Player.Instance.attackSkillCount++;

            if (Player.Instance.attackSkillCount >= 4)
                Managers.Instance.skillLockAction();
        }
        else
            Player.Instance.sawBladeLevel++;

        PrintExplanation();

        if (Player.Instance.firstStore)
            gameObject.transform.parent.parent.gameObject.GetComponent<FirstStoreItems>().PrintFieldMoney();
        else
            gameObject.transform.parent.parent.gameObject.GetComponent<StoreItems>().PrintFieldMoney();

        Managers.Instance.buyCheckAction();
        buyButton.interactable = false;

        Player.Instance.SawBladeAdd();
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
        if (Player.Instance.sawBladeLevel == 0)
            buyButton.interactable = false;
    }
}
