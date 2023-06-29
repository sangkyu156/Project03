using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box2 : MonoBehaviour
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
        if (hitCount >= 3)
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

        if (isDrop == false)
        {
            isDrop = true;
            Drop();
        }

        Destroy(gameObject, 0.4f);
    }

    void Drop()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                GameObject pottion = Instantiate(Resources.Load<GameObject>("Field/HP_Potion")) as GameObject;
                float posX = Random.Range(-0.5f, 0.3f);
                float posY = Random.Range(-0.5f, 0.3f);
                pottion.transform.position = new Vector2(transform.position.x + posX, transform.position.y + posY);
                break;
            case 1:
                for (int i = 0; i < 2; i++)
                {
                    GameObject pottion2 = Instantiate(Resources.Load<GameObject>("Field/HP_Potion")) as GameObject;
                    float posX2 = Random.Range(-0.5f, 0.3f);
                    float posY2 = Random.Range(-0.5f, 0.3f);
                    pottion2.transform.position = new Vector2(transform.position.x + posX2, transform.position.y + posY2);
                }
                break;
        }
    }
}
