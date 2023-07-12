using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TempData
{
    public string t_Name;
    public bool[] t_StageCheck = new bool[2];
    public int t_MainDiamond;
    public int t_Achievement01;
    public int t_Achievement02;
    public int t_Achievement03;
    public int t_Achievement04;
    public int t_Achievement05;
    public int t_Achievement06;
    public int t_Achievement07;
    public int t_Achievement08;
    public int t_Achievement09;
    public int t_Achievement10;
    public int t_PigCount;
    public int t_TryCount;
    public int t_RedrawCount;
    public int t_LegendSkillCount;
    public string t_DateTime0;
    public string t_DateTime1;
    public string t_DateTime2;
    public int t_State_PowerLevel;
    public int t_State_Power;
    public int t_State_HealthLevel;
    public int t_State_Health;
    public int t_State_StartGoldLevel;
    public int t_State_StartGold;
    public int t_State_PotionRecoverLevel;
    public int t_State_PotionRecover;
}

public class SLManager 
{
    private static DataManager instance = null;

    public TempData temp = new TempData();

    public string path;
    public int nowSlot;
    public string data;
    public string dateTime0;
    public string dateTime1;
    public string dateTime2;

    public void Init()
    {
        GameObject root = GameObject.Find("@SLManager");
        if (root == null)
        {
            root = new GameObject { name = "@SLManager" };
            UnityEngine.Object.DontDestroyOnLoad(root);
        }

        path = Application.persistentDataPath + nowSlot.ToString();
        Debug.Log($"저장경로 = {path}");
    }

    public void SaveData()//    Slot 3\nSave date/time : 2023-04-09 (22:54)
    {
        string subPath;

        ToJson();

        data = JsonUtility.ToJson(temp);
        subPath = path.Substring(0, path.Length - 1);//뒤에 마지막 문자 자르기
        path = subPath + $"{nowSlot}";
        File.WriteAllText(path, data);

        Debug.Log($"저장함 - {path}");
    }

    public void ToJson()
    {
        temp.t_Name = "";
        temp.t_MainDiamond = Managers.diamond;
        temp.t_Achievement01 = Managers.Data.achievement01;
        temp.t_Achievement02 = Managers.Data.achievement02;
        temp.t_Achievement03 = Managers.Data.achievement03;
        temp.t_Achievement04 = Managers.Data.achievement04;
        temp.t_Achievement05 = Managers.Data.achievement05;
        temp.t_Achievement06 = Managers.Data.achievement06;
        temp.t_Achievement07 = Managers.Data.achievement07;
        temp.t_Achievement08 = Managers.Data.achievement08;
        temp.t_Achievement09 = Managers.Data.achievement09;
        temp.t_Achievement10 = Managers.Data.achievement10;
        temp.t_PigCount = Managers.Data.pigCount;
        temp.t_TryCount = Managers.Data.tryCount;
        temp.t_RedrawCount = Managers.Data.redrawCount;
        temp.t_LegendSkillCount = Managers.Data.legendSkillCount;
        temp.t_State_PowerLevel = Managers.Data.state_PowerLevel;
        temp.t_State_Power = Managers.Data.state_PowerLevel;
        temp.t_State_HealthLevel = Managers.Data.state_HealthLevel;
        temp.t_State_Health = Managers.Data.state_Health;
        temp.t_State_StartGoldLevel = Managers.Data.state_StartGoldLevel;
        temp.t_State_StartGold = Managers.Data.state_StartGold;
        temp.t_State_PotionRecoverLevel = Managers.Data.state_PotionRecoverLevel;
        temp.t_State_PotionRecover = Managers.Data.state_PotionRecover;
        for (int i = 0; i < temp.t_StageCheck.Length; i++)
        {
            temp.t_StageCheck[i] = Managers.Data.stageCheck[i];
        }
        switch (nowSlot)
        {
            case 0:
                temp.t_DateTime0 = DateTime.Now.ToString(("yyyy-MM-dd (HH:mm)"));
                break;
            case 1:
                temp.t_DateTime1 = DateTime.Now.ToString(("yyyy-MM-dd (HH:mm)"));
                break;
            case 2:
                temp.t_DateTime2 = DateTime.Now.ToString(("yyyy-MM-dd (HH:mm)"));
                break;
        }
    }

