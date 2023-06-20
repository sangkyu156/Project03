using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "NoDamage")
        {
            //상점 오픈해야함
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }
}
