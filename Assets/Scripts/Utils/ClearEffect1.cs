using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEffect1 : MonoBehaviour
{
    float rotSpeed = 10;

    void Start()
    {
        rotSpeed = 10;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -rotSpeed * Time.unscaledDeltaTime));
    }
}
