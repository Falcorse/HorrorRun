using UnityEngine;
using System.Collections.Generic;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject FencePrefab;
    [SerializeField] GameObject ApplePrefab;
    [SerializeField] GameObject CoinPrefab;
    [SerializeField] float[] lanes = {-2.5f,0f,2.5f};

    [SerializeField] float AppleSpawnChance = 0.5f;
    [SerializeField] float CoinSpawnChance = 0.7f;
    [SerializeField] float CoinSeperationLength = 2f;
    LevelGenerator levelGenerator;
    ScoreManger scoreManger;

    List<int> AvailabeLanes = new List<int>{0,1,2};

    void Start()
    {
        SpawnFence();
        SpawnApple();
        SpawnCoin();
    }
    public void init(LevelGenerator levelGenerator , ScoreManger scoreManger)
    {
        this.levelGenerator = levelGenerator;
        this.scoreManger = scoreManger;
    }
    void SpawnFence()
    {
        int FencesToSpawn = Random.Range(0,lanes.Length);
        for(int i = 0;i<FencesToSpawn; i++)
        {
            if(AvailabeLanes.Count <= 0) break;
            int SelectedLane = SelectLane();
            Vector3 SpawanPosition = new Vector3(lanes[SelectedLane],transform.position.y,transform.position.z);
            Instantiate(FencePrefab,SpawanPosition,Quaternion.identity,this.transform);
        }

    }
    void SpawnApple()
    {
        if(Random.value > AppleSpawnChance || AvailabeLanes.Count <= 0) return;
        int SelectedLane = SelectLane();
        Vector3 SpawnPosition = new Vector3(lanes[SelectedLane],transform.position.y,transform.position.z);
        Apple newapple = Instantiate(ApplePrefab,SpawnPosition,Quaternion.identity,this.transform).GetComponent<Apple>();
        newapple.init(levelGenerator);
    }

    void SpawnCoin()
    {
        
        if(Random.value > CoinSpawnChance || AvailabeLanes.Count <= 0) return;
        int SelectedLane = SelectLane();

        int MaxCoinToSpawn = 6;
        int CoinToSpawn = Random.Range(1,MaxCoinToSpawn);

        float TopOfZPos = transform.position.z + (CoinSeperationLength * 2f);
        for(int i = 0;i<CoinToSpawn;i++)
        {
            float SpawnPositionZ = TopOfZPos - (i*CoinSeperationLength);
            Vector3 SpawnPosition = new Vector3(lanes[SelectedLane],transform.position.y,SpawnPositionZ);
            Coin newcoin = Instantiate(CoinPrefab,SpawnPosition,Quaternion.identity,this.transform).GetComponent<Coin>();
            newcoin.init(scoreManger);
        } 
    }

    int SelectLane()
    {
        int RandomLane = Random.Range(0,AvailabeLanes.Count);
        int SelectedLane = AvailabeLanes[RandomLane];
        AvailabeLanes.RemoveAt(RandomLane);
        return SelectedLane;
    }
}
