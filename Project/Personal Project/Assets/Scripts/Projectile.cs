using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;

    [SerializeField] private float maxDistance = 100f;
    private float distance = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ProjectileCollision>().onCollision += () => { speed = 0f; };
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        
        distance += speed * Time.deltaTime;
        if (distance >= maxDistance) 
            Destroy(gameObject);
    }


}
