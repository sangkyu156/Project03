using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Action buyAction;
    public int killCount;

    public bool clairvoyant = false;
    public int state_PowerLevel = 0;
    public int state_Power = 0;
    public int state_HealthLevel = 0;
    public int state_Health = 0;
    public int state_StartGoldLevel = 0;
    public int state_StartGold = 0;
    public int state_PotionRecoverLevel = 0;
    public int state_PotionRecover = 0;
    public int paymentGold;//����� ��ȭ
    public int curStage = 1;

    //0 -> �Ϸ����� ����, 1 -> �Ϸ� �ؼ� ������ ���� �غ��, 2 -> �̹� ������ �Ϸ���
    //���� 1000���� ���
    public int achievement01 = 0;
    public int pigCount = 0;

    //3�� �����ϱ�
    public int achievement02 = 0;
    public int tryCount = 0;

    //���� 100��������
    public int achievement03 = 0;

    //���� 500��������
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
}
