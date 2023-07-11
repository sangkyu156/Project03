using Redcode.Pools;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region ��ų����
    public int fireBallLevel = 0;           //���̾
    public float fireBallCooldown = 0;
    public int tornadoLevel = 0;            //����̵�
    public float tornadoCooldown = 0;
    public int blackholeLevel = 0;          //��Ȧ
    public float blackholeCooldown = 0;
    public int sawBladeLevel = 0;           //�鳯
    public int sparkLevel = 0;              //����ũ(����)
    public float sparkCooldown = 0;
    public int waveEnergyLevel = 0;         //��������(����)
    public float waveEnergyCooldown = 0;
    public int volcanoLevel = 0;            //�����̳�
    public float volcanoCooldown = 0;
    public bool volcano = false;
    public int tridentLevel = 0;            //����â
    public float tridentCooldown = 0;
    public bool trident = false;
    public int quicknessLevel = 0;          //�ż�
    public int slowdownLevel = 0;           //����
    public int regenerateLevel = 0;         //ü��ȸ��
    public bool regenerate = false;
    public float regenerateCooldown = 0;
    public int rageExplosionLevel = 0;      //�г�����
    public float rageExplosionTime = 0;
    public bool rageExplosion = false;
    public int bulkingUpLevel = 0;          //��ũ��
    public int goldChestLevel = 0;          //��ȭ����
    public int potionChestLevel = 0;        //���ǻ���
    public int regularLevel = 0;            //�ܰ�
    public bool clairvoyant = false;             //õ����
    public GameObject rageExplosionSkill;
    public GameObject[] sawBlade;
    #endregion

    public GameObject skillPos;//�߻罺ų ��������
    public int attackSkillCount = 0;
    public int maxHealth = 10;
    public int currentHealth;
    public float moveSpeed = 7;
    public Transform textPostion;
    public Transform volcanoPos;
    public Transform tridentPos;
    public bool firstStore = false;
    float dist = 0f;
    bool isCoin = false;

    public GameObject effect_Heal;
    public PlayerHealthBar healthBar;
    Vector2 vector2;
    GameObject healUI;
    GameObject stageCamera;
    Animator animator;
    SpriteRenderer spriteRenderer;
    GameObject canvas;
    PoolManager poolManager; //������Ʈ Ǯ�� �Ŵ���
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
        canvas = GameObject.FindGameObjectWithTag("StageCanvas");
        SetPlayerStats();
    }

    private void Update()
    {
        //����
        if (currentHealth <= 0)
        {
            GameObject deadPopup = Managers.Resource.Instantiate("UI/Popup/DeadPopup");
            deadPopup.transform.SetParent(canvas.transform, false);
        }

        Collider2D[] coin = Physics2D.OverlapBoxAll(transform.position, new Vector2(3, 10), 0);
        for (int i = 0; i < coin.Length; i++)
        {
            if (coin[i].tag == "Coin")
            {
                coin[i].transform.position = Vector2.MoveTowards(coin[i].transform.position, this.transform.position + new Vector3(0, 1.5f, 0), 13 * Time.deltaTime);
            }
        }

        //�г����� ��ų ȹ���
        if (rageExplosion)
        {
            rageExplosionTime -= Time.deltaTime;
            if (rageExplosionTime < 0)
            {
                rageExplosionSkill.SetActive(true);
                Managers.Sound.Play("RageExplosion");
                Invoke("RageExplosionOff", 0.7f);
                rageExplosionTime = rageExplosionSkill.GetComponent<RageExplosion_Skill>().rageExplosionCooldown;
            }
        }

        //ü��ȸ�� ��ų ȹ���
        if (regenerate)
        {
            regenerateCooldown -= Time.deltaTime;
            if (regenerateCooldown < 0)
                Heal();
        }

        //����â ��ų ȹ���
        if (trident)
        {
            tridentCooldown -= Time.deltaTime;
            if (tridentCooldown < 0)
            {
                Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, new Vector2(12, 5), 0);

                for (int i = 0; i < targets.Length; i++)
                {
                    if (targets[i].tag == "Enemy" && targets[i].GetComponent<EnemyBase>().currentHealth > 0)
                    {
                        if (Mathf.Abs(transform.position.x - targets[i].transform.position.x) < 13)
                        {
                            tridentPos = targets[i].transform;
                            Spawn7();
                        }

                        switch (tridentLevel)
                        {
                            case 1:
                                tridentCooldown = 0.9f; break;
                            case 2:
                                tridentCooldown = 0.9f; break;
                            case 3:
                                tridentCooldown = 0.8f; break;
                            case 4:
                                tridentCooldown = 0.8f; break;
                            case 5:
                                tridentCooldown = 0.7f; break;
                            case 6:
                                tridentCooldown = 0.7f; break;
                            case 7:
                                tridentCooldown = 0.5f; break;
                        }

                        return;
                    }
                }
            }
        }

        //�����̳� ��ų ȹ���
        if (volcano)
        {
            volcanoCooldown -= Time.deltaTime;
            if (volcanoCooldown < 0)
            {
                Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, new Vector2(10, 5), 0);

                for (int i = 0; i < targets.Length; i++)
                {
                    if (targets[i].tag == "Enemy" && targets[i].GetComponent<EnemyBase>().currentHealth > 0)
                    {
                        if (Mathf.Abs(transform.position.x - targets[i].transform.position.x) < 13)
                        {
                            volcanoPos = targets[i].transform;
                            Spawn6();
                        }

                        switch (volcanoLevel)
                        {
                            case 1:
                                volcanoCooldown = 2f; break;
                            case 2:
                                volcanoCooldown = 2f; break;
                            case 3:
                                volcanoCooldown = 1.9f; break;
                            case 4:
                                volcanoCooldown = 1.9f; break;
                            case 5:
                                volcanoCooldown = 1.8f; break;
                            case 6:
                                volcanoCooldown = 1.7f; break;
                            case 7:
                                volcanoCooldown = 1.6f; break;
                        }

                        return;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //�̵�
        #region �̵�
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

        // ����������
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

            if (dist > 19) //�������� ���̻� ������ ����
            {
                float posY = transform.position.y;
                transform.position = stageCamera.transform.position + new Vector3(-16.2f, posY, +10);//GuideTextCanvas

                //���̵�Text ���
                GameObject guideText = Managers.Resource.Instantiate("UI/Text/GuideTextCanvas");
                guideText.transform.SetParent(textPostion, false);
            }
        }

        //y���� �������� �Ѿ���� �ٽ� �ʱ�ȭ
        if (transform.position.y > -3.5f || transform.position.y < -11)
        {
            transform.position = new Vector3(transform.position.x, -7);
        }
    }

    public void SetCamera()
    {
        stageCamera = GameObject.FindGameObjectWithTag("Camera");
    }

    //���̾ �߻� ����
    public void FireBallAction()
    {
        CancelInvoke("Spawn");
        InvokeRepeating("Spawn", 0, fireBallCooldown);
    }

    //����̵� �߻� ����
    public void TornadoAction()
    {
        CancelInvoke("Spawn2");
        InvokeRepeating("Spawn2", 0, tornadoCooldown);
    }

    //��Ȧ �߻� ����
    public void BlackHoleAction()
    {
        CancelInvoke("Spawn3");
        InvokeRepeating("Spawn3", 0, blackholeCooldown);
    }

    //�����̳� �߻� ����
    public void VolcanoAction()
    {
        volcano = true;
    }

    //����â �߻� ����
    public void TridentAction()
    {
        trident = true;
    }

    void RageExplosionOff()
    {
        rageExplosionSkill.SetActive(false);
    }

    //��� �߰�
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

    //����ũ �߻� ����
    public void SparkAction()
    {
        CancelInvoke("Spawn4");
        InvokeRepeating("Spawn4", 0, sparkCooldown);
    }

    //�������� �߻� ����
    public void WaveEnergyAction()
    {
        CancelInvoke("Spawn5");
        InvokeRepeating("Spawn5", 0, waveEnergyCooldown);
    }

    //�г� ���� ����
    public void RageExplosionAction()
    {
        rageExplosion = true;
    }

    //������ƮǮ�� ����
    void Spawn()
    {
        Managers.Sound.Play("FireBall");
        FireBall_Skill fireBall_Skill = poolManager.GetFromPool<FireBall_Skill>();
        fireBall_Skill.gameObject.transform.position = skillPos.transform.position;
    }
    void Spawn2()
    {
        Managers.Sound.Play("Tornado");
        Tornado_Skill tornado_Skill = poolManager.GetFromPool<Tornado_Skill>();
        tornado_Skill.gameObject.transform.position = skillPos.transform.position;
    }
    void Spawn3()
    {
        Managers.Sound.Play("BlackHole");
        BlackHole_Skill blackHole_Skill = poolManager.GetFromPool<BlackHole_Skill>();
        blackHole_Skill.gameObject.transform.position = skillPos.transform.position;
    }
    void Spawn4()
    {
        Managers.Sound.Play("Spark");
        Spark_Skill spark_Skill = poolManager.GetFromPool<Spark_Skill>();
        spark_Skill.gameObject.transform.position = skillPos.transform.position;
    }
    void Spawn5()
    {
        Managers.Sound.Play("EnergyBall");
        WaveEnergy_Skill waveEnergy_Skill = poolManager.GetFromPool<WaveEnergy_Skill>();
        waveEnergy_Skill.gameObject.transform.position = skillPos.transform.position;
    }
    void Spawn6()
    {
        Managers.Sound.Play("Volcano");
        Volcano_Skill volcano_Skill = poolManager.GetFromPool<Volcano_Skill>();
    }
    void Spawn7()
    {
        Managers.Sound.Play("Trident");
        Trident_Skill trident_Skill = poolManager.GetFromPool<Trident_Skill>();
    }

    //������Ʈ ȸ��
    public void ReturnPool(FireBall_Skill clone)
    {
        poolManager.TakeToPool<FireBall_Skill>(clone.idName, clone);
    }
    public void ReturnPool(Tornado_Skill clone)
    {
        poolManager.TakeToPool<Tornado_Skill>(clone.idName, clone);
    }
    public void ReturnPool(BlackHole_Skill clone)
    {
        poolManager.TakeToPool<BlackHole_Skill>(clone.idName, clone);
    }
    public void ReturnPool(Spark_Skill clone)
    {
        poolManager.TakeToPool<Spark_Skill>(clone.idName, clone);
    }
    public void ReturnPool(WaveEnergy_Skill clone)
    {
        poolManager.TakeToPool<WaveEnergy_Skill>(clone.idName, clone);
    }
    public void ReturnPool(Volcano_Skill clone)
    {
        poolManager.TakeToPool<Volcano_Skill>(clone.idName, clone);
    }
    public void ReturnPool(Trident_Skill clone)
    {
        poolManager.TakeToPool<Trident_Skill>(clone.idName, clone);
    }

    //������ �޾�����
    public void TakeDamage(int damage_, bool direction)//�Ű����� bool�� ���� ���������� �з����� �������� �з����� ���ؾ���
    {
        currentHealth -= damage_;
        healthBar.SetHealth(currentHealth);

        //������ ���
        GameObject damageUI = Managers.Resource.Instantiate("UI/Text/DamageTextCanvas");
        damageUI.GetComponentInChildren<DamageText>().damage = damage_;
        damageUI.transform.SetParent(textPostion, false);
        Managers.Sound.Play("PlayerHit");

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


        if (direction)//�����ʿ��� �������� ����
            this.rigidbody2D.AddForce(new Vector2(-1, 0) * 5000);
        else
            this.rigidbody2D.AddForce(new Vector2(1, 0) * 5000);

        Invoke("OnDamage", 1f);
    }

    //���� Ǯ��
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

    //���� �Ծ�����
    public void GetHP_Potion(Define.Potion potion_)
    {
        effect_Heal.SetActive(true);
        Managers.Sound.Play("Heal");

        currentHealth += ((int)potion_ + Managers.Data.state_PotionRecover);

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        HealPrint(potion_);

        healthBar.SetHealth(currentHealth);

        Invoke("HealOff", 0.7f);
    }

    //��Text ���
    void HealPrint(Define.Potion potion_)
    {
        if (healUI == null)
            healUI = Managers.Resource.Instantiate("UI/Text/HealTextCanvas");

        healUI.transform.SetParent(textPostion, false);
        healUI.GetComponentInChildren<HealText>().heal = $"+{((int)potion_ + Managers.Data.state_PotionRecover)}";
    }

    void HealPrint(int skillLevel)
    {
        if (healUI == null)
            healUI = Managers.Resource.Instantiate("UI/Text/HealTextCanvas");

        healUI.transform.SetParent(textPostion, false);
        healUI.GetComponentInChildren<HealText>().heal = $"+{skillLevel + 2}";
    }

    void HealOff()
    {
        effect_Heal.SetActive(false);
    }

    //ü��ȸ�� ��ų
    void Heal()
    {
        regenerateCooldown = 10;
        effect_Heal.SetActive(true);

        Managers.Sound.Play("Heal");

        currentHealth += regenerateLevel + 2;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        HealPrint(regenerateLevel);

        healthBar.SetHealth(currentHealth);

        Invoke("HealOff", 0.7f);
    }

    void SetPlayerStats()
    {
        Managers.Data.countBG = 0;
        maxHealth += Managers.Data.state_Health;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Managers.fieldMoney = Managers.Data.state_StartGold + 500;
        Managers.Instance.PrintFieldMoney();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
            StartCoroutine("CoinSound");
    }

    IEnumerator CoinSound()
    {
        if (isCoin)
            yield break;

        isCoin = true;
        Managers.Sound.Play("Coin");

        yield return new WaitForSeconds(0.08f);

        isCoin = false;
    }
}
