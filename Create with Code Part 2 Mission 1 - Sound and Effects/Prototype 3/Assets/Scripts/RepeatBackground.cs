using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPosition;
    private float repeatWidth;

    void Start()
    {
        startPosition = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }
    // Update is called once per frame
    void Update()
    {
        if (startPosition.x - transform.position.x > repeatWidth)
            transform.position = startPosition;
    }
}
