using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealText : MonoBehaviour
{
    TextMeshProUGUI healText;

    public float alphaSpeed;
    public string heal;

    void Start()
    {
        healText = GetComponent<TextMeshProUGUI>();
        healText.text = heal;
    }

    void Update()
    {
        healText.color = new Color32(44, 255, 33, 255);
    }
}
