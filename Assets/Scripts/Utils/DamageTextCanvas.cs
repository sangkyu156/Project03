using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextCanvas : MonoBehaviour
{
    public float destoryTime = 1.5f;
    public float textUpSpeed = 0.5f;

    void Start()
    {
        Invoke("DestroyDamageText", destoryTime);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, textUpSpeed * Time.deltaTime));
    }

    void DestroyDamageText()
    {
        Destroy(gameObject);
    }
}
