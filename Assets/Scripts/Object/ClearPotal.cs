using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPotal : MonoBehaviour
{
    [SerializeField]
    float rotSpeed = 100f;
    GameObject canvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "NoDamage")
        {
            GameObject clearPopup = Managers.Resource.Instantiate("UI/Popup/ClearPopup");
            clearPopup.transform.SetParent(canvas.transform, false);

            Destroy(gameObject);
        }
    }

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("StageCanvas");
    }

    private void Update()
    {
        //transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }
}
