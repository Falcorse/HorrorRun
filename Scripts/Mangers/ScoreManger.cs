using UnityEngine;
using TMPro;

public class ScoreManger : MonoBehaviour
{
    [SerializeField] TMP_Text Scoretext;
    [SerializeField] GameManger gameManger;
    int score = 0;

    public void IncrementScore(int value)
    {
        if (gameManger.GameOver) return;
        score += value;
        Scoretext.text = score.ToString();
    }
}
