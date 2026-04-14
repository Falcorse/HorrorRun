using UnityEngine;
using System.Collections;
using Unity.Cinemachine;


public class CameraController : MonoBehaviour
{
    [SerializeField] float MinFov = 20f;
    [SerializeField] float MaxFov = 120f;
    [SerializeField] float ZoomDuration = 1f;
    [SerializeField] float ZoomSpeedModifier = 5f;
    [SerializeField] ParticleSystem SpeedUpParticleSystem;
    CinemachineCamera cinemachineCamera;

    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ChangeCameraFOV(float SpeedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFovRoutine(SpeedAmount));
        if(SpeedAmount > 0)
        {
            SpeedUpParticleSystem.Play();
        } 
    }

    IEnumerator ChangeFovRoutine(float SpeedAmount)
    {
        float StartFov = cinemachineCamera.Lens.FieldOfView;
        float TargetFov = Mathf.Clamp(StartFov + SpeedAmount * ZoomSpeedModifier, MinFov, MaxFov);

        float ElapsedTime = 0f;

        while (ElapsedTime < ZoomDuration)
        {
            ElapsedTime += Time.deltaTime;
            float t = ElapsedTime / ZoomDuration;

            cinemachineCamera.Lens.FieldOfView =
                Mathf.Lerp(StartFov, TargetFov, t);

            yield return null;  
        }

        cinemachineCamera.Lens.FieldOfView = TargetFov;
    }
}
