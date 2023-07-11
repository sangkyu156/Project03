using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RageExplosion_Store : RageExplosion_Skill
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
        if (Player.Instance.rageExplosionLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:rageexplosion");
        }
        else if (Player.Instance.rageExplosionLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>분노 폭발</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n공격력 <#FF2D2D>{curPower}</color>\n공격속도 <#FF2D2D>{rageExplosionCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>Rage Explosion</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nPower <#FF2D2D>{curPower}</color>\nCooldown <#FF2D2D>{rageExplosionCooldown}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>분노 폭발</color></size>\n<size=70%>Level {Player.Instance.rageExplosionLevel} -> <#3EFF3E>{Player.Instance.rageExplosionLevel + 1}</color></size>\n\n공격력 {curPower} -> <#3EFF3E>{nextPower}</color>\n공격속도 {rageExplosionCooldown} -> <#3EFF3E>{rageExplosionCooldown - 0.1f}</color>\n크기 증가";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>Rage Explosion</color></size>\n<size=70%>Level {Player.Instance.rageExplosionLevel} -> <#3EFF3E>{Player.Instance.rageExplosionLevel + 1}</color></size>\n\nPower {curPower} -> <#3EFF3E>{nextPower}</color>\nCooldown {rageExplosionCooldown} -> <#3EFF3E>{rageExplosionCooldown - 0.1f}</color>\nIncrease in size";
            }
        }
    }

    public void RageExplosionBuy()
    {
        if (Player.Instance.rageExplosionLevel >= 7)
            return;

        if (Managers.fieldMoney < priceValue)
        {
            Managers.Sound.Play("DonotBuy");
            return;
        }

        Managers.fieldMoney -= priceValue;
        Managers.Data.paymentGold += priceValue;

        Managers.Sound.Play("Buy");

        if (Player.Instance.rageExplosionLevel == 0)
        {
            Player.Instance.rageExplosionLevel++;
            Player.Instance.attackSkillCount++;

            if (Player.Instance.attackSkillCount >= 4)
                Managers.Instance.skillLockAction();
        }
        else
            Player.Instance.rageExplosionLevel++;

        PrintExplanation();

        if (Player.Instance.firstStore)
            gameObject.transform.parent.parent.gameObject.GetComponent<FirstStoreItems>().PrintFieldMoney();
        else
            gameObject.transform.parent.parent.gameObject.GetComponent<StoreItems>().PrintFieldMoney();

        Managers.Instance.buyCheckAction();

        buyButton.interactable = false;

        Player.Instance.RageExplosionAction();
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
        if (Player.Instance.rageExplosionLevel == 0)
            buyButton.interactable = false;
    }
}
