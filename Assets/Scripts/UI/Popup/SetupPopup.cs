using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetupPopup : UI_Popup
{
    //TODO
    //��� ��ư ������ �����̴��� �Ŵ������� �����ؼ� �� �Ѿ�� ������ ������ �ʱ�ȭ ���Ѿ���
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
