using Redcode.Pools;
using UnityEngine;

public class BlackHole_Skill : ProjectileSkill, IPoolObject
{
    float blackHoleSpeed = 0;
    public string idName;

    new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        SetAbility();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        blackHoleSpeed = 10;
        deadTiem = 1.2f;
        idName = "BlackHole_Skill";
    }

    void Update()
    {
        if (Player.Instance.blackholeLevel <= 0)
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
            deadTiem = 1.2f;
            OnTargetReached();
        }
    }

    //에너미랑 부딪쳤을 때
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            Attack(collision);
    }

    void Attack(Collider2D collider_)
    {
        EnemyBase enemy;
        enemy = collider_.GetComponent<EnemyBase>();

        collider_.transform.position = new Vector3(collider_.transform.position.x + 1f, collider_.transform.position.y);

        enemy.TakeDamage(curPower + (int)(Managers.Data.state_Power * 0.3f));
    }

    public override void Shoot()
    {
        rigidbody2D.velocity = new Vector3(blackHoleSpeed, 0, 0);
    }

    //처음생성됬을때 실행되는 메소드
    public void OnCreatedInPool()
    {
        SetAbility();
    }

    //재사용되서 다시한번 실행될때마다 실행되는 메소드
    public void OnGettingFromPool()
    {
        deadTiem = 1.2f;
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
        switch (Player.Instance.blackholeLevel)
        {
            case 0:
                curPower = 0;
                nextPower = 0;
                nextCooldown = 0;
                break;
            case 1:
                curPower = 0;
                nextPower = 1;
                nextCooldown = 2f;
                break;
            case 2:
                curPower = 1;
                nextPower = 2;
                nextCooldown = 1.9f;
                break;
            case 3:
                curPower = 2;
                nextPower = 3;
                nextCooldown = 1.9f;
                break;
            case 4:
                curPower = 3;
                nextPower = 4;
                nextCooldown = 1.8f;
                break;
            case 5:
                curPower = 4;
                nextPower = 5;
                nextCooldown = 1.7f;
                break;
            case 6:
                curPower = 5;
                nextPower = 6;
                nextCooldown = 1.6f;
                break;
            case 7:
                curPower = 6;
                nextPower = 0;
                nextCooldown = 0f;
                break;
        }
    }
}
