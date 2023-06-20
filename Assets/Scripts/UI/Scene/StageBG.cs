using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBG : UI_Scene
{
    public GameObject[] map;
    GameObject player;

    float dist = 0f;

    private void Update()
    {
        if (Managers.currScene != (int)Define.Scene.Stage)
            return;

        for (int i = 0; i < map.Length; i++)
        {
            if(player == null)
                player = GameObject.FindGameObjectWithTag("Player");

            dist = player.transform.position.x - 60;

            if (dist > map[i].transform.position.x)
            {
                map[i].transform.position += new Vector3(150, 0, 0);//∏ ¿Ãµø
            }
        }
    }
}
