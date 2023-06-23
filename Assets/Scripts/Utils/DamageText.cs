using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    TextMeshProUGUI damageText;

    public float alphaSpeed;
    public int damage;

    void Start()
    {
        damageText = GetComponent<TextMeshProUGUI>();
        damageText.text = damage.ToString();
    }

    void Update()
    {
        damageText.color = new Color(1f, 0, 0, Mathf.Lerp(damageText.color.a, 0, Time.deltaTime * alphaSpeed));
    }
}
