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
    public int paymentGold;//사용한 금화
    public int clearRewardDiamond;
    public int countBG = 0;
    public int storCount = 0;
    //public int curStage = 1;

    //0 -> 완료하지 못함, 1 -> 완료 해서 보상을 받을 준비됨, 2 -> 이미 보상을 완료함
    //돼지 100마리 잡기
    public int achievement01 = 0;
    public int pigCount = 0;

    //돼지 500마리집기

    public int achievement02 = 0;
    public int tryCount = 0;

    //돼지 1000마리집기
    public int achievement03 = 0;

    //3번 도전하기
    public int achievement04 = 0;

    //'다시뽑기'3번 구매
    public int achievement05 = 0;
    public int redrawCount = 0;

    //'다시뽑기'10번 구매
    public int achievement06 = 0;

    //'다시뽑기'50번 구매
    public int achievement07 = 0;

    //'레전드'등급 스킬 10번 구매
    public int achievement08 = 0;
    public int legendSkillCount = 0;

    //'레전드'등급 스킬 50번 구매
    public int achievement09 = 0;

    //'레전드'등급 스킬 100번 구매
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
