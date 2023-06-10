using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntroWolf : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    Animator animator;
    float abcspeed = 5.5f;
    float time = 0;
    Vector3 moveDirectione;

    public GameObject head;
    public GameObject head2;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        abcspeed = 5.5f;
        time = 0;
        head.SetActive(true);
        head2.SetActive(false);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (0 <= time && time < 2)
            abcMove();
        else if (2 <= time && time < 3.2f)
            abcStop();
        else if (3.2f <= time && time < 5)
            abcDie();
    }

    void abcMove()
    {
        moveDirectione = new Vector3(-1, 0);
        rigidbody.velocity = moveDirectione * abcspeed;
    }

    void abcStop()
    {
        rigidbody.velocity = moveDirectione * 0;
        animator.SetBool("Idle", true);
    }

    void abcDie()
    {
        head.SetActive(false);
        head2.SetActive(true);
        animator.SetBool("Idle", false);
        animator.SetBool("Death", true);
    }
}
