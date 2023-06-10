using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetupPopup : UI_Popup
{
    //TODO
    //취소 버튼 누를때 슬라이더값 매니져스에 저장해서 씬 넘어가도 저장한 값으로 초기화 시켜야함
    //public AudioMixer audioMixer;
    //public Slider bgmSlider;
    //public Slider sfxSlider;

    //public void SetBGMVolme()
    //{
    //    audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
    //}

    //public void SetSFXVolme()
    //{
    //    audioMixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
    //}

    public void SetUpPopupOff()
    {
        //GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        Destroy(this.gameObject);
    }

    public void LanguageEnglishChoice()
    {
        //GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        TextUtil.languageNumber = 2;
    }

    public void LanguageKoreanChoice()
    {
        //GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        TextUtil.languageNumber = 1;
    }
}
