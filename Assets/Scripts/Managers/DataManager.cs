using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Action buyAction;
    public int killCount;

    public bool[] stageCheck = new bool[2];
    public int state_PowerLevel = 0;
    public int state_Power = 0;
    public int state_HealthLevel = 0;
    public int state_Health = 0;
    public int state_StartGoldLevel = 0;
    public int state_StartGold = 0;
    public int state_PotionRecoverLevel = 0;
    public int state_PotionRecover = 0;
    public int paymentGold;//����� ��ȭ
    public int clearRewardDiamond;
    public int countBG = 0;
    public int storCount = 0;
    //public int curStage = 1;

    //0 -> �Ϸ����� ����, 1 -> �Ϸ� �ؼ� ������ ���� �غ��, 2 -> �̹� ������ �Ϸ���
    //���� 100���� ���
    public int achievement01 = 0;
    public int pigCount = 0;

    //���� 500��������

    public int achievement02 = 0;
    public int tryCount = 0;

    //���� 1000��������
    public int achievement03 = 0;

    //3�� �����ϱ�
    public int achievement04 = 0;

    //'�ٽû̱�'3�� ����
    public int achievement05 = 0;
    public int redrawCount = 0;

    //'�ٽû̱�'10�� ����
    public int achievement06 = 0;

    //'�ٽû̱�'50�� ����
    public int achievement07 = 0;

    //'������'��� ��ų 10�� ����
    public int achievement08 = 0;
    public int legendSkillCount = 0;

    //'������'��� ��ų 50�� ����
    public int achievement09 = 0;

    //'������'��� ��ų 100�� ����
    public int achievement10 = 0;

    public void AchievementCheck()
    {
        if (pigCount >= 100 && achievement01 == 0)
            achievement01 = 1;

        if (pigCount >= 500 && achievement02 == 0)
            achievement02 = 1;

        if (pigCount >= 1000 && achievement03 == 0)
            achievement03 = 1;

        if (tryCount >= 3 && achievement04 == 0)
            achievement04 = 1;

        if (redrawCount >= 3 && achievement05 == 0)
            achievement05 = 1;

        if (redrawCount >= 10 && achievement06 == 0)
            achievement06 = 1;

        if (redrawCount >= 50 && achievement07 == 0)
            achievement07 = 1;

        if (legendSkillCount >= 10 && achievement08 == 0)
            achievement08 = 1;

        if (legendSkillCount >= 50 && achievement09 == 0)
            achievement09 = 1;

        if (legendSkillCount >= 100 && achievement10 == 0)
            achievement10 = 1;
    }
}
