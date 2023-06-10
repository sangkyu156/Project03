using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    public Transform target;
    float speed;
    Vector3 offSet;

    private void Start()
    {
        offSet = transform.position - target.position;
        speed = 220;
    }

    private void Update()
    {
        transform.position = target.position + offSet;
        transform.RotateAround(target.position, Vector3.back, speed * Time.deltaTime);

        offSet = transform.position - target.position;
    }
}
