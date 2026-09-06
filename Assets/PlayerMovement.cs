using UnityEngine;
using UnityEngine.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float speed;

    private vector2 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<RigidBody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        body.linearVelocity = new Vector2(directionVec.x * speed, directionVec.y * speed);
    }

    public void Move(InputAction.CallbackContext context)
    {
        
    }
}
