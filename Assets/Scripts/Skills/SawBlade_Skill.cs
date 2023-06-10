using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SawBlade_Skill : MonoBehaviour
{
    static protected int sb_CurPower;
    protected int sb_NextPower;

    private void Awake()
    {
        SetAbility();
    }

    //¿¡³Ê¹Ì¶û ºÎµúÃÆÀ» ¶§
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            Attack(collision);
    }

    void Attack(Collider2D collider_)
    {
        //EnemyBase enemy;
        //enemy = collider_.GetComponent<EnemyBase>();

        //enemy.TakeDamage(sb_CurPower + Player.Instance.playerPower);
    }

    public void SetAbility()
    {
        //switch (Player.Instance.sawBladeLevel)
        //{
        //    case 0:
        //        sb_CurPower = 0;
        //        sb_NextPower = 0;
        //        break;
        //    case 1:
        //        sb_CurPower = 5;
        //        sb_NextPower = 6;
        //        break;
        //    case 2:
        //        sb_CurPower = 6;
        //        sb_NextPower = 7;
        //        break;
        //    case 3:
        //        sb_CurPower = 7;
        //        sb_NextPower = 8;
        //        break;
        //    case 4:
        //        sb_CurPower = 8;
        //        sb_NextPower = 9;
        //        break;
        //    case 5:
        //        sb_CurPower = 9;
        //        sb_NextPower = 10;
        //        break;  
        //    case 6:
        //        sb_CurPower = 11;
        //        sb_NextPower = 13;
        //        break;
        //    case 7:
        //        sb_CurPower = 13;
        //        sb_NextPower = 0;
        //        break;
        //}
    }
}
