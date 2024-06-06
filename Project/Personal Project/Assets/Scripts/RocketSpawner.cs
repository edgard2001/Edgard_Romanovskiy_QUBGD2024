using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    //[SerializeField] private float lifeTime = 30;

    private Transform _playerTransform;
    private Player _player;

    [SerializeField] private float spawnStartDelay = 5f;
    [SerializeField] private float spawnRate = 1f;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float spawnRadius = 20;
    private float _speed;

    [SerializeField] private float interceptTime = 1f;

    private bool _seekTarget;
    [SerializeField] private Transform turretTransform;

    [SerializeField] private ParticleSystem despawnEffect;
    [SerializeField] private GameObject model;

    [SerializeField] private Transform[] missileMounts;
    private int _missilesUsed = 0;

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        _playerTransform = playerObject.transform;
        _player = playerObject.GetComponent<Player>();

        _speed = projectile.GetComponent<Projectile>().speed;

        _seekTarget = false;
        Invoke("SeekTarget", spawnStartDelay);
        //Invoke("SelfDestruct", lifeTime);
    }

    void Update()
    {
        if (_missilesUsed == missileMounts.Length) return;

        Transform missileTransform = missileMounts[_missilesUsed];

        //Vector2 randomDirection = Random.insideUnitCircle.normalized;
        //Vector3 spawnPosition = _playerTransform.position + spawnRadius * new Vector3(randomDirection.x, 0, randomDirection.y);

        Vector3 spawnPosition = missileTransform.position;
        //spawnPosition.y = _playerTransform.position.y;

        Vector3 spawnPositionXZ = spawnPosition;
        spawnPositionXZ.y = 0;
        Vector3 playerVelocityXZ = _player.Velocity;
        playerVelocityXZ.y = 0;
        Vector3 playerPositionXZ = _playerTransform.position;
        playerPositionXZ.y = 0;

        float cosA = Mathf.Cos(Vector3.Angle(playerVelocityXZ, spawnPositionXZ - playerPositionXZ) * Mathf.PI / 180);
        float c = playerVelocityXZ.magnitude * interceptTime;
        float b = (playerPositionXZ - spawnPositionXZ).magnitude;
        float distance = Mathf.Sqrt(c * c + b * b - 2 * b * c * cosA);

        Vector3 direction = playerPositionXZ + playerVelocityXZ * interceptTime - spawnPositionXZ;

        turretTransform.rotation = Quaternion.Lerp(turretTransform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime * 3);


        if (!_seekTarget) return;

        if (Quaternion.Angle(turretTransform.rotation, Quaternion.LookRotation(direction, Vector3.up)) > 10) return;

        SpawnProjectile(spawnPosition, direction, distance);
        _seekTarget = false;
        Invoke("SeekTarget", 1 / spawnRate);

        missileTransform.gameObject.SetActive(false);
        _missilesUsed++;
        if (_missilesUsed == missileMounts.Length)
        {
            Invoke("PlayDespawnEffect", 1f);
            Invoke("SelfDestruct", 3f);
        }
    }

    void SpawnProjectile(Vector3 spawnPosition, Vector3 direction, float distance)
    {

        Projectile rocket = Instantiate(projectile, spawnPosition, Quaternion.LookRotation(direction, Vector3.up)).GetComponent<Projectile>();
        rocket.speed = distance / interceptTime;
        //Debug.DrawLine(spawnPosition, playerTransform.position + _player.Velocity * interceptTime, Color.red, 2);
    }

    void SeekTarget()
    {
        _seekTarget = true;
    }

    void PlayDespawnEffect()
    {
        model.SetActive(false);
        despawnEffect.Play();
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
