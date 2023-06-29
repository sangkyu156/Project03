using UnityEngine;

public class StageBG : UI_Scene
{
    public int countBG = 0;
    public GameObject[] map;
    GameObject player;

    float dist = 0f;

    private void Update()
    {
        if (Managers.currScene != (int)Define.Scene.Stage)
            return;

        for (int i = 0; i < map.Length; i++)
        {
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player");

            dist = player.transform.position.x - 60;

            if (dist > map[i].transform.position.x)
            {
                map[i].transform.position += new Vector3(150, 0, 0);//맵이동
                countBG++;
                CreateManager.Instance.Create_01();

                //상점 생성
                if (countBG % 2 == 0)
                {
                    GameObject portal = Managers.Resource.Instantiate("Object/Portal");
                    portal.transform.position = new Vector3(map[i].transform.position.x - 50, -3.5f, 0);
                }

                //금화상자 생성
                for (int q = 0; q < Player.Instance.goldChestLevel + 4; q++)
                {
                    if (Random.Range(0, 5) == 2) //20% 확률
                    {
                        float ran_x = Random.Range(20, 48);
                        float ran_y = Random.Range(-9f, -5.5f);
                        GameObject box = Managers.Resource.Instantiate("Object/Box1");
                        box.transform.position = new Vector3(Player.Instance.transform.position.x + ran_x, ran_y, 0);
                    }
                }
            }
        }
    }
}
