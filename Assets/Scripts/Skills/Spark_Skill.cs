using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Spark_Skill : ProjectileSkill, IPoolObject
{
    float sparkSpeed = 0;
    public string idName;

    Animator animator;
    BoxCollider2D skillCollider;
    new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        SetAbility();
        animator = GetComponent<Animator>();
        skillCollider = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        sparkSpeed = 22;
        deadTiem = 0.8f;
    }

    void Update()
    {
        if (Player.Instance.sparkLevel <= 0)
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

    //레벨에 따라서 능력치 세팅
    public override void SetAbility()
    {
        switch (Player.Instance.sparkLevel)
        {
            case 0:
                curPower = 0;
                nextPower = 0;
                nextCooldown = 0;
                break;
            case 1:
                curPower = 10;
                nextPower = 12;
                nextCooldown = 0.4f;
                break;
            case 2:
                curPower = 12;
                nextPower = 14;
                nextCooldown = 0.4f;
                break;
            case 3:
                curPower = 14;
                nextPower = 16;
                nextCooldown = 0.3f;
                break;
            case 4:
                curPower = 16;
                nextPower = 18;
                nextCooldown = 0.3f;
                break;
            case 5:
                curPower = 20;
                nextPower = 22;
                nextCooldown = 0.3f;
                break;
            case 6:
                curPower = 22;
                nextPower = 25;
                nextCooldown = 0.2f;
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
        if (animator.GetBool("hit") == false)
            rigidbody2D.velocity = new Vector3(sparkSpeed, 0, 0);
        else if (animator.GetBool("hit") == true)
            rigidbody2D.velocity = Vector3.zero;
    }

    //오브젝트 회수
    public override void OnTargetReached()
    {
        if (this.gameObject.activeSelf)
            Player.Instance.ReturnPool(this);
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
        skillCollider.enabled = true;
        animator.SetBool("hit", false);
        SetAbility();
    }

    //에너미랑 부딪쳤을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            animator.SetBool("hit", true);
            skillCollider.enabled = false;
            EnemyBase enemy;

            enemy = collision.GetComponent<EnemyBase>();
            enemy.TakeDamage(curPower + Managers.Data.state_Power);


            if (deadTiem > 0.33f)
                Invoke("OnTargetReached", 0.33f);//이펙트 애니메이션 길이 만큼 기다렸다 회수
        }
    }
}
