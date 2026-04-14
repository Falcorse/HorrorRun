using UnityEngine;

public class Coin : Pickups
{
    ScoreManger scoreManger;

    public void init(ScoreManger scoreManger)
    {
        this.scoreManger = scoreManger;
    }
    protected override void OnPickUp()
    {
        scoreManger.IncrementScore(100);
    }
}
