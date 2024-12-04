using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{

    [SerializeField] private ParticleSystem testParticleSystem = default;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            testParticleSystem.Play();
        }
    }
}
