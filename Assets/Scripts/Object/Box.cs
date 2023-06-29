using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class Box : MonoBehaviour
{
    Animator animator;
    int hitCount = 0;
    bool isDrop = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        hitCount = 0;
        isDrop = false;
    }

    void Update()
    {
        if(hitCount >= 3)
        {
            Open();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Skill")
        {
            animator.SetBool("Hit", true);
            hitCount++;
            Invoke("OnIdle", 0.3f);
        }
    }

    void OnIdle()
    {
        animator.SetBool("Hit", false);
    }

    void Open()
    {
        animator.SetBool("Hit", false);
        animator.SetBool("Open", true);

        if(isDrop == false)
        {
            isDrop = true;
            Drop();
        }

        Destroy(gameObject, 0.4f);
    }

    void Drop()
    {
        if(Random.Range(0,4) == 3)
        {
            GameObject potion = Managers.Resource.Instantiate("Object/HP_Potion");
            float posX = Random.Range(-0.5f, 0.3f);
            float posY = Random.Range(-0.5f, 0.3f);
            potion.transform.position = new Vector2(transform.position.x + posX, transform.position.y + posY);
        }


        for (int i = 0; i < 3; i++)
        {
            if (Random.Range(0, 2) == 1)
            {
                GameObject coin = Managers.Resource.Instantiate("Object/Coin_1");
                float posX = Random.Range(-0.5f, 0.3f);
                float posY = Random.Range(-0.5f, 0.3f);
                coin.transform.position = new Vector2(transform.position.x + posX, transform.position.y + posY);
            }
        }
    }
}
