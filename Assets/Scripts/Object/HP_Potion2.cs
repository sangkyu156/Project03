using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Potion2 : MonoBehaviour
{
    int speed = 13;

    private void Start()
    {
        speed = 13;
    }

    private void Update()
    {
        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, new Vector2(10, 10), 0);
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].tag == "Player" || targets[i].tag == "NoDamage")
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.Instance.transform.position + new Vector3(0, 1.5f, 0), speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.Instance.GetHP_Potion(Define.Potion.HP_Potion2);

            Destroy(gameObject);
        }
    }
}
