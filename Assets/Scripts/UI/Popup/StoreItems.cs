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

        //여기다 플레이어 공격스킬 4개 정해졌으면 정해진 4개 뺴고 다 삭제 & 랩 7인것도 빼야함 해야함
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

        SetSkills();
    }

    //다시 뽑기
    public void Redraw()
    {
        if (Managers.fieldMoney < 200)
        {
            //사운드 해야함
            //GameManager.Instance.SFXPlay(GameManager.Sfx.DonotBuy);
            return;
        }

        //GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        Managers.fieldMoney -= 200; //돈차감
        //해야함
        //AchievementManager.Instance.redrawCount++;

        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i].transform.GetChild(0).gameObject);
        }
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
