using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPlayer : MonoBehaviour
{
    public GameObject leftWalk;
    public GameObject rightWalk;
    public GameObject sowrd;
    public GameObject idle;
    public GameObject exclamationMark;
    public GameObject exclamationMark2;
    public GameObject exclamationMark3;

    void Start()
    {
        Invoke("SowrdStart", 2f);
        Invoke("Surprise", 6.7f);
        Invoke("Surprise2", 6.5f);
        Invoke("Off", 7.7f);
    }

    void SowrdStart()
    {
        leftWalk.SetActive(false);
        sowrd.SetActive(true);
        Invoke("IdleStart", 1.7f);
    }

    void IdleStart()
    {
        sowrd.SetActive(false);
        idle.SetActive(true);
    }

    void Surprise()
    {
        exclamationMark.SetActive(true);
        exclamationMark3.SetActive(true);
    }

    void Surprise2()
    {
        exclamationMark2.SetActive(true);
    }

    void Off()
    {
        exclamationMark.SetActive(false);
        exclamationMark2.SetActive(false);
        exclamationMark3.SetActive(false);

        leftWalk.SetActive(false);
        idle.SetActive(false);
        rightWalk.SetActive(true);
        rightWalk.gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }
}