    public void LoadData()
    {
        data = File.ReadAllText(path);
        temp = JsonUtility.FromJson<TempData>(data);

        FromJson();
    }

    public void FromJson()
    {
        Managers.diamond = temp.t_MainDiamond;
        Managers.Data.achievement01 = temp.t_Achievement01;
        Managers.Data.achievement02 = temp.t_Achievement02;
        Managers.Data.achievement03 = temp.t_Achievement03;
        Managers.Data.achievement04 = temp.t_Achievement04;
        Managers.Data.achievement05 = temp.t_Achievement05;
        Managers.Data.achievement06 = temp.t_Achievement06;
        Managers.Data.achievement07 = temp.t_Achievement07;
        Managers.Data.achievement08 = temp.t_Achievement08;
        Managers.Data.achievement09 = temp.t_Achievement09;
        Managers.Data.achievement10 = temp.t_Achievement10;
        Managers.Data.pigCount = temp.t_PigCount;
        Managers.Data.redrawCount = temp.t_RedrawCount;
        Managers.Data.legendSkillCount = temp.t_LegendSkillCount;
        Managers.Data.state_PowerLevel = temp.t_State_PowerLevel;
        Managers.Data.state_Power = temp.t_State_PowerLevel;
        Managers.Data.state_HealthLevel = temp.t_State_HealthLevel;
        Managers.Data.state_Health = temp.t_State_Health;
        Managers.Data.state_StartGoldLevel = temp.t_State_StartGoldLevel;
        Managers.Data.state_StartGold = temp.t_State_StartGold;
        Managers.Data.state_PotionRecoverLevel = temp.t_State_PotionRecoverLevel;
        Managers.Data.state_PotionRecover = temp.t_State_PotionRecover;
        for (int i = 0; i < Managers.Data.stageCheck.Length; i++)
        {
            Managers.Data.stageCheck[i] = temp.t_StageCheck[i];
        }
        for (int q = 0; q < 3; q++)
        {
            if (q == 0)
                dateTime0 = temp.t_DateTime0;
            if (q == 1)
                dateTime1 = temp.t_DateTime1;
            if (q == 2)
                dateTime2 = temp.t_DateTime2;
        }
    }

    public void SlotButton(int slotNum)
    {
        nowSlot = slotNum;
        path = Application.persistentDataPath + nowSlot.ToString();

        if (File.Exists(path))
        {
            LoadData();
            GoMain();
        }
        else
        {
            DataClear();
            GoIntro();
        }
    }

    public void GoIntro()
    {
        Managers.currScene = (int)Define.Scene.Intro;
        Managers.Sound.Play("Button01");
        Time.timeScale = 1.0f;

        Managers.Scene.LoadScene(Define.Scene.Intro);
    }

    public void GoMain()
    {
        Managers.currScene = (int)Define.Scene.Main;
        Managers.Sound.Play("Button01");
        Time.timeScale = 1.0f;

        Managers.Scene.LoadScene(Define.Scene.Main);
    }

    public void DataClear()
    {
        temp = new TempData();
        Managers.diamond = 0;
        Managers.Data.achievement01 = 0;
        Managers.Data.achievement02 = 0;
        Managers.Data.achievement03 = 0;
        Managers.Data.achievement04 = 0;
        Managers.Data.achievement05 = 0;
        Managers.Data.achievement06 = 0;
        Managers.Data.achievement07 = 0;
        Managers.Data.achievement08 = 0;
        Managers.Data.achievement09 = 0;
        Managers.Data.achievement10 = 0;
        Managers.Data.pigCount = 0;
        Managers.Data.redrawCount = 0;
        Managers.Data.legendSkillCount = 0;
        Managers.Data.state_PowerLevel = 0;
        Managers.Data.state_Power = 0;
        Managers.Data.state_HealthLevel = 0;
        Managers.Data.state_Health = 0;
        Managers.Data.state_StartGoldLevel = 0;
        Managers.Data.state_StartGold = 0;
        Managers.Data.state_PotionRecoverLevel = 0;
        Managers.Data.state_PotionRecover = 0;
        for (int i = 0; i < Managers.Data.stageCheck.Length; i++)
        {
            Managers.Data.stageCheck[i] = false;
        }
    }
}
