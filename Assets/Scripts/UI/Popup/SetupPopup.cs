using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetupPopup : UI_Popup
{
    //TODO
    //��� ��ư ������ �����̴��� �Ŵ������� �����ؼ� �� �Ѿ�� ������ ������ �ʱ�ȭ ���Ѿ���
    public Slider bgmSlider;
    public Slider sfxSlider;
    GameObject sound;

    private void Start()
    {
        bgmSlider.value = Managers.bgmVolume;
        sfxSlider.value = Managers.sfxVolume;
        sound = GameObject.FindGameObjectWithTag("Sound");
    }

    public void SetBGMVolme()
    {
        Managers.bgmVolume = bgmSlider.value;
        if(sound == null)
            sound = GameObject.FindGameObjectWithTag("Sound");
        else
            sound.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().volume = bgmSlider.value;
    }

    public void SetSFXVolme()
    {
        Managers.sfxVolume = sfxSlider.value;
        if (sound == null)
            sound = GameObject.FindGameObjectWithTag("Sound");
        else
            sound.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().volume = sfxSlider.value;
    }

    public void SetUpPopupOff()
    {
        Managers.Sound.Play("Button01");
        Destroy(this.gameObject);
    }

    public void LanguageEnglishChoice()
    {
        Managers.Sound.Play("Button01");
        TextUtil.languageNumber = 2;
    }

    public void LanguageKoreanChoice()
    {
        Managers.Sound.Play("Button01");
        TextUtil.languageNumber = 1;
    }
}
