using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnergy_Skill : ProjectileSkill, IPoolObject
{
    float waveEnergySpeed = 0;
    public string idName;

    new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        SetAbility();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        waveEnergySpeed = 11;
        deadTiem = 0.8f;
    }

    void Update()
    {
        if (Player.Instance.waveEnergyLevel <= 0)
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

        enemy.TakeDamage(curPower + Managers.Data.state_Power);
    }

    public override void Shoot()
    {
        rigidbody2D.velocity = new Vector3(waveEnergySpeed, 0, 0);
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
        switch (Player.Instance.waveEnergyLevel)
        {
            case 0:
                curPower = 0;
                nextPower = 0;
                nextCooldown = 0;
                break;
            case 1:
                curPower = 7;
                nextPower = 8;
                nextCooldown = 1.6f;
                break;
            case 2:
                curPower = 8;
                nextPower = 9;
                nextCooldown = 1.5f;
                break;
            case 3:
                curPower = 9;
                nextPower = 10;
                nextCooldown = 1.4f;
                break;
            case 4:
                curPower = 10;
                nextPower = 11;
                nextCooldown = 1.3f;
                break;
            case 5:
                curPower = 11;
                nextPower = 12;
                nextCooldown = 1.2f;
                break;
            case 6:
                curPower = 12;
                nextPower = 15;
                nextCooldown = 1.0f;
                break;
            case 7:
                curPower = 15;
                nextPower = 0;
                nextCooldown = 0f;
                break;
        }
    }

}
