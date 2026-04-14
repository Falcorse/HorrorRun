using UnityEngine;
using System.Collections.Generic;
public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject[] ChunkPrefab;
    [SerializeField] GameObject Checkpoint;
    [SerializeField] Transform ChunkParent;
    [SerializeField] CameraController cameraController;
    [SerializeField] ScoreManger scoreManger;

    [Header("Level Settings")]
    [SerializeField] int StartingChunksAmount = 12;
    [Tooltip("Do not change chunklength value unless chunk prefab size reflects change")]
    [SerializeField] float ChunkLength = 10f;
    [SerializeField] float MoveSpeed = 5f;
    [SerializeField] float MinMoveSpeed = 2f;
    [SerializeField] float MaxMoveSpeed = 20f;
    [SerializeField] float MinGravityX = -22f;
    [SerializeField] float MaxGravityX = -2f;

    [SerializeField] int CheckPointSpawnInterval =8;
    List<GameObject> chunks = new List<GameObject>();
    int ChunksSpawned = 0;

    void Start()
    {
        SpawnStartingChunks();
    }
    void Update()
    {
        MoveChunks();
    }
    public void ChangeChunkMoveSpeed(float SpeedAmount)
    {
        float newMoveSpeed = SpeedAmount + MoveSpeed;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed,MinMoveSpeed,MaxMoveSpeed);
        if (newMoveSpeed != MoveSpeed)
        {
            MoveSpeed = newMoveSpeed;

            float newGravityZ = Physics.gravity.z -SpeedAmount;
            newGravityZ = Mathf.Clamp(newGravityZ,MinGravityX,MaxGravityX);
            
            Physics.gravity = new Vector3(Physics.gravity.x,Physics.gravity.y,newGravityZ);
            cameraController.ChangeCameraFOV(SpeedAmount);
        }
    }
    void SpawnStartingChunks()
    {
        for(int i = 0;i<StartingChunksAmount ; i++)
        {
            SpawnChunk();
        }
    }
    void SpawnChunk()
    {
        float SpawnPositionz = CalculateSpawnPositionz();
        Vector3 ChunkSpawnPos = new Vector3(transform.position.x,transform.position.y,SpawnPositionz);
        GameObject ChunkToSpawn = ChooseChunkToSpawn();
        GameObject NewChunkGO =Instantiate(ChunkToSpawn,ChunkSpawnPos,Quaternion.identity,ChunkParent);
        chunks.Add(NewChunkGO);
        Chunk newChunk = NewChunkGO.GetComponent<Chunk>();
        newChunk.init(this,scoreManger);
        ChunksSpawned ++;
    }
    GameObject ChooseChunkToSpawn()
    {
        GameObject ChunkToSpawn;
        if(ChunksSpawned % CheckPointSpawnInterval == 0 && ChunksSpawned != 0)
        {
            ChunkToSpawn = Checkpoint;
        }
        else
        {
            ChunkToSpawn = ChunkPrefab[Random.Range(0,ChunkPrefab.Length)];
        }
        return ChunkToSpawn;
    }
    float CalculateSpawnPositionz()
    {
        float SpawnPositionz;
        if(chunks.Count == 0)
        {
            SpawnPositionz = 0;
        }
        else
        {
            SpawnPositionz = chunks[chunks.Count-1].transform.position.z + ChunkLength;
        }
        return SpawnPositionz;
    }
    void MoveChunks()
    {
        for(int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (MoveSpeed * Time.deltaTime));
            if(chunk.transform.position.z <= Camera.main.transform.position.z - ChunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
