using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerController playerController;
    [Header("Settings")]
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject GameOverText;
    [SerializeField] float StartTime = 5f;
    [SerializeField] AudioSource NoScream;
    [SerializeField] AudioSource Walking;

    bool gameOver = false;

    float TimeLeft;
    public bool GameOver => gameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimeLeft =StartTime;
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseTime();
    }
    void GameOverScene()
    {
        gameOver = true;
        playerController.enabled = false;
        GameOverText.SetActive(true);
        Time.timeScale = .1f;
        NoScream.Play();
        Walking.Stop();
        StartCoroutine(RestartAfterDelay());

    }
    void DecreaseTime()
    {
        if(gameOver) return;
        TimeLeft -= Time.deltaTime;
        timeText.text = TimeLeft.ToString("F1");

        if(TimeLeft <= 0f)
        {
            GameOverScene();
        }
    }
    public void IncreaseTime(float Amount)
    {
        TimeLeft += Amount;
    }
    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
