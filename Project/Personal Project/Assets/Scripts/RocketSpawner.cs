using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Player _player;

    [SerializeField] private float spawnStartDelay = 5f;
    [SerializeField] private float spawnRate = 1f;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float spawnRadius = 20;
    private float _speed;

    [SerializeField] private float interceptTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _player = playerTransform.GetComponent<Player>();
        _speed = projectile.GetComponent<Projectile>().speed;
        InvokeRepeating("SpawnProjectile", spawnStartDelay, 1 / spawnRate);
    }

    void SpawnProjectile()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        //Vector3 spawnPosition = transform.position + Vector3.up * playerTransform.position.y;
        Vector3 spawnPosition = playerTransform.position + spawnRadius * new Vector3(randomDirection.x, 0, randomDirection.y);
        
        float cosA = Mathf.Cos(Vector3.Angle(_player.Velocity, spawnPosition - playerTransform.position) * Mathf.PI / 180);
        float c = _player.Velocity.magnitude * interceptTime;
        float b = (playerTransform.position - spawnPosition).magnitude;
        float a = Mathf.Sqrt(c * c + b * b - 2 * b * c * cosA);
        //print(cosA + " " + b + " " + c + " " + a);

        Vector3 direction = playerTransform.position + _player.Velocity * interceptTime - spawnPosition;
        Projectile rocket = Instantiate(projectile, spawnPosition, Quaternion.LookRotation(direction, Vector3.up)).GetComponent<Projectile>();
        rocket.speed = a / interceptTime;
        //Debug.DrawLine(spawnPosition, playerTransform.position + _player.Velocity * interceptTime, Color.red, 2);
    }
}
