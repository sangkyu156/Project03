using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider playerSlider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        playerSlider.maxValue = health;
        playerSlider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        playerSlider.value = health;

        fill.color = gradient.Evaluate(playerSlider.normalizedValue);
    }
}
