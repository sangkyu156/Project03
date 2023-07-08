using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Boss : MonoBehaviour
{
    GameObject canvas;
    Vector3 moveDirectione;
    new Rigidbody2D rigidbody;
    Animator animator;
    public float bossSpeed = 7;
    bool attack = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bossSpeed = 7;
    }

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("StageCanvas");
    }

    void Update()
    {
        if (attack == false)
            BossMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "NoDamage")
        {
            animator.SetBool("Attack", true);
            Invoke("BossAttackOff", 0.6f);
        }
    }

    void BossMove()
    {
        moveDirectione = new Vector3(1, 0);
        rigidbody.velocity = moveDirectione * bossSpeed;
    }

    void BossAttackOff()
    {
        attack = false;
        animator.SetBool("Attack", false);

        GameObject deadPopup = Managers.Resource.Instantiate("UI/Popup/DeadPopup");
        deadPopup.transform.SetParent(canvas.transform, false);
    }
}
