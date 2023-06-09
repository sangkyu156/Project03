using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clairvoyant_Skill : MonoBehaviour
{
    public TextMeshProUGUI text;
    GameObject boss;

    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    void Update()
    {
        if (boss == null)
            return;

        if (Managers.currScene != (int)Define.Scene.Stage)
            return;

        float dis = Player.Instance.transform.position.x - boss.transform.position.x;
        text.text = $"{((int)dis) - 14}";
    }
}
