using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Redraw3_Store : MonoBehaviour
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;

    void Start()
    {
        //GameManager.Instance.SFXPlay(GameManager.Sfx.LegendarySkill);

        price.text = "+100";
        priceValue = Int32.Parse("100");

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
        explanation.text = TextUtil.GetText("game:skill:explanation:redraw3");
    }

    //구매
    public void RedrawBuy3()
    {
        //GameManager.Instance.SFXPlay(GameManager.Sfx.Buy);

        Managers.fieldMoney += priceValue;
        Managers.Data.legendSkillCount++;

        if (Player.Instance.firstStore)
            gameObject.transform.parent.parent.gameObject.GetComponent<FirstStoreItems>().PrintFieldMoney();
        else
            gameObject.transform.parent.parent.gameObject.GetComponent<StoreItems>().PrintFieldMoney();

        Managers.Instance.buyCheckAction();

        gameObject.transform.parent.parent.gameObject.GetComponent<StoreItems>().OverlapRedraw();//다시 뽑기
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
