using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeEmitter;
    [SerializeField] private ParticleSystem flareEmitter;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject model;

    private void Awake()
    {
        smokeEmitter.Stop();
        flareEmitter.Stop();
    }
    private void Start()
    {
        GetComponent<ProjectileCollision>().onCollision += Explode;
    }

    public void Explode()
    {
        audioSource.Play();

        smokeEmitter.time = 0;
        flareEmitter.time = 0;
        smokeEmitter.Play();
        flareEmitter.Play();

        model.SetActive(false);
        Invoke("Destroy", 3f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }


}
