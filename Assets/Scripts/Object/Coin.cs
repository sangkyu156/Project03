using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Coin : MonoBehaviour, IPoolObject
{
    public string idName;
    public int speed = 13;

    void Start()
    {
        speed = 13;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "NoDamage")
        {
            Managers.fieldMoney += Random.Range(2, 3);
            Managers.Instance.PrintFieldMoney();
            if (gameObject.activeSelf)
                CreateManager.Instance.ReturnPool(this);
        }
    }

    //처음생성됬을때 실행되는 메소드
    public void OnCreatedInPool()
    {
        float posX = Random.Range(-0.5f, 0.3f);
        float posY = Random.Range(-0.5f, 0.3f);
        transform.position = EnemyBase.deadPostion + new Vector3(posX, posY, 0);
    }

    //재사용되서 다시한번 실행될때마다 실행되는 메소드
    public void OnGettingFromPool()
    {
        float posX = Random.Range(-0.5f, 0.3f);
        float posY = Random.Range(-0.5f, 0.3f);
        transform.position = EnemyBase.deadPostion + new Vector3(posX, posY, 0);
    }
}
