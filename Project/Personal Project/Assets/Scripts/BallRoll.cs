using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRoll : MonoBehaviour
{
    [SerializeField] private Transform model;
    private Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //model.rotation = Quaternion.Euler(-360 * Vector3.forward * direction.x * Time.deltaTime / (Mathf.PI * transform.localScale.x)) * model.rotation;
        //model.rotation = Quaternion.Euler(360 * Vector3.right * direction.z * Time.deltaTime / (Mathf.PI * transform.localScale.x)) * model.rotation;
        model.rotation = Quaternion.AngleAxis(360 * direction.magnitude * Time.deltaTime / (Mathf.PI * transform.localScale.x), Vector3.Cross(Vector3.up, direction.normalized)) * model.rotation;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
}
