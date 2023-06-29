using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Skill : ProjectileSkill, IPoolObject
{
    float fireballSpeed = 0;
    public string idName;

    Animator animator;
    BoxCollider2D skillCollider;
    new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        SetAbility();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        skillCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        fireballSpeed = 17;
        deadTiem = 1.4f;
        idName = "FireBall_Skill";
    }

    void Update()
    {
        if (Player.Instance.fireBallLevel <= 0)
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
            deadTiem = 1.4f;
            OnTargetReached();
        }
    }

    //에너미랑 부딪쳤을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            animator.SetBool("hit", true);
            Attack();

            if (deadTiem > 0.43f)
                Invoke("OnTargetReached", 0.43f);//이펙트 애니메이션 길이 만큼 기다렸다 회수
        }
    }

    void Attack()
    {
        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, new Vector2(7, 5), 0);
        skillCollider.enabled = false;
        EnemyBase enemy;
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].tag == "Enemy")
            {
                enemy = targets[i].GetComponent<EnemyBase>();
                enemy.TakeDamage(curPower + Managers.Data.state_Power);
            }
        }
    }

    public void OnCreatedInPool()
    {
        SetAbility();
    }

    public void OnGettingFromPool()
    {
        deadTiem = 1.4f;
        skillCollider.enabled = true;
        animator.SetBool("hit", false);
        SetAbility();
    }

    public override void OnTargetReached()
    {
        if (this.gameObject.activeSelf)
            Player.Instance.ReturnPool(this);
    }

    public override void SetAbility()
    {
        switch (Player.Instance.fireBallLevel)
        {
            case 0:
                curPower = 0;
                nextPower = 0;
                nextCooldown = 0;
                break;
            case 1:
                curPower = 15;
                nextPower = 18;
                nextCooldown = 1;
                break;
            case 2:
                curPower = 18;
                nextPower = 21;
                nextCooldown = 0.9f;
                break;
            case 3:
                curPower = 21;
                nextPower = 24;
                nextCooldown = 0.9f;
                break;
            case 4:
                curPower = 24;
                nextPower = 27;
                nextCooldown = 0.8f;
                break;
            case 5:
                curPower = 27;
                nextPower = 30;
                nextCooldown = 0.8f;
                break;
            case 6:
                curPower = 30;
                nextPower = 35;
                nextCooldown = 0.7f;
                break;
            case 7:
                curPower = 35;
                nextPower = 0;
                nextCooldown = 0f;
                break;
        }
    }

    public override void Shoot()
    {
        if (animator.GetBool("hit") == false)
            rigidbody2D.velocity = new Vector3(fireballSpeed, 0, 0);
        else if (animator.GetBool("hit") == true)
            rigidbody2D.velocity = Vector3.zero;
    }
}
