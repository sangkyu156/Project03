using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntroBoss : MonoBehaviour
{
    public GameObject head;
    public GameObject head2;
    public GameObject exclamationMark;
    public GameObject exclamationMark2;
    Animator animator;
    new Rigidbody2D rigidbody;
    float time = 0;
    float bossSpeed = 6;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //GameManager.Instance.BGMPlay();
        time = 0;
        bossSpeed = 6;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (5.05f <= time && time < 5.2f)
            Exclamation();
        else if (5.2f <= time && time < 5.3f)
            BossWalk();
        else if (5.3f <= time)
            BossMove();
    }

    void Exclamation()
    {
        head.SetActive(false);
        head2.SetActive(true);
        exclamationMark.SetActive(true);
        Invoke("ExclamationOn", 0.1f);
    }

    void ExclamationOn()
    {
        exclamationMark2.SetActive(true);
    }

    void ExclamationOff()
    {
        exclamationMark.SetActive(false);
        exclamationMark2.SetActive(false);
    }

    void BossWalk()
    {
        animator.SetBool("Walk", true);
    }

    void BossMove()
    {
        rigidbody.velocity = new Vector3(1, 0) * bossSpeed;
        Invoke("ExclamationOff", 1f);
    }
}
