using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float xclamp =4f;
    [SerializeField] float zclamp = 2f;
    Rigidbody rb;
    Vector2 Movement;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        HandleMovement();
    }
    public void Move(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
        transform.Translate(Movement * speed * Time.deltaTime);
    }
    void HandleMovement()
    {
        Vector3 CurrentPosition = rb.position;
        Vector3 MoveDirection = new Vector3(Movement.x , 0f , Movement.y);
        Vector3 NewPosition = CurrentPosition + MoveDirection * (speed * Time.fixedDeltaTime);
        NewPosition.x = Mathf.Clamp(NewPosition.x, -xclamp, xclamp);
        NewPosition.z = Mathf.Clamp(NewPosition.z, -zclamp, zclamp);
        rb.MovePosition(NewPosition);
    }
}
