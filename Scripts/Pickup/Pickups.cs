using UnityEngine;

public abstract class Pickups : MonoBehaviour
{
    const string PlayerString = "Player";
    [SerializeField] float RotateSpeed = 100f;
    void Update()
    {
        transform.Rotate(0f,RotateSpeed*Time.deltaTime,0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerString))
        {
            OnPickUp();
            Destroy(gameObject);
        }
    }
    protected abstract void OnPickUp();
}
