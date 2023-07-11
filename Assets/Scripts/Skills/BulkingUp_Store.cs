using System;
using TMPro;

public class BulkingUp_Store : PassiveSkill
{
    public UnityEngine.UI.Button buyButton;

    int addHealth = 0;

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
        if (Player.Instance.bulkingUpLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:bulkingup");
        }
        else if (Player.Instance.bulkingUpLevel == 15)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>벌크 업</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n추가 최대체력 <#FF2D2D>{addHealth * Player.Instance.bulkingUpLevel}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>Bulking Up</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nAdditional Max Health<#FF2D2D>{addHealth * Player.Instance.bulkingUpLevel}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>벌크 업</color></size>\n<size=70%>Level {Player.Instance.bulkingUpLevel} -> <#3EFF3E>{Player.Instance.bulkingUpLevel + 1}</color></size>\n\n추가 최대체력 {addHealth * Player.Instance.bulkingUpLevel} -> <#3EFF3E>{addHealth * Player.Instance.bulkingUpLevel + 3}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>Bulking Up</color></size>\n<size=70%>Level {Player.Instance.bulkingUpLevel} -> <#3EFF3E>{Player.Instance.bulkingUpLevel + 1}</color></size>\n\nAdditional Max Health {addHealth * Player.Instance.bulkingUpLevel} -> <#3EFF3E>{addHealth * Player.Instance.bulkingUpLevel + 3}</color>";
            }
        }
    }

    public void SetAbility()
    {
        addHealth = 3;
    }

    //구매
    public void BulkingUpBuy()
    {
        if (Managers.fieldMoney < priceValue)
        {
            Managers.Sound.Play("DonotBuy");
            return;
        }

        Managers.fieldMoney -= priceValue;
        Managers.Data.paymentGold += priceValue;

        Managers.Sound.Play("Buy");

        Player.Instance.bulkingUpLevel++;

        if (Player.Instance.firstStore)
            gameObject.transform.parent.parent.gameObject.GetComponent<FirstStoreItems>().PrintFieldMoney();
        else
            gameObject.transform.parent.parent.gameObject.GetComponent<StoreItems>().PrintFieldMoney();
        Managers.Instance.buyCheckAction();
        PrintExplanation();

        Player.Instance.maxHealth += addHealth;
        Player.Instance.currentHealth += addHealth;
        Player.Instance.healthBar.SetMaxHealth(Player.Instance.maxHealth);
        Player.Instance.healthBar.SetHealth(Player.Instance.currentHealth);

        buyButton.interactable = false;
    }
}
