using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    GameObject player;

    public void FixedUpdate()
    {
        if(player != null)
        {
            Vector3 dir = player.transform.position - this.transform.position;
            if (dir.x > 0)
            {
                Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.fixedDeltaTime, 0, 0.0f);
                this.transform.Translate(moveVector);
            }
        }
    }

    //플레이어 찾는 함수
    public void SetPlayer()
    {
        Debug.Log("실행됨");
        player = GameObject.FindGameObjectWithTag("Player");
    }
}