using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private float minSpawnRadius = 5;
    [SerializeField] private float maxSpawnRadius = 15;

    private Transform _playerTransform;
    private int _turretCount = 1;
    private bool spawning = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (FindObjectsByType<RocketSpawner>(FindObjectsSortMode.None).Length == 0 && !spawning)
        {
            SpawnTurrets(_turretCount);
        }
    }

    void SpawnTurrets(int spawnCount)
    {
        spawning = true;
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = Vector3.zero;

            int retryCount = 0;
            bool spawnBlocked = true;
            while (spawnBlocked && retryCount++ < 5)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                float spawnRadius = Random.Range(minSpawnRadius, maxSpawnRadius);
                spawnPosition = _playerTransform.position + spawnRadius * new Vector3(randomDirection.x, 0, randomDirection.y);
                spawnPosition.y = -0.5f;
                
                spawnBlocked = Physics.CheckSphere(spawnPosition, 1.5f, 1 << LayerMask.NameToLayer("Default"));
            }
            if (!spawnBlocked)
            {
                Instantiate(turretPrefab, spawnPosition, Quaternion.identity);
            }
        }
        _turretCount++;
        spawning = false;
    }

}
