using System;
using System.Collections;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject[] platformPrefabs;

    public float endLevelHeight = 10;
    public float startLevelHeight = 0;

    public float leftLimit = -5f;
    public float rightLimit = 0f;

    public float spawnDelay = 5f;


    private void Start()
    {
        InvokeRepeating("NextPlatformSpawn", 0, spawnDelay);
    }

    private void NextPlatformSpawn()
    {

        GameObject randomPrefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];

        Vector3 randomPosition = new Vector3(Random.Range(leftLimit, rightLimit), startLevelHeight, 0);

        GameObject thisPrefab = Instantiate(randomPrefab, randomPosition, randomPrefab.transform.rotation);
        thisPrefab.AddComponent<PlatformBehavior>();
        thisPrefab.AddComponent<Rigidbody2D>();
        thisPrefab.GetComponent<Rigidbody2D>().isKinematic = true;
        thisPrefab.GetComponent<Rigidbody2D>().gravityScale = 0f;
        thisPrefab.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        thisPrefab.GetComponent<Rigidbody2D>().useFullKinematicContacts = true;

        Debug.Log($"I generated {randomPrefab.name} at {randomPosition}");
    }
}
