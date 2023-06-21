using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticaleAnimator : MonoBehaviour
{
    double lastTime;
    ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        lastTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        float deltaTime = Time.realtimeSinceStartup - (float)lastTime;

        particle.Simulate(deltaTime, true, false);

        lastTime = Time.realtimeSinceStartup;
    }
}
