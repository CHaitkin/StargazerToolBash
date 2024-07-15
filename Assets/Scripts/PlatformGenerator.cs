using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformGenerator : MonoBehaviour
{
    [Tooltip("When a platform should spawn, a random prefab from this list is chosen.")]
    public GameObject[] platformPrefabs;

    [Tooltip("The distance between platforms at Y = 0. This is the easiest difficuly.")]
    public PlatformSpawnParameters startSpawnParameters;
    [Tooltip("The distance between platforms at the end-level height. This is the highest difficulty.")]
    public PlatformSpawnParameters endSpawnParameters;

    public float startGameHeight;
    public float endGameHeight;

    public float spawnZoneWidth = 7;
    public float nextSpawnDistance = 10;

    [Tooltip("The max number of platforms to spawn per frame.")]
    public int maxPlatforms = 20;

    private float previousSpawnHeight;

    private new Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        previousSpawnHeight = startGameHeight;
        NextPlatformSpawn(true);
        camera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        maxPlatforms = 20;
        while (camera.transform.position.y > previousSpawnHeight - nextSpawnDistance)
        {
            NextPlatformSpawn(false);

            maxPlatforms--;
            if (maxPlatforms <= 0)
            {
                break;
            }
        }
    }

    private void NextPlatformSpawn(bool isFirstPlatform)
    {
        GameObject randomPrefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
        Vector3 randomPosition = GetNextPlatformPosition();

        if (isFirstPlatform)
        {
            randomPosition.y = startGameHeight;
        }

        previousSpawnHeight = randomPosition.y;

        GameObject thisPrefab = Instantiate(randomPrefab, randomPosition, randomPrefab.transform.rotation);
        thisPrefab.AddComponent<PlatformBehavior>();
        thisPrefab.AddComponent<Rigidbody2D>();
        thisPrefab.GetComponent<Rigidbody2D>().gravityScale = 0f;
        thisPrefab.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        
    }

    private Vector3 GetNextPlatformPosition()
    {
        float difficultyPercent = (previousSpawnHeight - startGameHeight) / endGameHeight;
        float minDistanceToNextPlatform = Mathf.Lerp(
            startSpawnParameters.minDistanceToNextPlatform,
            endSpawnParameters.maxDistanceToNextPlatform,
            difficultyPercent);
        float maxDistanceToNextPlatform = Mathf.Lerp(
            startSpawnParameters.maxDistanceToNextPlatform,
            endSpawnParameters.maxDistanceToNextPlatform,
            difficultyPercent);

        float distanceToNextPlatform = Random.Range(minDistanceToNextPlatform, maxDistanceToNextPlatform);
        float xPos = Random.Range(0, spawnZoneWidth) - spawnZoneWidth / 2;
        float yPos = previousSpawnHeight + distanceToNextPlatform;

        return new Vector3(xPos, yPos, 0);
    }

    [Serializable]
    public class PlatformSpawnParameters
    {
        [Tooltip("The lowest possible randomized distance from one platform to the next.")]
        public float minDistanceToNextPlatform = 1;
        [Tooltip("The highest possible randomized distance from one platform to the next.")]
        public float maxDistanceToNextPlatform = 3;
    }
}
