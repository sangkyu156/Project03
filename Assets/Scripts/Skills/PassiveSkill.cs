using TMPro;
using UnityEngine;

public class PassiveSkill : MonoBehaviour
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    protected int priceValue = 0;

    //���Ű��ɿ���üũ
    public void BuyCheck()
    {
        if (priceValue > Managers.fieldMoney)
            price.color = Color.red;
        else
            price.color = Color.white;
    }
}
