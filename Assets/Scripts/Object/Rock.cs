using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    float deadLine = 30f;
    private void Awake()
    {
        deadLine = 30f;
    }

    private void Update()
    {
        deadLine -= Time.deltaTime;
        if (deadLine < 0)
            Destroy(gameObject);
    }
}
