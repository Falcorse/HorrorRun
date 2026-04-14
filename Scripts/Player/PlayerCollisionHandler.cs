using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float CollisionCooldown = 1f;
    [SerializeField] float AdjustChangeMoveSpeed = -2f;
    [SerializeField]  AudioSource audioSource;
    LevelGenerator levelGenerator;
    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    const string hitString ="Hit";
    float CoolDownTimer = 0f;
    void Update()
    {
        CoolDownTimer += Time.deltaTime;
    }
    void OnCollisionEnter(Collision other)
    {
        if(CoolDownTimer < CollisionCooldown) return;
        animator.SetTrigger(hitString);
        levelGenerator.ChangeChunkMoveSpeed(AdjustChangeMoveSpeed);
        audioSource.Play();
        CoolDownTimer = 0f;
    }
}
