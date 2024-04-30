using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float limitX = 20.0f;
    [SerializeField] private GameObject food;


    private float _horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * _horizontalInput * Time.deltaTime * speed);

        if (Mathf.Abs(transform.position.x) > limitX)
            transform.position -= (transform.position.x - Mathf.Sign(transform.position.x) * limitX) * transform.right;

        if (Input.GetKeyDown(KeyCode.Space))
            Instantiate(food, transform.position, food.transform.rotation);

    }
}
