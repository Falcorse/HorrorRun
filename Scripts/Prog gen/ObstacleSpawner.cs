using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] ObstaclePrefabs;
    [SerializeField] float ObstacleSpawnTime = 1f;
    [SerializeField] Transform ObjectParent;
    [SerializeField] float SpawnWidth = 4f;
    [SerializeField] float MinObstacleSpawnTime = 0.2f;
    

    void Start()
    {
        StartCoroutine(SpawnObstacleRoutine());
    }
    public void decreaseSpawnTime(float Amount)
    {
        ObstacleSpawnTime -= Amount;
        if(ObstacleSpawnTime <= MinObstacleSpawnTime)
        {
            ObstacleSpawnTime = MinObstacleSpawnTime;
        }
    }
    IEnumerator SpawnObstacleRoutine()
    {
        while(true)
        {
            GameObject ObstaclePrefab = ObstaclePrefabs[Random.Range(0,ObstaclePrefabs.Length)];
            Vector3 SpawnPosition = new Vector3(Random.Range(SpawnWidth,-SpawnWidth),transform.position.y,transform.position.z);
            yield return new WaitForSeconds(ObstacleSpawnTime);
            Instantiate(ObstaclePrefab,SpawnPosition,Random.rotation,ObjectParent);
        }    
    }

}
