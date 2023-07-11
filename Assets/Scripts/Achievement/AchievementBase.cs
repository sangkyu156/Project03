using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementBase : MonoBehaviour
{
    public GameObject button1;//완료하지 못함
    public GameObject button2;//완료 해서 보상을 받을 준비됨
    public GameObject button3;//이미 보상을 완료함
    public Slider slider;

    protected virtual void SetSlider()
    {

    }
}
