using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trident_Skill : ProjectileSkill, IPoolObject
{
    public string idName;

    private void Awake()
    {
        SetAbility();
    }

    void Start()
    {
        deadTiem = 1f;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            Attack(collision);        
    }

    void Attack(Collider2D collider_)
    {
        EnemyBase enemy;
        enemy = collider_.GetComponent<EnemyBase>();

        enemy.TakeDamage(curPower + Managers.Data.state_Power);
    }

    //처음생성됬을때 실행되는 메소드
    public void OnCreatedInPool()
    {
        SetAbility();

        transform.position = new Vector3(Player.Instance.tridentPos.position.x, Player.Instance.tridentPos.position.y + 1.5f, 0);
    }

    //재사용되서 다시한번 실행될때마다 실행되는 메소드
    public void OnGettingFromPool()
    {
        deadTiem = 1f;
        SetAbility();

        transform.position = new Vector3(Player.Instance.tridentPos.position.x, Player.Instance.tridentPos.position.y + 1.5f, 0);
    }

    //오브젝트 회수
    public override void OnTargetReached()
    {
        if (this.gameObject.activeSelf)
            Player.Instance.ReturnPool(this);
    }


    public override void SetAbility()
    {
        switch (Player.Instance.tridentLevel)
        {
            case 0:
                curPower = 0;
                nextPower = 0;
                nextCooldown = 0;
                break;
            case 1:
                curPower = 11;
                nextPower = 13;
                nextCooldown = 0.9f;
                break;
            case 2:
                curPower = 13;
                nextPower = 15;
                nextCooldown = 0.8f;
                break;
            case 3:
                curPower = 15;
                nextPower = 17;
                nextCooldown = 0.8f;
                break;
            case 4:
                curPower = 17;
                nextPower = 19;
                nextCooldown = 0.7f;
                break;
            case 5:
                curPower = 19;
                nextPower = 22;
                nextCooldown = 0.7f;
                break;
            case 6:
                curPower = 22;
                nextPower = 25;
                nextCooldown = 0.5f;
                break;
            case 7:
                curPower = 25;
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
