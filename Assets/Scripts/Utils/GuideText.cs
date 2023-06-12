using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideText : MonoBehaviour
{
    TextMeshProUGUI guideText;

    public float alphaSpeed = 2f;

    void Start()
    {
        guideText = GetComponent<TextMeshProUGUI>();
        if(TextUtil.languageNumber == 1) //�ѱ���
        {
            guideText.text = "�ڷδ� ���� �����ϴ�!";
        }
        else
        {
            guideText.text = "You can't go back!";
        }
    }

    void Update()
    {
        guideText.color = new Color(0.9f, 0.9f, 0.9f, Mathf.Lerp(guideText.color.a, 0, Time.deltaTime * alphaSpeed));
    }
}
