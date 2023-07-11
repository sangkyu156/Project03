using Redcode.Pools;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IPoolObject
{
    protected bool drop = false;
    public int maxHealth;
    public int currentHealth;
    protected float speed;
    protected int power;
    public float knockbackTime = 1;
    public bool knockback = false;

    public GameObject Shadow;
    public HealthBar healthBar;
    public Transform textPostion;
    static public Vector3 deadPostion;

    new public CapsuleCollider2D collider;
    Transform target = null;
    protected Animator animator;

    private void Awake()
    {
        target = Player.Instance.transform;
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {

    }

    //�÷��̾� ���� �̵�
    protected void TargetConfirm()
    {
        if (target != null)
        {
            //Vector3 direction = transform.position - target.position;
            transform.position = Vector2.MoveTowards(transform.position, target.position + new Vector3(0, 1, 0), speed * Time.deltaTime);
        }
    }

    //����
    protected void PositionStop()
    {
        //GameManager.Instance.SFXPlay(GameManager.Sfx.EnemyDie);
        transform.position = Vector2.MoveTowards(transform.position, target.position, 0 * Time.deltaTime);
    }

    //������ �޾�����
    public void TakeDamage(int damage_)
    {
        currentHealth -= damage_;

        //������ ���
        GameObject damageUI = Managers.Resource.Instantiate("UI/Text/DamageTextCanvas");
        damageUI.GetComponentInChildren<DamageText>().damage = damage_;
        damageUI.transform.SetParent(textPostion, false);

        healthBar.SetHealth(currentHealth);
    }

    public void KnockbackSet()
    {
        Invoke("KnockbackReset", 0.5f);
    }

    void KnockbackReset()
    {
        knockback = false;
    }

    //����
    protected void Death()
    {
        Shadow.SetActive(false);//�׸��� x
        collider.enabled = false;//�ݶ��̴� x
        PositionStop();//�̵� x
        animator.SetBool("Death", true);//�״� �ִϸ��̼�
        deadPostion = transform.position;
        Managers.Data.killCount++;
        Invoke("OnTargetReached", 0.4f);//0.7�ʵ� ȸ��
    }

    //�÷��̾� ��ġ�� ���� ȸ��(�ִϸ��̼��� �ٲٴ� ���)
    protected void Rotation()
    {
        if(target ==  null)
            return;

        if (target.position.x < transform.position.x)
        {
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
        }
        else
        {
            animator.SetBool("Left", false);
            animator.SetBool("Right", true);
        }
    }

    //���׷� ���� y���� �������� �Ѿ���� �ٽ� �ʱ�ȭ
    protected void PositionReset()
    {
        if (transform.position.y > -3.5f || transform.position.y < -11)
        {
            transform.position = new Vector3(transform.position.x, -7);
        }
    }

    //�ѹ��� �и��� ���ؼ� ���
    protected void Knockback()
    {
        if (knockback == true)
        {
            knockbackTime -= Time.deltaTime;
            if (knockbackTime <= 0)
            {
                knockbackTime = 1;
                knockback = false;
            }
        }
    }

    protected virtual void SetAbility()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        drop = false;
        Shadow.SetActive(true);
    }

    public void OnCreatedInPool()
    {
        SetAbility();

        float ranPosX = Random.Range(30f, 50f);
        float ranPosY = Random.Range(0f, -4f);
        transform.position = Player.Instance.transform.position + new Vector3(ranPosX, ranPosY, 0);
    }

    public void OnGettingFromPool()
    {
        SetAbility();
        collider.enabled = true;
        Shadow.SetActive(true);
        knockback = false;

        float ranPosX = Random.Range(30f, 50f);
        float ranPosY = Random.Range(0f, -4f);
        transform.position = Player.Instance.transform.position + new Vector3(ranPosX, ranPosY, 0);
    }

    //�÷��̾� ����
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            bool Right = true;
            if (collision.transform.position.x < this.transform.position.x)
                Right = true;
            else
                Right = false;

            Player.Instance.TakeDamage(power, Right);
        }
    }

    //���
    protected void Drop()
    {
        CreateManager.Instance.CoinSpawn();
    }

    protected void Drop2()
    {
        CreateManager.Instance.Coin2Spawn();
    }
}
