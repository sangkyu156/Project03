using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SavedFilePopup : UI_Base
{
    public TextMeshProUGUI[] texts;

    string subPath = string.Empty;

    enum Buttons
    {
        Slot1,
        Slot2,
        Slot3,
        Slot1_1,
        Slot2_1,
        Slot3_1,
    }

    private void Start()
    {
        Init();

        SlotText();
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));//Dictionary에 버튼 종류를 저장함

        //각각의 버튼에 클릭했을때 함수 연결해줌
        GetButton((int)Buttons.Slot1).gameObject.BindEvent(Slot1);
        GetButton((int)Buttons.Slot2).gameObject.BindEvent(Slot2);
        GetButton((int)Buttons.Slot3).gameObject.BindEvent(Slot3);
        GetButton((int)Buttons.Slot1_1).gameObject.BindEvent(Slot1_1);
        GetButton((int)Buttons.Slot2_1).gameObject.BindEvent(Slot2_1);
        GetButton((int)Buttons.Slot3_1).gameObject.BindEvent(Slot3_1);
    }

    public void Slot1(PointerEventData data)
    {
        Managers.SaveLode.SlotButton(0);
    }

    public void Slot2(PointerEventData data)
    {
        Managers.SaveLode.SlotButton(1);
    }

    public void Slot3(PointerEventData data)
    {
        Managers.SaveLode.SlotButton(2);
    }

    public void Slot1_1(PointerEventData data)
    {
        Managers.Sound.Play("Button01");

        Managers.SaveLode.nowSlot = 0;
        Managers.SaveLode.path = Application.persistentDataPath + "0";
        Managers.Resource.Instantiate("UI/Popup/QuestionPopup", transform);
    }

    public void Slot2_1(PointerEventData data)
    {
        Managers.Sound.Play("Button01");

        Managers.SaveLode.nowSlot = 1;
        Managers.SaveLode.path = Application.persistentDataPath + "1";
        Managers.Resource.Instantiate("UI/Popup/QuestionPopup", transform);
    }

    public void Slot3_1(PointerEventData data)
    {
        Managers.Sound.Play("Button01");

        Managers.SaveLode.nowSlot = 2;
        Managers.SaveLode.path = Application.persistentDataPath + "2";
        Managers.Resource.Instantiate("UI/Popup/QuestionPopup", transform);
    }

    public void SlotText()
    {
        subPath = Managers.SaveLode.path.Substring(0, Managers.SaveLode.path.Length - 1);//뒤에 마지막 문자 자르기

        Debug.Log($"subPath = {subPath}");
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(subPath + $"{i}"))
            {
                if (i == 0)
                {
                    Managers.SaveLode.path = subPath + $"{i}";
                    Managers.SaveLode.LoadData();
                    texts[i].text = $"Slot {i + 1}\nSave date/time : {Managers.SaveLode.dateTime0}";
                }

                if (i == 1)
                {
                    Managers.SaveLode.path = subPath + $"{i}";
                    Managers.SaveLode.LoadData();
                    texts[i].text = $"Slot {i + 1}\nSave date/time : {Managers.SaveLode.dateTime1}";
                }

                if (i == 2)
                {
                    Managers.SaveLode.path = subPath + $"{i}";
                    Managers.SaveLode.LoadData();
                    texts[i].text = $"Slot {i + 1}\nSave date/time : {Managers.SaveLode.dateTime2}";
                }
            }
            else
            {
                texts[i].text = TextUtil.GetText("game:start:empty");
            }
        }
    }
}
