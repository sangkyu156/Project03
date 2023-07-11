using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreItems : MonoBehaviour
{
    GameObject stageScene;
    GameObject[] items;
    string[] skillNameArray; //스킬명 모와두는곳
    int[] skillWeightedArray; //스킬 가중치 모와두는곳
    public WRandom.WeightedRandomPicker<string> weightedRandom = new WRandom.WeightedRandomPicker<string>(); //'가중치랜덤' 변수 생성 & 초기화

    private void Awake()
    {
        AddSkills();

        items = new GameObject[4];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = this.transform.GetChild(i).gameObject;
        }
    }

    void Start()
    {
        Time.timeScale = 0;
        Managers.Data.storCount++;
        Player.Instance.firstStore = false;

        SetSkills();
        stageScene = GameObject.FindGameObjectWithTag("StageScene");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OverlapRedraw();
        }
    }

    //랜덤으로 뽑을 스킬 추가
    void AddSkills()
    {
        skillNameArray = new string[Enum.GetValues(typeof(Define.Skills)).Length];
        skillWeightedArray = new int[Enum.GetValues(typeof(Define.Skills)).Length];

        skillNameArray = typeof(Define.Skills).GetEnumNames();//'Skills'에서 이름 있는 것들만 뽑아오기 (skillNameArray 세팅)
        SetSkillWeightedArray();//스킬 가중치 모와두기 (skillWeightedArray 세팅)

        //스킬,가중치 하나씩 추가 (가중치가 높을수록 잘뽑힘)
        for (int i = 0; i < skillNameArray.Length; i++)
            weightedRandom.Add($"{skillNameArray[i]}", skillWeightedArray[i]);

        //여기다 플레이어 공격스킬 4개 정해졌으면 정해진 4개 뺴고 다 삭제 & 만랩 인 스킬도 삭제
        if(Player.Instance.attackSkillCount >= 4)
        {
            Debug.Log($"스킬 4개라 들어옴 = {Player.Instance.attackSkillCount}");
            RemoveSkill();
        }

        //천리안 스킬 이미 획득했으면 목록에서 삭제
        if (Player.Instance.clairvoyant == true)
            weightedRandom.Remove("Clairvoyant_Store");
    }

    void SkillsReset()
    {
        for (int i = 0; i < skillNameArray.Length; i++)
            weightedRandom.Remove($"{skillNameArray[i]}");
    }

    void RemoveSkill()
    {
        if (Player.Instance.fireBallLevel == 0 || Player.Instance.fireBallLevel == 7)
            weightedRandom.Remove("FireBall_Store");
        if (Player.Instance.tornadoLevel == 0 || Player.Instance.tornadoLevel == 7)
            weightedRandom.Remove("Tornado_Store");
        if (Player.Instance.blackholeLevel == 0 || Player.Instance.blackholeLevel == 7)
            weightedRandom.Remove("BlackHole_Store");
        if (Player.Instance.sawBladeLevel == 0 || Player.Instance.sawBladeLevel == 7)
            weightedRandom.Remove("SawBlade_Store");
        if (Player.Instance.sparkLevel == 0 || Player.Instance.sparkLevel == 7)
            weightedRandom.Remove("Spark_Store");
        if (Player.Instance.waveEnergyLevel == 0 || Player.Instance.waveEnergyLevel == 7)
            weightedRandom.Remove("WaveEnergy_Store");
        if (Player.Instance.volcanoLevel == 0 || Player.Instance.volcanoLevel == 7)
            weightedRandom.Remove("Volcano_Store");
        if (Player.Instance.tridentLevel == 0 || Player.Instance.tridentLevel == 7)
            weightedRandom.Remove("Trident_Store");
        if (Player.Instance.rageExplosionLevel == 0 || Player.Instance.rageExplosionLevel == 7)
            weightedRandom.Remove("RageExplosion_Store");
    }

    //'SkillData.Skills'에서 key를 하나씩 넣어서 key에 해당하는 value를 저장하는 함수
    void SetSkillWeightedArray()
    {
        for (int i = 0; i < skillNameArray.Length; i++)
        {
            skillWeightedArray[i] = (int)Enum.Parse(typeof(Define.Skills), skillNameArray[i]);
        }
    }

    //상점 팝업에 랜덤으로, 중복 안되게 스킬 배치
    void SetSkills()
    {
        GameObject prefeb;
        GameObject skill;
        string skillName = "";
        bool overlap = false;
        List<string> skillList = new List<string>();

        for (int i = 0; i < items.Length; i++)
        {
            skillName = weightedRandom.GetRandomPick();
            skillList.Add(skillName);

            prefeb = Resources.Load<GameObject>($"Prefabs/Skills/{skillName}");//프리펩 찾음
            skill = Instantiate(prefeb);//프리펩 생성
            skill.transform.SetParent(items[i].transform, false);//부모 지정
            skill.transform.SetAsFirstSibling();//생성된 오브젝트 맨위로
        }

        overlap = skillList.GroupBy(x => x).All(g => g.Count() == 1);//뽑은 스킬리스트안에 모든 개별 요소의 개수가 1개씩인지 체크
        if (overlap == false)
        {
            OverlapRedraw();
        }
    }

    //생성할때 스킬이 중복으로 걸렸을때 다시뽑기
    public void OverlapRedraw()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i].transform.GetChild(0).gameObject);
        }

        //스킬목록 초기화
        SkillsReset();
        //다시 스킬목록 생성
        AddSkills();
        //스킬 뽑기
        SetSkills();
    }

    //다시 뽑기
    public void Redraw()
    {
        if (Managers.fieldMoney < 200)
        {
            Managers.Sound.Play("DonotBuy");
            return;
        }

        Managers.Sound.Play("Button01");

        Managers.fieldMoney -= 200; //돈차감
        Managers.Data.redrawCount++;

        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i].transform.GetChild(0).gameObject);
        }

        //스킬목록 초기화
        SkillsReset();
        //다시 스킬목록 생성
        AddSkills();

        SetSkills();

        PrintFieldMoney();
    }

    public void ClosePopup()
    {
        Time.timeScale = 1.0f;
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void PrintFieldMoney()
    {
        if (stageScene != null)
            stageScene.GetComponent<StageScene>().PrintFieldMoney();
    }
}
