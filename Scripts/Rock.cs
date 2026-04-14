using UnityEngine;
using Unity.Cinemachine;

public class Rock : MonoBehaviour
{
    [SerializeField] ParticleSystem CollisionParticleSystem;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float CollisionCooldown = 1f;

    float collisionTimer = 1f;
    CinemachineImpulseSource cinemachineImpulseSource;
    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }
    void Update()
    {
        collisionTimer += Time.deltaTime;
    }
    void OnCollisionEnter(Collision other) 
    {
        if(collisionTimer < CollisionCooldown) return;
        
        FireImpulse();
        CollisionVFX(other);
        collisionTimer = 0f;
    }
    void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position,Camera.main.transform.position);
        float intensity = 1f/distance;
        intensity = Mathf.Min(intensity,1f);
        cinemachineImpulseSource.GenerateImpulse();
    }
    void CollisionVFX(Collision other)
    {
        ContactPoint contactPoint = other.contacts[0];
        CollisionParticleSystem.transform.position = contactPoint.point;
        CollisionParticleSystem.Play();
        audioSource.Play();

    }
}
