using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreItems : MonoBehaviour
{
    GameObject stageScene;
    GameObject[] items;
    string[] skillNameArray; //��ų�� ��͵δ°�
    int[] skillWeightedArray; //��ų ����ġ ��͵δ°�
    public WRandom.WeightedRandomPicker<string> weightedRandom = new WRandom.WeightedRandomPicker<string>(); //'����ġ����' ���� ���� & �ʱ�ȭ

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

    //�������� ���� ��ų �߰�
    void AddSkills()
    {
        skillNameArray = new string[Enum.GetValues(typeof(Define.Skills)).Length];
        skillWeightedArray = new int[Enum.GetValues(typeof(Define.Skills)).Length];

        skillNameArray = typeof(Define.Skills).GetEnumNames();//'Skills'���� �̸� �ִ� �͵鸸 �̾ƿ��� (skillNameArray ����)
        SetSkillWeightedArray();//��ų ����ġ ��͵α� (skillWeightedArray ����)

        //��ų,����ġ �ϳ��� �߰� (����ġ�� �������� �߻���)
        for (int i = 0; i < skillNameArray.Length; i++)
            weightedRandom.Add($"{skillNameArray[i]}", skillWeightedArray[i]);

        //����� �÷��̾� ���ݽ�ų 4�� ���������� ������ 4�� ���� �� ���� & ���� �� ��ų�� ����
        if(Player.Instance.attackSkillCount >= 4)
        {
            Debug.Log($"��ų 4���� ���� = {Player.Instance.attackSkillCount}");
            RemoveSkill();
        }

        //õ���� ��ų �̹� ȹ�������� ��Ͽ��� ����
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

    //'SkillData.Skills'���� key�� �ϳ��� �־ key�� �ش��ϴ� value�� �����ϴ� �Լ�
    void SetSkillWeightedArray()
    {
        for (int i = 0; i < skillNameArray.Length; i++)
        {
            skillWeightedArray[i] = (int)Enum.Parse(typeof(Define.Skills), skillNameArray[i]);
        }
    }

    //���� �˾��� ��������, �ߺ� �ȵǰ� ��ų ��ġ
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

            prefeb = Resources.Load<GameObject>($"Prefabs/Skills/{skillName}");//������ ã��
            skill = Instantiate(prefeb);//������ ����
            skill.transform.SetParent(items[i].transform, false);//�θ� ����
            skill.transform.SetAsFirstSibling();//������ ������Ʈ ������
        }

        overlap = skillList.GroupBy(x => x).All(g => g.Count() == 1);//���� ��ų����Ʈ�ȿ� ��� ���� ����� ������ 1�������� üũ
        if (overlap == false)
        {
            OverlapRedraw();
        }
    }

    //�����Ҷ� ��ų�� �ߺ����� �ɷ����� �ٽû̱�
    public void OverlapRedraw()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i].transform.GetChild(0).gameObject);
        }

        //��ų��� �ʱ�ȭ
        SkillsReset();
        //�ٽ� ��ų��� ����
        AddSkills();
        //��ų �̱�
        SetSkills();
    }

    //�ٽ� �̱�
    public void Redraw()
    {
        if (Managers.fieldMoney < 200)
        {
            Managers.Sound.Play("DonotBuy");
            return;
        }

        Managers.Sound.Play("Button01");

        Managers.fieldMoney -= 200; //������
        Managers.Data.redrawCount++;

        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i].transform.GetChild(0).gameObject);
        }

        //��ų��� �ʱ�ȭ
        SkillsReset();
        //�ٽ� ��ų��� ����
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
