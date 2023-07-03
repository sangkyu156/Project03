using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano_Skill : ProjectileSkill, IPoolObject
{
    public string idName;

    private void Awake()
    {
        SetAbility();
    }

    void Start()
    {
        deadTiem = 0.8f;
    }

    void Update()
    {
        if (deadTiem > 0)
        {
            deadTiem -= Time.deltaTime;
            if (deadTiem < 0)
                OnTargetReached();
        }
    }

    //에너미랑 부딪쳤을 때

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            Attack(collision);
    }

    void Attack(Collider2D collider_)
    {
        EnemyBase enemy;
        enemy = collider_.GetComponent<EnemyBase>();

        enemy.TakeDamage(curPower + (int)(Managers.Data.state_Power * 0.3f));
    }

    //처음생성됬을때 실행되는 메소드
    public void OnCreatedInPool()
    {
        SetAbility();

        transform.position = new Vector3(Player.Instance.volcanoPos.position.x, Player.Instance.volcanoPos.position.y + 1.5f, 0);
    }

    //재사용되서 다시한번 실행될때마다 실행되는 메소드
    public void OnGettingFromPool()
    {
        deadTiem = 0.8f;
        SetAbility();

        transform.position = new Vector3(Player.Instance.volcanoPos.position.x, Player.Instance.volcanoPos.position.y + 1.5f, 0);
    }

    //오브젝트 회수
    public override void OnTargetReached()
    {
        if (this.gameObject.activeSelf)
            Player.Instance.ReturnPool(this);
    }


    public override void SetAbility()
    {
        switch (Player.Instance.volcanoLevel)
        {
            case 0:
                curPower = 0;
                nextPower = 0;
                nextCooldown = 0;
                break;
            case 1:
                curPower = 1;
                nextPower = 2;
                nextCooldown = 1.9f;
                break;
            case 2:
                curPower = 2;
                nextPower = 3;
                nextCooldown = 1.8f;
                break;
            case 3:
                curPower = 3;
                nextPower = 4;
                nextCooldown = 1.7f;
                break;
            case 4:
                curPower = 5;
                nextPower = 6;
                nextCooldown = 1.6f;
                break;
            case 5:
                curPower = 6;
                nextPower = 7;
                nextCooldown = 1.5f;
                break;
            case 6:
                curPower = 7;
                nextPower = 8;
                nextCooldown = 1.3f;
                break;
            case 7:
                curPower = 10;
                nextPower = 0;
                nextCooldown = 0f;
                break;
        }
    }

    public override void Shoot()
    {
        //비어있음
    }
}
