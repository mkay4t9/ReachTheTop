using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{ 
    [SerializeField] private GameObject[] obstaclesPrefab, powerupPrefabs;
    public Transform spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomObstacles", 1.0f, 3.0f);
        int randomPowerUp = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerUp], spawnPos.position, powerupPrefabs[randomPowerUp].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnRandomObstacles()
    {
        if(!PlayerController.gameOver && !PlayerController.gameCompleted)
        {
            int randomObstacle = Random.Range(0, obstaclesPrefab.Length);
            Instantiate(obstaclesPrefab[randomObstacle], transform.position, obstaclesPrefab[randomObstacle].transform.rotation);
        }
    }
}
