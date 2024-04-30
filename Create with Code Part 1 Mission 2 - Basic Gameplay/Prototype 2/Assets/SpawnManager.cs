using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject[] animals;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAnimal", 2f, 1.5f);
    }

    // Update is called once per frame
    void SpawnAnimal()
    {
        int animalIndex = Random.Range(0, animals.Length);
        Instantiate(animals[animalIndex], new Vector3(Random.Range(-20, 20), 0, 30), animals[animalIndex].transform.rotation);
    }
}
