using UnityEngine;

public class Apple : Pickups
{
    [SerializeField] float IncreaseSpeedAmount = 3f;
    LevelGenerator levelGenerator;

    public void init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }
    protected override void OnPickUp()
    {
        levelGenerator.ChangeChunkMoveSpeed(IncreaseSpeedAmount);
    }
    

}
