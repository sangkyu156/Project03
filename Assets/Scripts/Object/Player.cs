using Redcode.Pools;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 스킬변수
    public int fireBallLevel = 0;           //파이어볼
    public float fireBallCooldown = 0;
    public int tornadoLevel = 0;            //토네이도
    public float tornadoCooldown = 0;
    public int blackholeLevel = 0;          //블랙홀
    public float blackholeCooldown = 0;
    public int sawBladeLevel = 0;           //톱날
    public int sparkLevel = 0;              //스파크(연사)
    public float sparkCooldown = 0;
    public int waveEnergyLevel = 0;         //에너지볼(관통)
    public float waveEnergyCooldown = 0;
    public int volcanoLevel = 0;            //볼케이노
    public float volcanoCooldown = 0;
    public bool volcano = false;
    public int tridentLevel = 0;            //삼지창
    public float tridentCooldown = 0;
    public bool trident = false;
    public int quicknessLevel = 0;          //신속
    public int slowdownLevel = 0;           //감속
    public int regenerateLevel = 0;         //체력회복
    public bool regenerate = false;
    public float regenerateCooldown = 0;
    public GameObject rageExplosionSkill;   //분노폭발
    public int rageExplosionLevel = 0;
    public float rageExplosionTime = 0;
    public bool rageExplosion = false;
    public int bulkingUpLevel = 0;          //벌크업
    public int goldChestLevel = 0;          //금화상자
    public int potionChestLevel = 0;        //포션상자
    public int regularLevel = 0;            //단골
    public GameObject[] sawBlade;
    #endregion

    public GameObject skillPos;//발사스킬 시작지점
    public int attackSkillCount = 0;
    public int maxHealth = 10;
    public int currentHealth;
    public float moveSpeed = 7;
    public Transform textPostion;
    float dist = 0f;

    public PlayerHealthBar healthBar;
    Vector2 vector2;
    GameObject stageCamera;
    Animator animator;
    SpriteRenderer spriteRenderer;
    PoolManager poolManager; //오브젝트 풀링 매니져
    new Rigidbody2D rigidbody2D;

    public enum Eirection
    {
        Up, Down, Left, Right
    }
    Eirection eirection = Eirection.Down;

    private static Player instance = null;
    public static Player Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        poolManager = GetComponent<PoolManager>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        //이동
        #region 이동
        vector2.x = Input.GetAxisRaw("Horizontal");
        vector2.y = Input.GetAxisRaw("Vertical");

        rigidbody2D.velocity = vector2.normalized * moveSpeed;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            eirection = Eirection.Up;
            animator.SetBool("U_Walk", true);
            animator.SetBool("D_Walk", false);
            animator.SetBool("L_Walk", false);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            eirection = Eirection.Down;
            animator.SetBool("U_Walk", false);
            animator.SetBool("D_Walk", true);
            animator.SetBool("L_Walk", false);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            eirection = Eirection.Right;
            spriteRenderer.flipX = true;
            animator.SetBool("U_Walk", false);
            animator.SetBool("D_Walk", false);
            animator.SetBool("L_Walk", true);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            eirection = Eirection.Left;
            spriteRenderer.flipX = false;
            animator.SetBool("U_Walk", false);
            animator.SetBool("D_Walk", false);
            animator.SetBool("L_Walk", true);
        }

        // 멈춰있을때
        if (rigidbody2D.velocity.x == 0 && rigidbody2D.velocity.y == 0)
        {
            animator.SetBool("U_Walk", false);
            animator.SetBool("D_Walk", false);
            animator.SetBool("L_Walk", false);
            switch (eirection)
            {
                case Eirection.Up:
                    animator.SetBool("U_Idle", true);
                    animator.SetBool("D_Idle", false);
                    animator.SetBool("L_Idle", false);
                    break;
                case Eirection.Down:
                    animator.SetBool("U_Idle", false);
                    animator.SetBool("D_Idle", true);
                    animator.SetBool("L_Idle", false);
                    break;
                case Eirection.Left:
                case Eirection.Right:
                    animator.SetBool("U_Idle", false);
                    animator.SetBool("D_Idle", false);
                    animator.SetBool("L_Idle", true);
                    break;
            }
        }
        else
        {
            animator.SetBool("U_Idle", false);
            animator.SetBool("D_Idle", false);
            animator.SetBool("L_Idle", false);
        }
        #endregion

        if (stageCamera != null)
        {
            dist = Vector2.Distance(stageCamera.transform.position, transform.position);

            if (dist > 19) //왼쪽으로 더이상 못가게 막음
            {
                float posY = transform.position.y;
                transform.position = stageCamera.transform.position + new Vector3(-16.2f, posY, +10);//GuideTextCanvas

                //가이드Text 출력
                GameObject guideText = Managers.Resource.Instantiate("UI/Text/GuideTextCanvas");
                guideText.transform.SetParent(textPostion, false);
            }
        }

        //y값이 일정범위 넘어갔을때 다시 초기화
        if (transform.position.y > -3.5f || transform.position.y < -11)
        {
            transform.position = new Vector3(transform.position.x, -7);
        }
    }

    public void SetCamera()
    {
        stageCamera = GameObject.FindGameObjectWithTag("Camera");
    }

    //파이어볼 발사 시작
    public void FireBallAction()
    {
        CancelInvoke("Spawn");
        InvokeRepeating("Spawn", 0, fireBallCooldown);
    }

    //토네이도 발사 시작
    public void TornadoAction()
    {
        CancelInvoke("Spawn2");
        InvokeRepeating("Spawn2", 0, tornadoCooldown);
    }

    //블랙홀 발사 시작
    public void BlackHoleAction()
    {
        CancelInvoke("Spawn3");
        InvokeRepeating("Spawn3", 0, blackholeCooldown);
    }

    //볼케이노 발사 시작
    public void VolcanoAction()
    {
        volcano = true;
    }

    //삼지창 발사 시작
    public void TridentAction()
    {
        trident = true;
    }

    //톱니 추가
    public void SawBladeAdd()
    {
        switch (sawBladeLevel)
        {
            case 1:
                sawBlade[0].SetActive(true); break;
            case 2:
                sawBlade[1].SetActive(true); break;
            case 3:
                sawBlade[2].SetActive(true); break;
            case 4:
                sawBlade[3].SetActive(true); break;
            case 5:
                sawBlade[4].SetActive(true); break;
            case 6:
                sawBlade[5].SetActive(true); break;
            case 7:
                sawBlade[6].SetActive(true);
                sawBlade[7].SetActive(true); break;
        }
    }

    //스파크 발사 시작
    public void SparkAction()
    {
        CancelInvoke("Spawn4");
        InvokeRepeating("Spawn4", 0, sparkCooldown);
    }

    //에너지볼 발사 시작
    public void WaveEnergyAction()
    {
        CancelInvoke("Spawn5");
        InvokeRepeating("Spawn5", 0, waveEnergyCooldown);
    }

    //분노 폭발 시작
    public void RageExplosionAction()
    {
        rageExplosion = true;
    }

    //오브젝트풀링 생성
    void Spawn()
    {
        //GameManager.Instance.SFXPlay(Sfx.FireBall);
        FireBall_Skill fireBall_Skill = poolManager.GetFromPool<FireBall_Skill>();
        fireBall_Skill.gameObject.transform.position = skillPos.transform.position;
    }
    void Spawn2()
    {
        //GameManager.Instance.SFXPlay(Sfx.Tornado);
        //Tornado_Skill tornado_Skill = poolManager.GetFromPool<Tornado_Skill>();
    }
    void Spawn3()
    {
        //GameManager.Instance.SFXPlay(Sfx.BlackHole);
        BlackHole_Skill blackHole_Skill = poolManager.GetFromPool<BlackHole_Skill>();
        blackHole_Skill.gameObject.transform.position = skillPos.transform.position;
    }
    void Spawn4()
    {
        //GameManager.Instance.SFXPlay(Sfx.Spark);
        //Spark_Skill spark_Skill = poolManager.GetFromPool<Spark_Skill>();
    }
    void Spawn5()
    {
        //GameManager.Instance.SFXPlay(Sfx.EnergyBall);
        //WaveEnergy_Skill waveEnergy_Skill = poolManager.GetFromPool<WaveEnergy_Skill>();
    }
    void Spawn6()
    {
        //GameManager.Instance.SFXPlay(Sfx.Volcano);
        //Volcano_Skill volcano_Skill = poolManager.GetFromPool<Volcano_Skill>();
    }
    void Spawn7()
    {
        //GameManager.Instance.SFXPlay(Sfx.Trident);
        //Trident_Skill volcano_Skill = poolManager.GetFromPool<Trident_Skill>();
    }

    //오브젝트 회수
    public void ReturnPool(FireBall_Skill clone)
    {
        poolManager.TakeToPool<FireBall_Skill>(clone.idName, clone);
    }
    //public void ReturnPool(Tornado_Skill clone)
    //{
    //    poolManager.TakeToPool<Tornado_Skill>(clone.idName, clone);
    //}
    public void ReturnPool(BlackHole_Skill clone)
    {
        poolManager.TakeToPool<BlackHole_Skill>(clone.idName, clone);
    }
    //public void ReturnPool(Spark_Skill clone)
    //{
    //    poolManager.TakeToPool<Spark_Skill>(clone.idName, clone);
    //}
    //public void ReturnPool(WaveEnergy_Skill clone)
    //{
    //    poolManager.TakeToPool<WaveEnergy_Skill>(clone.idName, clone);
    //}
    //public void ReturnPool(Volcano_Skill clone)
    //{
    //    poolManager.TakeToPool<Volcano_Skill>(clone.idName, clone);
    //}
    //public void ReturnPool(Trident_Skill clone)
    //{
    //    poolManager.TakeToPool<Trident_Skill>(clone.idName, clone);
    //}

    //데미지 받았을때
    public void TakeDamage(int damage_, bool direction)//매개변수 bool값 으로 오른쪽으로 밀려날지 왼쪽으로 밀려날지 정해야함
    {
        currentHealth -= damage_;
        healthBar.SetHealth(currentHealth);

        //데미지 출력
        GameObject damageUI = Managers.Resource.Instantiate("UI/Text/DamageTextCanvas");
        damageUI.GetComponentInChildren<DamageText>().damage = damage_;
        damageUI.transform.SetParent(textPostion, false);
        //GameManager.Instance.SFXPlay(Sfx.PlayerHit);

        gameObject.tag = "NoDamage";
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("effect1", 0.2f);
        Invoke("effect2", 0.3f);
        Invoke("effect1", 0.4f);
        Invoke("effect2", 0.5f);
        Invoke("effect1", 0.6f);
        Invoke("effect2", 0.7f);
        Invoke("effect1", 0.8f);
        Invoke("effect2", 0.9f);


        if (direction)//오른쪽에서 데미지를 받음
            this.rigidbody2D.AddForce(new Vector2(-1, 0) * 5000);
        else
            this.rigidbody2D.AddForce(new Vector2(1, 0) * 5000);

        Invoke("OnDamage", 1f);
    }

    //무적 풀림
    public void OnDamage()
    {
        gameObject.tag = "Player";
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void effect1()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.6f);
    }

    void effect2()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);
    }
}
