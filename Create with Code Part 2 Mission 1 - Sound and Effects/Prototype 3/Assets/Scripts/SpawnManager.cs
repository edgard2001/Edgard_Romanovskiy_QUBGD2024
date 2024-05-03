using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstacle;
    public Vector3 spawnPosition;

    public float initialDelay = 3;
    public float spawnRate = 1;
    
    private PlayerController playerControllerScript;

    void Start()
    {
        InvokeRepeating("SpawnObstacle", initialDelay, 1 / spawnRate);

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void SpawnObstacle()
    {
        if (!playerControllerScript.gameOver)
            Instantiate(obstacle, spawnPosition, Quaternion.identity);
    }
}
