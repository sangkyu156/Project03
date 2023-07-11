using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Regular_Store : MonoBehaviour
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

        buyButton.transform.SetAsLastSibling();//��ư���� �Ʒ��� ��ġ

        PrintExplanation();
    }

    private void OnDestroy()
    {
        Managers.Instance.buyCheckAction -= BuyCheck;
    }

    //���� �ؽ�Ʈ ���
    void PrintExplanation()
    {
        if (Player.Instance.regularLevel == 0)
            explanation.text = TextUtil.GetText("game:skill:explanation:regular");
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
                explanation.text = $"<size=120%><#32FFC8>�ܰ�</color></size>\n<size=70%>Level {Player.Instance.regularLevel} -> <#3EFF3E>{Player.Instance.regularLevel + 1}</color></size>\n\n'����' ���� Ȯ���� �����߽��ϴ�.";
            else if (TextUtil.languageNumber == 2) //�̱�
                explanation.text = $"<size=120%><#32FFC8>Regular</color></size>\n<size=70%>Level {Player.Instance.regularLevel} -> <#3EFF3E>{Player.Instance.regularLevel + 1}</color></size>\n\nThe probability of a 'Store' appearing has increased.";
        }
    }

    //����
    public void RegularBuy()
    {
        if (Managers.fieldMoney < priceValue)
        {
            //GameManager.Instance.SFXPlay(GameManager.Sfx.DonotBuy);
            return;
        }

        Managers.fieldMoney -= priceValue;
        Managers.Data.paymentGold += priceValue;
        //GameManager.Instance.SFXPlay(GameManager.Sfx.Buy);

        Player.Instance.regularLevel++;

        if (Player.Instance.firstStore)
            gameObject.transform.parent.parent.gameObject.GetComponent<FirstStoreItems>().PrintFieldMoney();
        else
            gameObject.transform.parent.parent.gameObject.GetComponent<StoreItems>().PrintFieldMoney();

        Managers.Instance.buyCheckAction();
        PrintExplanation();

        buyButton.interactable = false;
    }

    //���Ű��ɿ���üũ
    public void BuyCheck()
    {
        if (priceValue > Managers.fieldMoney)
            price.color = Color.red;
        else
            price.color = Color.white;
    }
}
