using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageExplosion_Skill : MonoBehaviour
{
    protected int curPower;
    protected int nextPower;
    public float rageExplosionCooldown = 0;
    protected BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        SetAbility();

        if (boxCollider != null)
            boxCollider.enabled = true;
    }

    //¿¡³Ê¹Ì¶û ºÎµúÃÆÀ» ¶§
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

    public void SetAbility()
    {
        switch (Player.Instance.rageExplosionLevel)
        {
            case 0:
                curPower = 0;
                nextPower = 0;
                rageExplosionCooldown = 5f;
                break;
            case 1:
                curPower = 15;
                nextPower = 19;
                rageExplosionCooldown = 1.3f;
                Player.Instance.rageExplosionSkill.transform.localScale = new Vector3(1,1,1);
                Player.Instance.rageExplosionSkill.transform.localPosition = new Vector3(0, 1, 0);
                break;
            case 2:
                curPower = 19;
                nextPower = 23;
                rageExplosionCooldown = 1.2f;
                Player.Instance.rageExplosionSkill.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                Player.Instance.rageExplosionSkill.transform.localPosition = new Vector3(0, 1.1f, 0);
                break;
            case 3:
                curPower = 23;
                nextPower = 27;
                Player.Instance.rageExplosionSkill.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                Player.Instance.rageExplosionSkill.transform.localPosition = new Vector3(0, 1.2f, 0);
                rageExplosionCooldown = 1.1f;
                break;  
            case 4:
                curPower = 27;
                nextPower = 31;
                Player.Instance.rageExplosionSkill.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                Player.Instance.rageExplosionSkill.transform.localPosition = new Vector3(0, 1.3f, 0);
                rageExplosionCooldown = 1.0f;
                break;
            case 5:
                curPower = 31;
                nextPower = 35;
                Player.Instance.rageExplosionSkill.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                Player.Instance.rageExplosionSkill.transform.localPosition = new Vector3(0, 1.4f, 0);
                rageExplosionCooldown = 0.9f;
                break;
            case 6:
                curPower = 35;
                nextPower = 40;
                Player.Instance.rageExplosionSkill.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Player.Instance.rageExplosionSkill.transform.localPosition = new Vector3(0, 1.5f, 0);
                rageExplosionCooldown = 0.8f;
                break;
            case 7:
                curPower = 40;
                nextPower = 0;
                Player.Instance.rageExplosionSkill.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
                Player.Instance.rageExplosionSkill.transform.localPosition = new Vector3(0, 1.7f, 0);
                rageExplosionCooldown = 0.71f;
                break;
        }

        Invoke("ColliderOff", 0.2f);
    }

    void ColliderOff()
    {
        boxCollider.enabled = false;
    }
}
