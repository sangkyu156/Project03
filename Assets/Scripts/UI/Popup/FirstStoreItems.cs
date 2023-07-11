using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FirstStoreItems : MonoBehaviour
{
    GameObject[] items;
    GameObject stageScene;

    string[] firstSkillArray; //ù �������� ���;� �ϴ� ��ų ��͵δ°�
    int[] firstSkillWeightedArray; //ù �������� ���;� �ϴ� ��ų ����ġ ��͵δ°�
    public WRandom.WeightedRandomPicker<string> weightedRandomFirst = new WRandom.WeightedRandomPicker<string>(); //'����ġ����' ���� ���� & �ʱ�ȭ

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

    //ù �������� �������� ���� ��ų �߰�
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
            skillName = weightedRandomFirst.GetRandomPick();
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
        SetSkills();
    }

    //�ٽ� �̱�
    public void Redraw()
    {
        if (Managers.fieldMoney < 200)
        {
            //���� �ؾ���
            //GameManager.Instance.SFXPlay(GameManager.Sfx.DonotBuy);
            return;
        }

        //GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        Managers.fieldMoney -= 200; //������
        //�ؾ���
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
