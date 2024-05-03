using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public event Action onCollision;

    private void OnTriggerEnter(Collider other)
    {
        onCollision.Invoke();
    }
}
