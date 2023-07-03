using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado_Skill : ProjectileSkill, IPoolObject
{
    float tornadoSpeed = 0;
    public string idName;

    new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        SetAbility();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        tornadoSpeed = 15;
        deadTiem = 0.8f;
    }

    void Update()
    {
        if (Player.Instance.tornadoLevel <= 0)
            return;

        if (rigidbody2D == null)
            return;

        Shoot();

        if (deadTiem > 0)
        {
            deadTiem -= Time.deltaTime;
        }

        if (deadTiem < 0)
        {
            deadTiem = 0.8f;
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

        if(enemy.knockback == false)
        {
            if(collider_.transform.position.x < transform.position.x)
                collider_.transform.position = new Vector3(collider_.transform.position.x - 2f, collider_.transform.position.y);
            else if(collider_.transform.position.x > transform.position.x)
                collider_.transform.position = new Vector3(collider_.transform.position.x + 2f, collider_.transform.position.y);

            enemy.TakeDamage(curPower + Managers.Data.state_Power);
            enemy.knockback = true;
            enemy.KnockbackSet();
        }
    }

    public override void Shoot()
    {
        rigidbody2D.velocity = new Vector3(tornadoSpeed, 0, 0);
    }

    //처음생성됬을때 실행되는 메소드
    public void OnCreatedInPool()
    {
        SetAbility();
    }

    //재사용되서 다시한번 실행될때마다 실행되는 메소드
    public void OnGettingFromPool()
    {
        deadTiem = 0.8f;
        SetAbility();
    }

    //오브젝트 회수
    public override void OnTargetReached()
    {
        if (this.gameObject.activeSelf)
            Player.Instance.ReturnPool(this);
    }


    public override void SetAbility()
    {
        switch (Player.Instance.tornadoLevel)
        {
            case 0:
                curPower = 0;
                nextPower = 0;
                nextCooldown = 0;
                break;
            case 1:
                curPower = 9;
                nextPower = 10;
                nextCooldown = 2.5f;
                break;
            case 2:
                curPower = 10;
                nextPower = 11;
                nextCooldown = 2.3f;
                break;
            case 3:
                curPower = 11;
                nextPower = 12;
                nextCooldown = 2.2f;
                break;
            case 4:
                curPower = 12;
                nextPower = 13;
                nextCooldown = 2.0f;
                break;
            case 5:
                curPower = 13;
                nextPower = 14;
                nextCooldown = 1.9f;
                break;
            case 6:
                curPower = 14;
                nextPower = 15;
                nextCooldown = 1.6f;
                break;
            case 7:
                curPower = 15;
                nextPower = 0;
                nextCooldown = 0f;
                break;
        }
    }
}
