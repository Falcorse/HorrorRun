using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float CheckPointTimeExtension = 5f;
    [SerializeField] float decreaseTime = 0.2f;
    [SerializeField] AudioSource checkpost;
    GameManger gameManger;
    ObstacleSpawner obstacleSpawner;

    const string  PlayerString = "Player";
    void Start()
    {
        gameManger = FindFirstObjectByType<GameManger>();
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(PlayerString))
        {
            gameManger.IncreaseTime(CheckPointTimeExtension);
            obstacleSpawner.decreaseSpawnTime(decreaseTime);
            checkpost.Play();
        }
    }
}
