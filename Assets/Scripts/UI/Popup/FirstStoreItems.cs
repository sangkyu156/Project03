using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FirstStoreItems : MonoBehaviour
{
    GameObject[] items;
    GameObject stageScene;

    string[] firstSkillArray; //첫 상점에서 나와야 하는 스킬 모와두는곳
    int[] firstSkillWeightedArray; //첫 상점에서 나와야 하는 스킬 가중치 모와두는곳
    public WRandom.WeightedRandomPicker<string> weightedRandomFirst = new WRandom.WeightedRandomPicker<string>(); //'가중치랜덤' 변수 생성 & 초기화

    private void Awake()
    {
        firstAddSkills();
        items = new GameObject[4];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = this.transform.GetChild(i).gameObject;
        }
    }

    void Start()
    {
        Managers.Data.storCount++;
        Player.Instance.firstStore = true;
        stageScene = GameObject.FindGameObjectWithTag("StageScene");
        SetSkills();
    }

    //첫 상점에서 랜덤으로 뽑을 스킬 추가
    void firstAddSkills()
    {
        firstSkillArray = new string[8];
        firstSkillArray[0] = "FireBall_Store";
        firstSkillArray[1] = "SawBlade_Store";
        firstSkillArray[2] = "WaveEnergy_Store";
        firstSkillArray[3] = "Tornado_Store";
        firstSkillArray[4] = "Spark_Store";
        firstSkillArray[5] = "Trident_Store";
        firstSkillArray[6] = "RageExplosion_Store";
        firstSkillArray[7] = "Volcano_Store";

        firstSkillWeightedArray = new int[8];
        firstSkillWeightedArray[0] = 1000;
        firstSkillWeightedArray[1] = 1001;
        firstSkillWeightedArray[2] = 1002;
        firstSkillWeightedArray[3] = 600;
        firstSkillWeightedArray[4] = 601;
        firstSkillWeightedArray[5] = 602;
        firstSkillWeightedArray[6] = 603;
        firstSkillWeightedArray[7] = 31;

        for (int i = 0; i < 8; i++)
        {
            weightedRandomFirst.Add($"{firstSkillArray[i]}", firstSkillWeightedArray[i]);
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
            skillName = weightedRandomFirst.GetRandomPick();
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

        if (stageScene != null)
            stageScene.GetComponent<StageScene>().PrintFieldMoney();
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
