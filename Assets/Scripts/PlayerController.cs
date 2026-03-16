using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 10f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 1f;
    [SerializeField, Range(0f, 10f)] private float jumpHeight = 2f;
    [SerializeField, Range(0f, 5f)] private int maxAirJumps = 0;
    
    private Vector3 velocity;
    private Vector3 desiredVelocity;
    private Rigidbody body;
    
    [SerializeField] private bool desiredJump;
    [SerializeField] private bool onGround;
    [SerializeField] private int jumpPhase;
    
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        
        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        desiredJump |= Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        UpdateState();
        
        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }
        
        var acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        var maxSpeedChange = acceleration * Time.deltaTime;
        
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        
        onGround = false; // Keep this check at END of FixedUpdate.
    }
    
    private void UpdateState()
    {
        velocity = body.linearVelocity;

        if (onGround)
        {
            jumpPhase = 0;
        }
    }
    
    private void Jump()
    {
        if (onGround || jumpPhase < maxAirJumps)
        {
            jumpPhase += 1;
            var jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            
            if (velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed = velocity.y, 0f);
            }
            
            velocity.y += jumpSpeed;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void EvaluateCollision(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            onGround |= normal.y >= 0.9f;
        }
    }

    private void Move()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        // playerInput.Normalize(); // More limited movement. 
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        
        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        var maxSpeedChange = maxAcceleration * Time.deltaTime;
        
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        
        var displacement = velocity * Time.deltaTime;
        var newPosition = transform.localPosition + displacement;
        
        //transform.localPosition = new Vector3(playerInput.x, 0.5f, playerInput.y);
        
        transform.localPosition = newPosition;
    }
}
